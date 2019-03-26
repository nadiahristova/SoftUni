pragma solidity >=0.5.6 <0.6.0;

interface MemberBaseInterface {

    function registerMember() external;

    function sendJoinRequest() external returns (bool);

    //function revokeMembership() external returns (uint);
}