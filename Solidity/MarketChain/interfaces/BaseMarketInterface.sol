pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "../interfaces/ProducerBaseInterface.sol";
import "../interfaces/ClientBaseInterface.sol";

import "../contracts/InvoiceProductPurchaseValidator.sol";

contract BaseMarketInterface {

    // function affiliateClientBase (ClientBaseInterface memberBase) external;

    // function affiliateProducerBase (ProducerBaseInterface memberBase) external;

    // function openStore (ProducerBaseInterface memberBase, bytes32 name) external;

    // function addStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) external returns (bool);

    function memberBaseAddStoreFront (address storeOwner, uint storeFrontId) public returns (bool);

    // function removeStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) external returns (bool);

    // function notifyForStoreFrontDeletion (address storeOwner, uint256 storeFrontId) public returns (bool);

    // function buyProduct (InvoiceProductPurchaseValidator.InvoiceDetails memory invoice, uint256 nonce, bytes memory signature) public payable returns(bool);

    // function retrieveProfit () external payable returns (bool);
}