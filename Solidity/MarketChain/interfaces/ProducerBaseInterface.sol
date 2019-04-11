pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./pure/IProducerBase.sol";
import "./BaseMarketInterface.sol";

import "../libraries/InventoryLib.sol";

import "../contracts/InvoiceProductPurchaseValidator.sol";

contract ProducerBaseInterface is IProducerBase {
  
    function initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint initialOwnerVoteWeight) public;

    function registerPurchaseWithInvoice (
            InvoiceProductPurchaseValidator.InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) public returns (bool);

    function getStoreFrontsByPageNum (address storeOwner, uint pageNum) public view returns(InventoryLib.StoreFront[] memory);

    function getStoreFrontById (address storeOwner, uint storeFrontId) public view returns(InventoryLib.StoreFront memory);

    function getProductsByPageNum (address storeOwner, uint storeFrontId, uint pageNum) public view returns(InventoryLib.Product[] memory);

    function getProductIdsByPageNum (address storeOwner, uint storeFrontId, uint pageNum) public view returns(uint[] memory);

    function getProductById (uint productId) public view returns(InventoryLib.Product memory);
}