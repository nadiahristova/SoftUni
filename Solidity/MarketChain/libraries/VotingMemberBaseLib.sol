pragma solidity >=0.5.6 <0.6.0;

import "../libraries/MemberBaseLib.sol";
import "../libraries/VotesKeeperLib.sol";

// example of combining existing libraries, should be MemberBase with MemberBaseLib <= VotingMemberBase with VotesKeeperLib
library VotingMemberBaseLib {
    using MemberBaseLib for MemberBaseLib.Members;
    using VotesKeeperLib for VotesKeeperLib.VotesKeeper;

    struct Members {
        MemberBaseLib.Members _members;
        VotesKeeperLib.VotesKeeper _votes;
        
        address[15] pendingMembershipRequests;
    }

    ///@dev Checkes whether an account is a member to a member base
    ///@param accAddress The address of the account
    function _isMember(Members storage self, address accAddress) 
        view
        internal 
    returns (bool) {
        return self._members._isMember(accAddress);
    }

    ///@dev Add new member to a member base
    function _registerMember(Members storage self, address candidateMember, uint voteWeight) internal returns(bool) {
        if(self._members._registerMember(candidateMember)) {
            self._votes._updateOverallMemberCount(true);

            if(voteWeight > 0) { 
                self._votes._updateOverallVotesWeight(voteWeight, true);
            }

            return true;
        }

        return false;
    }

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function _revokeMembership(Members storage self, address accAddress) 
        internal 
    returns (MemberBaseLib.RevokeMembershipStatus) {

        MemberBaseLib.RevokeMembershipStatus status = self._members._revokeMembership(accAddress);

        if(status == MemberBaseLib.RevokeMembershipStatus.Removed){
            self._votes._updateOverallMemberCount(false);

            uint memberVoteWeight =  self._votes.voteWeightMap[accAddress];

            if(memberVoteWeight != 0) {
                self._votes._updateOverallVotesWeight(memberVoteWeight, false);
            }

            delete self._votes.voteWeightMap[accAddress];
        }

        return status;
    } 

    ///@dev Used for forced removal of membership  
    function _revokeMembershipForced(Members storage self, address accAddress) 
        internal 
    returns (bool) {
        return self._members._revokeMembershipForced(accAddress);
    } 

    /** VOTING REGION */

    function _updateOverallVotesWeight (
            Members storage self, 
            uint weight, 
            bool increase)
        internal 
        returns (bool) {

        self._votes._updateOverallVotesWeight(weight, increase);

        return true;
    }
    
    function _updateOverallMemberCount (Members storage self, bool increase) private returns (bool) {
        self._votes._updateOverallMemberCount(increase);

        return true;
    }

    function _updateMemberVoteWeight (
        Members storage self, 
            address accAddress,
            uint weight, 
            bool increase
        ) internal 
    returns (bool) {
        self._votes._updateMemberVoteWeight(accAddress, weight, increase);

        return true;
    }

    function _isCampaignSupported (
            Members storage self, 
            uint votingCampaignId, 
            address accAddress) 
        view
        internal 
    returns (bool) {
        return self._votes._isCampaignSupported(accAddress, votingCampaignId);
    } 

    function _hasActiveCampaigns (
            Members storage self,
            address accAddress) 
        view
        internal 
    returns (bool) {
        return self._votes._hasActiveCampaigns(accAddress);
    }

    function _supportMember(
            Members storage self, 
            address supporter, 
            uint248 votingCampaignId,
            address accAddress,
            uint maxNumOfVoters,
            uint votesWeightProportion,
            uint votesMemberProportion) 
        internal 
    returns (bool) {
        return self._votes._supportMember(supporter, votingCampaignId, accAddress, maxNumOfVoters, 
            votesWeightProportion, votesMemberProportion); 
    } 

    function _resetVotingCampaingn(
            Members storage self, 
            uint votingCampaignId,
            address accAddress) 
        internal 
    returns (bool) {
        return self._votes._resetVotingCampaingn(accAddress, uint248(votingCampaignId));
    } 

    function _setVotingCampaign(
            Members storage self, 
            uint votingCampaignId, 
            address accAddress, 
            uint validUntil) 
        internal 
    returns (bool) {
        return self._votes._setVotingCampaign(accAddress, votingCampaignId, validUntil);
    } 
}