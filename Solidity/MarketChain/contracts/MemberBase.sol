pragma solidity >=0.5.6 <0.6.0;

import "./Ownable.sol";
import "../libraries/MemberBaseLib.sol";
import "../interfaces/MemberBaseInterface.sol";

contract MemberBase is Ownable, MemberBaseInterface {

// https://blog.colony.io/writing-upgradeable-contracts-in-solidity-6743f0eecc88/ Upgrade Repository by using library and another contract

    using MemberBaseLib for MemberBaseLib.Members;

    MemberBaseLib.Members _memberBase;

    modifier onlyMember() {
        require(isMember(msg.sender));
        _;
    }

    ///@dev Checkes whether an account is a member to a member base
    ///@param accAddress The address of the account
    function isMember(address accAddress) public view returns (bool) {
        return _memberBase._isMember(accAddress);
    }

    ///@dev Add new member to a member base
    function registerMember() external returns (bool) {
        return _memberBase._registerMember(msg.sender);
    }

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function revokeMembership() 
        external 
    returns (MemberBaseLib.RevokeMembershipStatus) {
        return _memberBase._revokeMembership(msg.sender);
    } 
}