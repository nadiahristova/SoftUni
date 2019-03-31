pragma solidity >=0.5.6 <0.6.0;

import "../libraries/MemberBaseLib.sol";

interface MemberBaseInterface {

    function registerMember(bytes32 about) external returns (bool);

    //prob will not work
    function revokeMembership() external returns (MemberBaseLib.RevokeMembershipStatus);
}