pragma solidity >=0.5.6 <0.6.0;

import "../libraries/ECDSA.sol";

contract InvoiceProductPurchaseValidator {
    using ECDSA for bytes32;
    
    mapping (address => mapping(uint => bool)) seenNonces;

    struct InvoiceDetails {
        address seller;
        address buyer;
        address producerBase;
        uint256 productId;
        uint256 storeFrontId;
        uint256 amount;
        uint256 pricePerUnit;
        uint256 validUntil;
    }

    function _validateProductPurchase (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        internal 
    returns (bool) {

        require (!seenNonces[invoice.seller][nonce]);

        // This recreates the message hash that was signed on the client.
        bytes32 hash = keccak256(abi.encodePacked(invoice.seller, invoice.buyer, invoice.producerBase, invoice.productId, 
        invoice.storeFrontId, invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce));
        bytes32 messageHash = hash.toEthSignedMessageHash();
        
        // Verify that the message's signer is the owner of the order
        address signer = messageHash.recover(signature);

        require(invoice.seller == signer);

        seenNonces[invoice.seller][nonce] = true;

        return true;
    }

    function _hasValidState(InvoiceDetails memory invoice) internal view returns (bool) {
        return invoice.seller != address(0) 
            && invoice.amount >= 0 
            && invoice.pricePerUnit > 0 
            && invoice.validUntil >= now;
    }
}