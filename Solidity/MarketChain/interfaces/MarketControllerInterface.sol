pragma solidity >=0.5.6 <0.6.0;

import "./ProducerBaseInterface.sol";

interface MarketControllerInterface { // abstract contract
    
    function registerMarketMember(address member) external;

    function supportProducer(address producer) external returns (bool);

    function voteForMarketMemberBlock(address producer) external returns (bool);

    function revokeMembership(address member) external;

    function sendJoinRequest(address accAddress, ProducerBaseInterface memberBase) external;

    //function kill() external returns (bool);
    // initialize function skipped
}