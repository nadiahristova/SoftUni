pragma solidity >=0.5.6 <0.6.0;

import "./Pausable.sol";
import "./VotingMemberBase.sol";

import "../interfaces/MarketMemberBaseInterface.sol";

import "../libraries/PartnerRelationsKeeperLib.sol";


contract MarketMemberBase is Pausable, VotingMemberBase, MarketMemberBaseInterface {
    
    using PartnerRelationsKeeperLib for PartnerRelationsKeeperLib.Partners;

    PartnerRelationsKeeperLib.Partners _partnerMarkets;

    event LogPartnerEntityAdded(address indexed partnerAddress);
    event LogPartnerEntityRemoved(address indexed partnerAddress);
    event LogAffiliationWithPartner(address indexed accAddress, address indexed partnerAddress);
    event LogAffiliationWithPartnerRevoked(address indexed accAddress, address indexed partnerAddress);

    
    modifier onlyOnValidMarketMembership(address shopOwner, address market) {
        require(hasMarketMembership(shopOwner, market), '20');
        _;
    }

    modifier onlyRegisteredPartner(address market) {
        require(isPartner(market), '5');
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

    /// @dev Add market partner
    /// @param market Address of market partner
    function addMarketPartner (address market) 
        external 
        onlyValidAddress(market)
        onlyWhenInitialized
        onlyOwner {
            
        require(!_partnerMarkets._isRegisteredPartner(market), '4');

        require(_partnerMarkets._addPartner(market));
    }

    /// @dev Remove market partner
    /// @notice Only owner can remove market partner
    /// @param market Address of market partner
    function removeMarketPartner (address market) 
        external 
        onlyValidAddress(market)
        onlyOwner
        onlyWhenInitialized
        onlyRegisteredPartner(market) 
    returns(bool) {

        return _partnerMarkets._removePartner(market);
    }

    /// @dev Resets active voting campaign for given user
    /// @notice Only owner can trigger this function
    /// @param accAddress Account Address
    /// @param campaignId Id of the campaign
    function removeVotingCampaign(address accAddress, uint campaignId) 
        external 
        onlyNaturalNumber(campaignId)
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyOwner 
        onlyWhenMember(accAddress, true)
    {
        require(campaignId > 2);

        VotingMemberBase._resetVotingCampaingn(campaignId, accAddress);
    }

    /// @dev Checks whether given market is a partner
    /// @param market Market contract address
    /// @return True if partner, false otherwise
    function isPartner (address market) 
        view
        public 
        onlyValidAddress(market)
        onlyWhenInitialized
    returns(bool) {

        return _partnerMarkets._isRegisteredPartner(market);
    }

    /// @dev Gets fixed array of market partner contract addresses
    /// @notice Max num of partner contracts is 25
    /// @return Market partner contract addresses
    function getMarketPartners () 
        view
        external 
        onlyWhenInitialized
    returns(address[25] memory) {

        return _partnerMarkets._getPartners();
    }

    /// @dev Checks whether given member has market membership
    /// @param accAddress Member address 
    /// @param market Market address   
    /// @return True if affiliated with market, false otherwise
    function hasMarketMembership (address accAddress, address market)  
        view 
        public
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyWhenMember(accAddress, true)
        onlyRegisteredPartner(market)
    returns(bool) {

        return _partnerMarkets._hasMembership(market, accAddress);
    }

    /// @dev Request market membership
    /// @param market The address of market
    function requestMarketMembership (address market) 
        external 
        onlyValidAddress(market)
        onlyWhenInitialized
        onlyMember
        onlyRegisteredPartner(market) {

        _partnerMarkets._addMembership(market, msg.sender);
    }

    /// @dev Revoke market membership
    /// @param market The address of market
    function revokeMarketMembership (address market) 
        external 
        onlyValidAddress(market)
        onlyWhenInitialized
        onlyMember
        onlyRegisteredPartner(market) {

        _partnerMarkets._revokeMembership(market, msg.sender);
    }

    ///@dev Used for a revokation of membership 
    ///@param accAddress Account address of member
    ///@return Membership revocation status
    function triggerMembershipRevocation(address accAddress) 
        external 
        onlyValidAddress(accAddress) 
        onlyWhenInitialized
        onlyMember {
        require(owner != accAddress);

        VotingMemberBase._revokeMembership(accAddress);

        _revokeAllMarketMembership(accAddress);
    } 

    function _revokeAllMarketMembership (address accAddress) 
        internal 
    returns(bool) {
        _partnerMarkets._removeAllMemberships(accAddress);

        return true;
    }
}