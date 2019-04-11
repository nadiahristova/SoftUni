pragma solidity >=0.5.6 <0.6.0;

import "./pure/IMarketMemberBase.sol";

contract MarketMemberBaseInterface is IMarketMemberBase {

    function isPartner (address market) view public returns(bool);

    function hasMarketMembership (address accAddress, address market) view public returns(bool);
}