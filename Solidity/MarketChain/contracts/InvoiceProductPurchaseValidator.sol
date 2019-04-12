pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "../libraries/ECDSA.sol";

import "../interfaces/InvoicePurchaserInterface.sol";

contract InvoiceProductPurchaseValidator is InvoicePurchaserInterface {
    using ECDSA for bytes32;
    
    //mapping (address => mapping(uint => bool)) seenNonces; 

    // struct InvoiceDetails {
    //     address seller;
    //     address buyer;
    //     address producerBase;
    //     uint256 storeFrontId;
    //     uint256 productId;
    //     uint256 amount;
    //     uint256 pricePerUnit;
    //     uint256 validUntil;
    // }

    // modifier onlyValidInvoice(InvoiceDetails memory invoice) {
    //     require(invoice.seller != address(0)
    //         && invoice.producerBase != address(0)
    //         && invoice.buyer != address(0)
    //         && invoice.amount > 0 
    //         && invoice.pricePerUnit >= 0
    //         && invoice.validUntil >= now, '31');
    //     _;
    // }

    function _validateProductPurchase (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
    returns (bool) {

        require (!seenNonces[invoice.seller][nonce], '15');

        // This recreates the message hash that was signed on the client.
        bytes32 hash = keccak256(abi.encodePacked(invoice.seller, invoice.buyer, invoice.productId, 
        invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce)); 
        bytes32 messageHash = hash.toEthSignedMessageHash();
        
        // Verify that the message's signer is the owner of the order
        address signer = messageHash.recover(signature);

        require(invoice.seller == signer, '14');

        seenNonces[invoice.seller][nonce] = true;

        return true;
    }

    function _returnHash (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        pure
    returns (address) {

        // This recreates the message hash that was signed on the client.
        bytes32 hash = keccak256(abi.encodePacked(invoice.seller, invoice.buyer, invoice.productId, invoice.amount, 
        invoice.pricePerUnit, invoice.validUntil, nonce)); 
        
        bytes32 messageHash = hash.toEthSignedMessageHash();
        
        address signer = messageHash.recover(signature);

        return signer;
    }
}