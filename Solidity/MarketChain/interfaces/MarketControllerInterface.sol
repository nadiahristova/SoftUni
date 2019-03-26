pragma solidity >=0.5.6 <0.6.0;

interface MarketControllerInterface {
    
    function registerMarketMember(address member) external;

    function supportProducer(address producer) external returns (bool);

    function voteForMarketMemberBlock(address producer) external returns (bool);

    function revokeMembership(address member) external;

    //function kill() external returns (bool);
    // initialize function skipped
}