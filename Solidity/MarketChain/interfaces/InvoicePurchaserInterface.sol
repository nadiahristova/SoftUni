pragma solidity >=0.5.6 <0.6.0;

contract InvoicePurchaserInterface {
    
    struct InvoiceDetails {
        address seller;
        address buyer;
        address producerBase;
        uint256 storeFrontId;
        uint256 productId;
        uint256 amount;
        uint256 pricePerUnit;
        uint256 validUntil;
    }

    modifier onlyValidInvoice(InvoiceDetails memory invoice) {
        require(invoice.seller != address(0)
            && invoice.producerBase != address(0)
            && invoice.buyer != address(0)
            && invoice.amount > 0 
            && invoice.pricePerUnit >= 0
            && invoice.validUntil >= now, '31');
        _;
    }
}