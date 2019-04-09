pragma solidity >=0.5.6 <0.6.0;

import "./VotingMemberBase.sol";

import "../interfaces/BaseMarketInterface.sol";

import "../libraries/PartnerRelationsKeeperLib.sol";


// todo change name
contract MarketMemberBase is VotingMemberBase {
    
    using PartnerRelationsKeeperLib for PartnerRelationsKeeperLib.Partners;

    PartnerRelationsKeeperLib.Partners _partnerMarkets;

    event LogPartnerEntityAdded(address indexed partnerAddress);
    event LogPartnerEntityRemoved(address indexed partnerAddress);
    event LogAffiliationWithPartner(address indexed accAddress, address indexed partnerAddress);
    event LogAffiliationWithPartnerRevoked(address indexed accAddress, address indexed partnerAddress);

    
    modifier onlyOnValidMarketMembership(address shopOwner, address market) {
        require(hasMarketMembership(shopOwner, market));
        _;
    }

    modifier onlyRegisteredPartner(address market) {
        require(isPartner(market), 'Not partner');
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

        require(numOfCampaigns <= 25 && numOfCampaigns == campaignTimePeriods.length);

        for (uint i = 0; i < numOfCampaigns; i++) {
            require(campaignTimePeriods[i] > 35 minutes);

            _availableCampaigns[i].name = campaignNames[i];
            _availableCampaigns[i].activeTimespan = campaignTimePeriods[i];
        }
        
        _initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, initialOwnerVoteWeight);
    }

    function addMarketPartner (address market) 
        external 
        onlyValidAddress(market)
        onlyWhenInitialized
        onlyOwner
    returns(bool) {
        require(!_partnerMarkets._isRegisteredPartner(market), 'Registered');

        return _partnerMarkets._addPartner(market);
    }

    function removeMarketPartner (address market) 
        public 
        onlyOwner
        onlyRegisteredPartner(market)
    returns(bool) {
        return _partnerMarkets._removePartner(market);
    }

    function isPartner (address market) 
        view
        public 
        onlyValidAddress(market)
        onlyWhenInitialized
    returns(bool) {

        return _partnerMarkets._isRegisteredPartner(market);
    }

    function getMarketPartners () 
        view
        public 
        onlyWhenInitialized
    returns(address[25] memory) {

        return _partnerMarkets._getPartners();
    }

    /// @dev Checks whether given member has market membership
    /// @param accAddress Member address 
    /// @param market Market address   
    /// @return true if affiliated with market, false otherwise
    function hasMarketMembership (address accAddress, address market)  
        view 
        public
        onlyValidAddress(accAddress)
        onlyWhenMember(accAddress, true)
        onlyRegisteredPartner(market)
    returns(bool) {

        return _partnerMarkets._hasMembership(market, accAddress);
    }

    function requestMarketMembership (address market) 
        public 
        onlyMember
        onlyRegisteredPartner(market)
    returns(bool) {
        return _partnerMarkets._addMembership(market, msg.sender);
    }

    function revokeMarketMembership (address market) 
        public 
        onlyMember
        onlyRegisteredPartner(market)
    returns(bool) {
        return _partnerMarkets._revokeMembership(market, msg.sender);
    }

    ///@dev Used for a revokation of membership 
    ///@return Membership revocation status
    function triggerMembershipRevocation(address accAddress) 
        external 
        onlyValidAddress(accAddress) 
        onlyWhenInitialized
        onlyMember
    returns (bool) {
        require(owner != accAddress);

        require(_revokeMembership(accAddress));

        require(_revokeAllMarketMembership(accAddress));

        return true;
    } 

    function _revokeAllMarketMembership (address accAddress) 
        internal 
    returns(bool) {
        _partnerMarkets._removeAllMemberships(accAddress);

        return true;
    }
}