contract ProducerBaseInterface {
    function registerPurchaseWithInvoice (
            address seller, 
            address buyer,
            uint256 productId,
            uint256 storeFrontId,
            uint256 amount,
            uint256 pricePerUnit,
            uint256 validUntil,
            uint256 nonce, 
            bytes memory signature) public returns (bool);
}