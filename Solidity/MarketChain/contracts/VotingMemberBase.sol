pragma solidity >=0.5.6 <0.6.0;

import "./Ownable.sol";
import "./BaseContract.sol";
import "./Initializable.sol";

import "../libraries/VotingMemberBaseLib.sol";

import "../interfaces/MemberBaseInterface.sol";

contract VotingMemberBase is BaseContract, Ownable, Initializable, MemberBaseInterface {
    
    using VotingMemberBaseLib for VotingMemberBaseLib.Members;

    //denominator used for determining how much of overall vote weight is needed for a given campaign to be successful
    uint _decisiveVoteWeightProportion;
    //denominator used for determining how much of overall supporters member count is needed for a given campaign to be successful
    uint _decisiveVoteCountProportion;
    
    mapping (address => bool) _requestsForMembership;
    // campaign id to campaign
    mapping (uint => CampaignDetails) _availableCampaigns;

    VotingMemberBaseLib.Members _memberBase;


    uint constant MAX_VOTES_PER_CAMPAIGN = 125;
    uint constant INITIAL_VOTE_WEIGHT = 0;

    struct CampaignDetails {
        bytes32 name;
        uint activeTimespan; // secs
    }

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

        Initializable._initialize();
    }

    /// @dev Account requests membeship
    /// @return true if Bugs will eat it, false otherwise
    function requestMembership () 
        external 
        onlyWhenInitialized 
        onlyWhenMember(msg.sender, false) 
    {
        address requester = msg.sender;

        emit LogMembershipRequested(requester);

        require(!_requestsForMembership[requester]);

        _requestsForMembership[requester] = true;

        emit LogMembershipRequestPending(requester);
    }

    /// @dev Member can launch Membershi Granting Campaign
    /// @notice Only member can trigger the campaign, an account needs to request membership first
    /// @param accAddress Account address 
    function launchMembershipGrantingCampaign (address accAddress) 
        external 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyMember 
        onlyWhenMember(accAddress, false)
    returns(bool) {

        require(_requestsForMembership[accAddress], 'Not requested');

        delete _requestsForMembership[accAddress];
        
        return _memberBase._setVotingCampaign(1, accAddress, now + _availableCampaigns[1].activeTimespan);
    }

    /// @dev Checks whether given member has active campaigns
    /// @param accaddress The name of a food to evaluate (English)
    /// @return True if a campaign is in process, false otherwise
    function hasActiveCampaigns (address accaddress) view public onlyWhenInitialized returns(bool) {
        return _memberBase._hasActiveCampaigns(accaddress);
    }

    /// @dev Checkes whether an account is a member of the member base
    /// @param accAddress Account address
    /// @return True if is member, false otherwise
    function isMember(address accAddress)
        view
        public
        onlyValidAddress(accAddress) 
        onlyWhenInitialized
    returns (bool) {
        return _memberBase._isMember(accAddress);
    }

    function transferOwnership(address newOwner) 
        public 
        onlyValidAddress(newOwner) 
        onlyWhenMember(newOwner, true) {

        Ownable.transferOwnership(newOwner);
    }

    ///@dev Revoke membership
    ///@notice Used by the member him/herself
    ///@return Membership revocation status
    function revokeMembership() 
        external 
        onlyWhenInitialized
        onlyMember
        onlyWhenNoActiveCampaignsForMember(msg.sender)
    returns (MemberBaseLib.RevokeMembershipStatus) {
        require(Ownable.owner != msg.sender, '16');

        return _memberBase._revokeMembership(msg.sender);
    } 

    ///@dev Used to finalize membership revocation
    ///@param accAddress Member account address
    ///@notice Can be invoked by any member
    function triggerMembershipRevocation(address accAddress) 
        external 
        onlyValidAddress(accAddress) 
        onlyWhenInitialized
        onlyMember
    {
        require(Ownable.owner != accAddress);

        require(VotingMemberBase._revokeMembership(accAddress));

        require(VotingMemberBase._resetVotingCampaingn(2, accAddress));
    } 

    /// @dev Returns the vote weight of member
    /// @param accAddress Member account address
    /// @return Vote weight in the organisation   
    function getVoteWeight (address accAddress) 
        view 
        external 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyMember
    returns(uint) {

        return _memberBase._getVoteWeight(accAddress);
    }

    function _getMembershipInfo(address accAddress) internal view returns (bool, bool){

        if (!_memberBase._isMember(accAddress)) {
            return (false, false);
        } else if(Ownable.owner == accAddress) {
            return (true, true);
        }

        return (true, false);
    }

    ///@dev Add new member to a member base if voted
    function _registerMember(address accAddress) 
        internal 
    {
        require(_memberBase._isCampaignSupported(1, accAddress), "Not voted");
        
        require(_memberBase._registerMember(accAddress, INITIAL_VOTE_WEIGHT));

        require(VotingMemberBase._resetVotingCampaingn(1, accAddress));
    }
    
    function _launchCampaign (address accAddress, uint campaignId) 
        internal 
    returns(bool) {

        return _memberBase._setVotingCampaign(campaignId, accAddress, now + _availableCampaigns[campaignId].activeTimespan);
    }

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

    // increase vote weight by given amouont
    function _upMemberVoteWeight (address accAddress, uint weight) internal {
        require(_memberBase._updateMemberVoteWeight(accAddress, weight, true));
    }

    // decrease vote weight by given amouont
    function _downMemberVoteWeight (address accAddress, uint weight) internal {
        require(_memberBase._updateMemberVoteWeight(accAddress, weight, false));
    }
}