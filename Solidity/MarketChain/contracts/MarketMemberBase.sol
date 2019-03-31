pragma solidity >=0.5.6 <0.6.0;

import "./VotingMemberBase.sol";
import "./VotingMemberBase.sol";

import "../libraries/MarketPartnerLib.sol";

contract MarketMemberBase is VotingMemberBase {
    
    using MarketPartnerLib for MarketPartnerLib.PartnerMarkets;

    MarketPartnerLib.PartnerMarkets _partnerMarkets;

    // https://blog.colony.io/writing-upgradeable-contracts-in-solidity-6743f0eecc88/ Upgrade Repository by using library and another contract

    event LogMarketPartnerAdded(address indexed partnerAddress);
    event LogMarketPartnerRemoved(address indexed exMemberAddress);
    event LogStoreOpeningConfirmed(address indexed accAddress, address indexed marketAddress);
    event LogStoreClosureConfirmation(address indexed accAddress, address indexed marketAddress);

    
    modifier onlyOnValidMarketMembership(address shopOwner, address market) {
        require(_partnerMarkets._hasMarketMembership(shopOwner, market));
        _;
    }

    function initialize (uint[2] memory defaultCampaignTimePeriods, uint decisiveVoteWeightProportion, uint decisiveVoteCountProportion, bytes32[] memory campaignNames, uint[] memory campaignTimePeriods) onlyOwner public returns(bool) {
        require(!_isInitialized);
        uint numOfCampaigns = campaignNames.length;
        require(numOfCampaigns == campaignTimePeriods.length && numOfCampaigns <= 25);

        for (uint i = 0; i < numOfCampaigns; i++) {
            require(campaignNames[i] != 0x0 && campaignTimePeriods[i] > 5 minutes);

            _availableCampaigns[i].name = campaignNames[i];
            _availableCampaigns[i].activeTimespan = campaignTimePeriods[i];
        }
        
        super.initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion);
    }

    function hasMarketMembership (address accAddress, address market) public view returns(bool) {
        return _partnerMarkets._hasMarketMembership(market, accAddress);
    }

    function addMarketPartner (MarketControllerInterface market) 
        public 
        onlyOwner
    returns(bool) {
        return _partnerMarkets._addMarketPartner(address(market));
    }

    function removeMarketPartner (MarketControllerInterface market) 
        public 
        onlyOwner
    returns(bool) {
        return _partnerMarkets._removeMarketPartner(address(market));
    }

    function confirmMarketMembership (MarketControllerInterface market) 
        public 
        onlyMember
    returns(bool) {
        return _partnerMarkets._addMarketMembership(address(market), msg.sender);
    }

    function revokeMarketMembership (MarketControllerInterface market) 
        public 
        onlyMember
    returns(bool) {
        return _partnerMarkets._removeMarketMembership(address(market), msg.sender);
    }
}