pragma solidity >=0.5.6 <0.6.0;

import "./Ownable.sol";
import "./BaseContract.sol";
import "./Initializable.sol";

import "../libraries/MarketPartnerLib.sol";
import "../libraries/VotingMemberBaseLib.sol";

import "../interfaces/MemberBaseInterface.sol";

// TODO Check if LIb can detect variables in base contract

contract VotingMemberBase is BaseContract, Ownable, Initializable, MemberBaseInterface {
    
    using VotingMemberBaseLib for VotingMemberBaseLib.Members;

    VotingMemberBaseLib.Members _memberBase;

    mapping (address => bool) _requestsForMembership;
    mapping (uint => CampaignDetails) _availableCampaigns;

    uint constant MAX_VOTES_PER_CAMPAIGN = 125;

    struct CampaignDetails {
        bytes32 name;
        uint activeTimespan; // secs
    }

    uint _decisiveVoteWeightProportion;
    uint _decisiveVoteCountProportion;

    event LogMemberRegistration(address indexed accAddress);
    event LogMemberLeaving(address indexed exMemberAddress);
    event LogMemberReinstatement(address indexed accAddress);
    event LogMemberRequestingMembershipCancelation(address indexed accAddress);

    event LogVotingCampaignRevoked(address indexed revokedBy, address indexed accAddress, uint campaignId);
    event LogOpenedNewVotingCampaign(address indexed accAddress, uint campaignId);
    event PropositionAccepted(address indexed accAddress, uint campaignId);
    event LogVotingCampaignSupported(address indexed supporter, address indexed supported);

    event LogMembershipRequested(address indexed accAddress);
    event LogMembershipRequestPending(address indexed accAddress);

    modifier onlyMember() {
        require(_memberBase._isMember(msg.sender));
        _;
    }

    modifier onlyWhenNoActiveCampaigns(address accAddress) {
        require(!_memberBase._hasActiveCampaigns(accAddress));
        _;
    }

    function initialize (uint[2] memory campaignTimePeriods, uint decisiveVoteWeightProportion, uint decisiveVoteCountProportion) onlyOwner public returns(bool) {
        require(!_isInitialized);

        _memberBase._registerMember(msg.sender, 50);

        // set Default Campaigns
        _availableCampaigns[1].name = 0x4772616e744d656d626572736869700000000000000000000000000000000000; // GrantMembership
        _availableCampaigns[1].activeTimespan = campaignTimePeriods[0];
        _availableCampaigns[2].name = 0x5265766f6b654d656d6265727368697000000000000000000000000000000000; // RevokeMembership
        _availableCampaigns[2].activeTimespan = campaignTimePeriods[1];

        _decisiveVoteWeightProportion = decisiveVoteWeightProportion;
        _decisiveVoteCountProportion = decisiveVoteCountProportion;

        super.initialize();
    }

    function requestMembership () public {
        address requester = msg.sender;

        emit LogMembershipRequested(requester);

        require(!isMember(requester));
        require(!_requestsForMembership[requester]);

        emit LogMembershipRequestPending(requester);
    }

    function launchMembershipGrantingCampaign (address accAddress) 
        public 
        onlyValidAddress(accAddress)
        onlyMember 
    returns(bool) {
        require(!isMember(accAddress));

        require(_requestsForMembership[accAddress]);

        delete _requestsForMembership[accAddress];
        
        return _memberBase._setVotingCampaign(1, accAddress, now + _availableCampaigns[1].activeTimespan);
    }

    function _hasActiveCampaigns (address accaddress) view public returns(bool) {
        return _memberBase._hasActiveCampaigns(accaddress);
    }

    function launchCampaign (address accAddress, uint campaignId) 
        public 
        onlyOwner 
        onlyValidAddress(accAddress)
    returns(bool) {
        require(campaignId > 2); // Grant and Revoke Membership campaigns are not accessible here
        require(_availableCampaigns[campaignId].name != 0x0); // campaign is existing
        require(isMember(accAddress));

        return _memberBase._setVotingCampaign(campaignId, accAddress, now + _availableCampaigns[campaignId].activeTimespan);
    }

    function removeVotingCampaign(address accAddress, uint campaignId) 
        public 
        onlyValidAddress(accAddress)
        onlyOwner 
    returns (bool) {
        require(campaignId > 2);
        require(accAddress == owner || accAddress == msg.sender);

        return _memberBase._resetVotingCampaingn(campaignId, accAddress);
    }

    ///@dev Checkes whether an account is a member to a member base
    ///@param accAddress The address of the account
    function isMember(address accAddress) public view onlyValidAddress(accAddress) returns (bool) {
        return _memberBase._isMember(accAddress);
    }

    ///@dev Add new member to a member base
    function registerMember(address accAddress) public onlyMember onlyValidAddress(accAddress) returns (bool) {

        if(_memberBase._isCampaignSupported(1, accAddress)) {
            if(_memberBase._registerMember(accAddress, 0)) {
                if(_memberBase._resetVotingCampaingn(1, accAddress)) {
                    return true;
                }
            }
        }

        return false;
    }

    // what will happen with the markets
    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function revokeMembership() 
        public 
        onlyMember
        onlyWhenNoActiveCampaigns(msg.sender)
    returns (MemberBaseLib.RevokeMembershipStatus) {
        return _memberBase._revokeMembership(msg.sender);
    } 

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function _revokeMembership(address accAddress) 
        internal 
        onlyValidAddress(accAddress)
    returns (bool) {
        if(_memberBase._isCampaignSupported(2, accAddress)) {
            return _memberBase._revokeMembershipForced(accAddress);
        }

        return false;
    } 

    function supportMember( 
            address accAddress,
            uint248 votingCampaignId)
        external 
        onlyValidAddress(accAddress) //TODO
    {
        require(_memberBase._supportMember(msg.sender, votingCampaignId, accAddress, MAX_VOTES_PER_CAMPAIGN, _decisiveVoteWeightProportion, _decisiveVoteCountProportion));
    } 

    function upMemberVoteWeight (address accAddress, uint weight) internal {
        require(_memberBase._updateMemberVoteWeight(accAddress, weight, true));
    }

    function downMemberVoteWeight (address accAddress, uint weight) internal {
        require(_memberBase._updateMemberVoteWeight(accAddress, weight, false));
    }
}