pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./BaseMarketInterface.sol";

import "../contracts/InvoiceProductPurchaseValidator.sol";

contract ProducerBaseInterface {
    function revokeMembership(address accAddress) external returns (bool);

    function addStoreFront(bytes32 name) external;

    function removeStoreFront(uint storeFrontId) external; 

    function disableStoreFront(uint storeFrontId) external; 

    function enableStoreFront(uint storeFrontId) external; 
        
    function pushStoreFrontToMarket(BaseMarketInterface market, uint storeFrontId) external;
            
    function addProductToStoreFront(
            uint storeFrontId,
            uint specificationId, 
            uint pricePerUnit, 
            uint amount, 
            bool hasNegotiablePrice) 
        external;

    function removeProductFromStoreFront(uint storeFrontId, uint productId) external;

    function updateProduct(
            uint storeFrontId,
            uint productId, 
            uint pricePerUnit, 
            uint amountProduced, 
            bool hasNegotiablePrice) 
        external;

    function registerPurchaseWithInvoice (
            InvoiceProductPurchaseValidator.InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) public returns (bool);
}