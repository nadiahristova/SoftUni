pragma solidity >=0.5.6 <0.6.0;

import "../../libraries/VotingMemberBaseLib.sol";

interface IVotingMemberBase {

    function requestMembership () external;

    function launchMembershipGrantingCampaign (address accAddress) external returns(bool);

    function revokeMembership() external returns (MemberBaseLib.RevokeMembershipStatus);

    function triggerMembershipRevocation(address accAddress) external;

    function getVoteWeight (address accAddress) view external returns(uint);
}