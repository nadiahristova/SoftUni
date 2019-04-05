pragma solidity >=0.5.6 <0.6.0;

import "./Ownable.sol";
import "./BaseContract.sol";
import "./Initializable.sol";

import "../libraries/VotingMemberBaseLib.sol";

import "../interfaces/MemberBaseInterface.sol";

// TODO Check if LIb can detect variables in base contract

contract VotingMemberBase is BaseContract, Ownable, Initializable, MemberBaseInterface {
    
    using VotingMemberBaseLib for VotingMemberBaseLib.Members;

    VotingMemberBaseLib.Members _memberBase;

    mapping (address => bool) _requestsForMembership;
    mapping (uint => CampaignDetails) _availableCampaigns;

    uint constant MAX_VOTES_PER_CAMPAIGN = 125;
    uint constant INITIAL_VOTE_WEIGHT = 0;

    struct CampaignDetails {
        bytes32 name;
        uint activeTimespan; // secs
    }

    uint _decisiveVoteWeightProportion;
    uint _decisiveVoteCountProportion;

    event LogMemberRegistration(address indexed accAddress);
    event LogMemberLeaving(address indexed accAddress);
    event LogMemberReinstatement(address indexed accAddress);
    event LogMemberRequestingMembershipCancelation(address indexed accAddress);

    event LogVotingCampaignRevoked(address indexed revokedBy, address indexed accAddress, uint campaignId);
    event LogOpenedNewVotingCampaign(address indexed accAddress, uint campaignId);
    event PropositionAccepted(address indexed accAddress, uint campaignId);
    event LogVotingCampaignSupported(address indexed supporter, address indexed supported);

    event LogMembershipRequested(address indexed accAddress);
    event LogMembershipRequestPending(address indexed accAddress);

    modifier onlyMember() {
        require(_memberBase._isMember(msg.sender), '3');
        _;
    }

    modifier onlyWhenMember (address accAddress, bool isMember) {
        require(_memberBase._isMember(accAddress) == isMember, '3');
        _;
    }

    modifier onlyWhenNoActiveCampaignsForMember(address accAddress) {
        require(!_memberBase._hasActiveCampaigns(accAddress), 'Active campaign');
        _;
    }

    function _initialize (uint[2] memory campaignTimePeriods, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint initialOwnerVoteWeight) 
        internal {

        assert(_memberBase._registerMember(msg.sender, initialOwnerVoteWeight));

        // set Default Campaigns
        _availableCampaigns[1].name = 0x4772616e744d656d626572736869700000000000000000000000000000000000; // GrantMembership
        _availableCampaigns[1].activeTimespan = campaignTimePeriods[0];
        _availableCampaigns[2].name = 0x5265766f6b654d656d6265727368697000000000000000000000000000000000; // RevokeMembership
        _availableCampaigns[2].activeTimespan = campaignTimePeriods[1];

        _decisiveVoteWeightProportion = decisiveVoteWeightProportion;
        _decisiveVoteCountProportion = decisiveVoteCountProportion;

        super._initialize();
    }

    function requestMembership () external onlyWhenMember(msg.sender, false) onlyWhenInitialized returns(bool) {
        address requester = msg.sender;

        emit LogMembershipRequested(requester);

        require(!_requestsForMembership[requester]);

        _requestsForMembership[requester] = true;

        emit LogMembershipRequestPending(requester);

        return true;
    }

    /// @dev Member can launch Membershi Granting Campaign
    /// @notice Only member can trigger the campaign, an account needs to request membership first
    /// @param accAddress Account address 
    function launchMembershipGrantingCampaign (address accAddress) 
        public 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyMember 
        onlyWhenMember(accAddress, false)
    returns(bool) {

        require(_requestsForMembership[accAddress], 'Not requested');

        delete _requestsForMembership[accAddress];
        
        return _memberBase._setVotingCampaign(1, accAddress, now + _availableCampaigns[1].activeTimespan);
    }

    function hasActiveCampaigns (address accaddress) view public returns(bool) {
        return _memberBase._hasActiveCampaigns(accaddress);
    }

    function removeVotingCampaign(address accAddress, uint campaignId) 
        public 
        onlyValidAddress(accAddress)
        onlyNaturalNumber(campaignId)
        onlyWhenInitialized
        onlyValidAddress(accAddress)
        onlyOwner 
        onlyWhenMember(accAddress, true)
    returns (bool) {
        require(campaignId > 2);

        return _resetVotingCampaingn(campaignId, accAddress);
    }

    ///@dev Checkes whether an account is a member of the member base
    ///@param accAddress Account address
    function isMember(address accAddress)
        view
        public
        onlyWhenInitialized
        onlyValidAddress(accAddress) 
    returns (bool) {
        return _memberBase._isMember(accAddress);
    }


    // what will happen with the markets
    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function revokeMembership() 
        external 
        onlyWhenInitialized
        onlyMember
        onlyWhenNoActiveCampaignsForMember(msg.sender)
    returns (MemberBaseLib.RevokeMembershipStatus) {
        require(owner != msg.sender);

        return _memberBase._revokeMembership(msg.sender);
    } 

    ///@dev Used for a revokation of membership of a member that has been voted out
    ///@return 
    function triggerMembershipRevocation(address accAddress) 
        external 
        onlyValidAddress(accAddress) 
        onlyWhenInitialized
        onlyMember
    returns (bool) {
        require(owner != accAddress);

        require(_revokeMembership(accAddress));

        require(_resetVotingCampaingn(2, accAddress));
    } 

    function getVoteWeight (address accAddress) 
        view 
        external 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyMember
        returns(uint) {

        return _memberBase._getVoteWeight(accAddress);
    }

    ///@dev Add new member to a member base if voted
    function _registerMember(address accAddress) 
        internal 
    {
        require(_memberBase._isCampaignSupported(1, accAddress), "Not voted");
        
        require(_memberBase._registerMember(accAddress, INITIAL_VOTE_WEIGHT));

        require(_resetVotingCampaingn(1, accAddress));
    }
    
    /// @dev Launches not basic campaign for given member
    /// @notice Only owner can do this, campaign id should be > 2
    /// @param accAddress Member address
    /// @return true if campaign was launched, false otherwise
    function _launchCampaign (address accAddress, uint campaignId) 
        internal 
    returns(bool) {

        return _memberBase._setVotingCampaign(campaignId, accAddress, now + _availableCampaigns[campaignId].activeTimespan);
    }

    ///@dev Used for a foreceful revokation of membership 
    ///@return Membership revocation status
    function _revokeMembership(address accAddress) 
        internal 
    returns (bool) {
        if(_memberBase._isCampaignSupported(2, accAddress)) {
            return _memberBase._revokeMembershipForced(accAddress);
        }

        return false;
    } 

    function _resetVotingCampaingn(uint campaignId, address accAddress) 
        internal 
    returns (bool) {
        return _memberBase._resetVotingCampaingn(campaignId, accAddress);
    } 

    function _supportMember( // support campaign
            address accAddress,
            uint248 votingCampaignId)
        internal 
    {
        require(_memberBase._supportMember(msg.sender, votingCampaignId, accAddress, MAX_VOTES_PER_CAMPAIGN, 
            _decisiveVoteWeightProportion, _decisiveVoteCountProportion));
    } 


    function _upMemberVoteWeight (address accAddress, uint weight) internal {
        require(_memberBase._updateMemberVoteWeight(accAddress, weight, true));
    }

    function _downMemberVoteWeight (address accAddress, uint weight) internal {
        require(_memberBase._updateMemberVoteWeight(accAddress, weight, false));
    }
}