pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "../contracts/InvoiceProductPurchaseValidator.sol";

contract ProducerBaseInterface {

    function registerPurchaseWithInvoice (
            InvoiceProductPurchaseValidator.InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) public returns (bool);
}