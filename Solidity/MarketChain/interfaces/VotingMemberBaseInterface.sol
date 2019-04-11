pragma solidity >=0.5.6 <0.6.0;

import "./pure/IVotingMemberBase.sol";

contract VotingMemberBaseInterface is IVotingMemberBase {
    function hasActiveCampaigns (address accaddress) view public returns(bool);

    function isMember(address accAddress) view public returns(bool);

    function requestMembership () public;
}