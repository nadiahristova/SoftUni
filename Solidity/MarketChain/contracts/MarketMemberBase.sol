pragma solidity >=0.5.6 <0.6.0;

import "./VotingMemberBase.sol";

import "../interfaces/BaseMarketInterface.sol";

import "../libraries/MarketPartnerLib.sol";

contract MarketMemberBase is VotingMemberBase {
    
    using MarketPartnerLib for MarketPartnerLib.PartnerMarkets;

    MarketPartnerLib.PartnerMarkets _partnerMarkets;

    event LogMarketPartnerAdded(address indexed partnerAddress);
    event LogMarketPartnerRemoved(address indexed exMemberAddress);
    event LogMarketMembershipConfirmed(address indexed accAddress, address indexed marketAddress);
    event LogMarketMembershipRevoked(address indexed accAddress, address indexed marketAddress);

    
    modifier onlyOnValidMarketMembership(address shopOwner, address market) {
        require(_partnerMarkets._hasMarketMembership(shopOwner, market));
        _;
    }

    modifier onlyRegisteredPartner(BaseMarketInterface market) {
        require(_partnerMarkets._isMarketRegisteredPartner(address(market)), 'not partner');
        _;
    }

    function _initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint initialOwnerVoteWeight, 
            bytes32[] memory campaignNames, 
            uint[] memory campaignTimePeriods) 
        internal {

        uint numOfCampaigns = campaignNames.length;

        require(numOfCampaigns == campaignTimePeriods.length && numOfCampaigns <= 25);

        for (uint i = 0; i < numOfCampaigns; i++) {
            require(campaignNames[i] != 0x0 && campaignTimePeriods[i] > 5 minutes);

            _availableCampaigns[i].name = campaignNames[i];
            _availableCampaigns[i].activeTimespan = campaignTimePeriods[i];
        }
        
        _initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, initialOwnerVoteWeight);
    }

    /// @dev Checks whether given member has market membership
    /// @param accAddress Account addressof the member
    /// @param market Targeted market   
    function hasMarketMembership (address accAddress, BaseMarketInterface market)  
        view 
        public
        onlyWhenInitialized
        onlyValidAddress(accAddress)
        onlyValidAddress(address(market))
        onlyRegisteredPartner(market)
    returns(bool) {

        require(isMember(accAddress), 'not member');

        return _partnerMarkets._hasMarketMembership(address(market), accAddress);
    }

    function addMarketPartner (BaseMarketInterface market) 
        external 
        onlyOwner
    returns(bool) {
        require(!_partnerMarkets._isMarketRegisteredPartner(address(market)));

        return _partnerMarkets._addMarketPartner(address(market));
    }

    function removeMarketPartner (BaseMarketInterface market) 
        public 
        onlyOwner
        onlyRegisteredPartner(market)
    returns(bool) {
        return _partnerMarkets._removeMarketPartner(address(market));
    }

    function confirmMarketMembership (BaseMarketInterface market) 
        public 
        onlyMember
        onlyRegisteredPartner(market)
    returns(bool) {
        return _partnerMarkets._addMarketMembership(address(market), msg.sender);
    }

    function revokeMarketMembership (BaseMarketInterface market) 
        public 
        onlyMember
        onlyRegisteredPartner(market)
    returns(bool) {
        return _partnerMarkets._revokeMarketMembership(address(market), msg.sender);
    }

    function _revokeAllMarketMembership (address accAddress) 
        internal 
    returns(bool) {
        _partnerMarkets._removeAllMarketMemberships(accAddress);
    }
}