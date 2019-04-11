pragma solidity >=0.5.6 <0.6.0;

interface IRegionalMarket {

    function openStore (address producerBase, bytes32 name) external;

    function removeStoreFront (address memberBase, uint256 storeFrontId) external;

    function suggestForFunding (address storeOwner, address memberBase) external;

    function supportMember(address member, uint248 votingCampaignId) external;
}