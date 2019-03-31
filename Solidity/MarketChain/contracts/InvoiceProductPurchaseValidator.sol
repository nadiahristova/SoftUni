pragma solidity >=0.4.21 <0.6.0;

import "../libraries/ECDSA.sol";

contract InvoiceProductPurchaseValidator {
    using ECDSA for bytes32;
    
    mapping (address => mapping(uint => bool)) seenNonces;

    function _validateProductPurchase (
            address seller, 
            address buyer,
            address producerBase,
            uint256 productId, 
            uint256 storeFrontId, 
            uint256 amount,
            uint256 pricePerUnit,
            uint256 validUntil,
            uint256 nonce, 
            bytes memory signature) 
        internal 
    returns (bool) {

        require (!seenNonces[seller][nonce]);

        // This recreates the message hash that was signed on the client.
        bytes32 hash = keccak256(abi.encodePacked(seller, buyer, producerBase, productId, 
        storeFrontId, amount, pricePerUnit, validUntil, nonce));
        bytes32 messageHash = hash.toEthSignedMessageHash();
        
        // Verify that the message's signer is the owner of the order
        address signer = messageHash.recover(signature);

        require(seller == signer);

        seenNonces[seller][nonce] = true;

        return true;
    }
}