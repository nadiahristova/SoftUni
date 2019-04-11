pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "../interfaces/ProducerBaseInterface.sol";
import "../interfaces/ClientBaseInterface.sol";
import "./pure/IBaseMarket.sol";

import "../contracts/InvoiceProductPurchaseValidator.sol";

contract BaseMarketInterface is IBaseMarket {

    function memberBaseAddStoreFront (address storeOwner, uint storeFrontId) public returns (bool);

    function notifyForStoreFrontDeletion (address storeOwner, uint storeFrontId) public returns (bool);

    function hasStoreFront(address storeOwner, address producerBase, uint storeFrontId) view public returns (bool);
}