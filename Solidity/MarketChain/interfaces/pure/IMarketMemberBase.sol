pragma solidity >=0.5.6 <0.6.0;

interface IMarketMemberBase {

    function addMarketPartner (address market) external;

    function removeMarketPartner (address market) external returns(bool);

    function removeVotingCampaign(address accAddress, uint campaignId) external;

    function getMarketPartners () view external returns(address[25] memory);

    function requestMarketMembership (address market) external;

    function revokeMarketMembership (address market) external;
}