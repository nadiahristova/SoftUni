pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./pure/IRegionalMarket.sol";

import "../contracts/AdministrableByRegion.sol";
import "../contracts/InvoiceProductPurchaseValidator.sol";

contract RegionalMarketInterface is IRegionalMarket {
    function initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint profit_fee, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint donationRoundTimePeriod, 
            uint initialOwnerVoteWeight) public;
    
    function buyProduct (
        InvoiceProductPurchaseValidator.InvoiceDetails memory invoice,
        uint256 nonce, 
        bytes memory signature) public payable returns(bool);

    function registerMember(AdministrableByRegion.Location memory location) public returns (bool);

    function launchMembershipRevocationCampaign (address accAddress) public returns(bool); 

    function triggerFundingDistribution(AdministrableByRegion.Location memory location) public returns (bool);

    function activateFundingCampaign (AdministrableByRegion.Location memory location) public;

    function getDonationCandidates (AdministrableByRegion.Location memory location) 
        public view returns(address[] memory);
    
    function getAccumulatedFundsPerRegion (AdministrableByRegion.Location memory location) 
        public view returns(uint);
    
    function donateToProvince(AdministrableByRegion.Location memory location) public payable;

    function getMembershipInfo(address accAddress) public view returns (bool isMember, bool isAdmin,bool isOwner);
}