pragma solidity >=0.5.6 <0.6.0;

import "../libraries/SafeMath.sol";

library VotesKeeperLib {
    using SafeMath for uint;

    struct VoteCampain {
        uint248 campaignId;
        bool inProgress;
        uint256 gatheredVotesWeight;
        uint256 validUntil;
        mapping(address => bool) supportersTracker;
        address[] supporters;
    }
    
    struct VotesKeeper {
        uint256 overallMemberCount;
        uint256 overallVotesWeight;

        mapping(address => uint256) activeCampaignsCount;
        mapping(address => VoteCampain) campains;
        mapping(address => uint256) voteWeightMap;
    } 
    
    event LogVotingCampaignRevoked(address indexed revokedBy, address indexed accAddress, uint campaignId);
    event LogOpenedNewVotingCampaign(address indexed accAddress, uint campaignId);
    event PropositionAccepted(address indexed accAddress, uint campaignId);
    event LogVotingCampaignSupported(address indexed supporter, address indexed supported);
    
    modifier onlyOngoingCampaign (
        VotesKeeper storage self,
        address accAddress,
        uint votingCampaignId) {

        require(votingCampaignId != 0, "Campaign not set");
        require(self.campains[accAddress].campaignId == votingCampaignId); // not requested campaign
        _;
    }

    function _hasActiveCampaigns (VotesKeeper storage self, address accAddress) 
        view
        internal 
    returns (bool) {
        return self.activeCampaignsCount[accAddress] != 0;
    }

    function _updateOverallVotesWeight(VotesKeeper storage self, uint weight, bool increase) 
        internal {
        if(increase) {
            self.overallVotesWeight = self.overallVotesWeight.add(weight);
        } else {
            self.overallVotesWeight = self.overallVotesWeight.sub(weight);
        }
    }

    function _updateMemberVoteWeight(
            VotesKeeper storage self, 
            address accAddress,
            uint weight, 
            bool increase) 
        internal {
        if(increase) {
            self.voteWeightMap[accAddress] = self.voteWeightMap[accAddress].add(weight);
        } else {
            self.voteWeightMap[accAddress] = self.voteWeightMap[accAddress].sub(weight);
        }
    }

    function _updateOverallMemberCount(VotesKeeper storage self, bool increase) internal {
        if(increase) {
            self.overallMemberCount = self.overallMemberCount.add(1);
        } else {
            self.overallMemberCount = self.overallMemberCount.sub(1);
        }
    }

    /// Checks whether the voting had finished and the campaign had been supported
    /// It had finished when id != 0 and inProgress == false
    function _isCampaignSupported (
            VotesKeeper storage self,
            address accAddress,
            uint votingCampaignId) 
        view
        internal 
        onlyOngoingCampaign(self, accAddress, votingCampaignId)
    returns(bool) {
        return !self.campains[accAddress].inProgress;
    }

    function _setVotingCampaign (
            VotesKeeper storage self,
            address accAddress, 
            uint votingCampaignId, 
            uint validUntil) 
        internal 
    returns (bool) {

        if(self.campains[accAddress].campaignId != 0) {
            return false;
        }    

        VoteCampain storage campaign = self.campains[accAddress];

        campaign.campaignId = uint248(votingCampaignId);
        campaign.inProgress = true;
        campaign.validUntil = validUntil;

        self.activeCampaignsCount[accAddress]++;

        emit LogOpenedNewVotingCampaign(accAddress, votingCampaignId);

        return true;
    }

    function _resetVotingCampaingn (
            VotesKeeper storage self,
            address accAddress, 
            uint votingCampaignId) 
        internal 
        onlyOngoingCampaign(self, accAddress, votingCampaignId)
    returns (bool) {

        if (self.campains[accAddress].inProgress) {
            _resetCampaign(self, accAddress);

            emit LogVotingCampaignRevoked(msg.sender, accAddress, votingCampaignId);
        } else {
            // other fields had been cleared when the goal of the campaing had been reached
            delete self.campains[accAddress].campaignId;
        }

        uint256 activeCampaignsCount = self.activeCampaignsCount[accAddress];
        assert(--activeCampaignsCount >= 0);

        self.activeCampaignsCount[accAddress] = activeCampaignsCount;

        return true;
    }
    
    function _supportMember (
            VotesKeeper storage self, 
            address supporter, 
            uint248 votingCampaignId,
            address accAddress, 
            uint maxNumOfVoters,
            uint votesWeightProportion,
            uint votesMemberProportion)
        internal 
    returns (bool) { 
        require((votesWeightProportion > 0 && votesWeightProportion < 1000)
                || (votesMemberProportion > 0 && votesMemberProportion < 1000));

        uint importance = self.voteWeightMap[supporter];

        VoteCampain storage campaign = self.campains[accAddress];

        if(//importance == 0 ||
            campaign.supportersTracker[supporter] ||
            _isCampaignSupported(self, accAddress, votingCampaignId)) {
                return false;
        }

        uint currentVotesWeight = campaign.gatheredVotesWeight.add(importance);
        uint currentNomOfSupporters = campaign.supporters.length;

        require(currentNomOfSupporters <= maxNumOfVoters);
        
        emit LogVotingCampaignSupported(supporter, accAddress);
        
        if((votesWeightProportion != 0 && currentVotesWeight >= self.overallVotesWeight.div(votesWeightProportion)) 
            || (votesMemberProportion != 0 && campaign.supporters.length >= self.overallMemberCount.div(votesMemberProportion))) {
            // goal had been reached free up storage
            _resetCampaign(self, accAddress);

            campaign.campaignId = votingCampaignId;
            
            emit PropositionAccepted(accAddress, votingCampaignId);
        } 
        else{
            campaign.gatheredVotesWeight = uint248(currentVotesWeight);
            campaign.supportersTracker[supporter] = true;
            
            campaign.supporters.push(supporter);
        }

        return true;
    }
    
    function _resetCampaign(VotesKeeper storage self, address accAddress) private {
        delete self.campains[accAddress];

        address[] memory supporters = self.campains[accAddress].supporters;
        
        for (uint i = 0; i < supporters.length; i++) {
            delete self.campains[accAddress].supportersTracker[supporters[i]];
        }
        
        delete self.campains[accAddress].supporters;
    }
}