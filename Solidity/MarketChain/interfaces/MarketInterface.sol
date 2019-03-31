pragma solidity >=0.5.6 <0.6.0;

import "../interfaces/ProducerBaseInterface.sol";
import "../interfaces/ClientBaseInterface.sol";

contract MarketInterface {

    function affiliateClientBase (ClientBaseInterface memberBase, bytes32 name) external;

    function affiliateProducerBase (ProducerBaseInterface memberBase, bytes32 name) external;

    function openStore (ProducerBaseInterface memberBase, bytes32 name) public;

    function addStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) external returns (bool);

    function memberBaseAddStoreFront (address storeOwner, uint storeFrontId) public returns (bool);

    function removeStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) external returns (bool);

    function notifyForStoreFrontDeletion (address storeOwner, uint storeFrontId) public returns (bool);

    function buyProduct (address seller, uint256 amount, uint256 pricePerUnit, uint256 nonce, bytes memory signature) public payable returns(bool);

    function retrieveProfit (ProducerBaseInterface memberBase) external payable;
}