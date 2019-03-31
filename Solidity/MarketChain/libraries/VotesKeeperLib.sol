pragma solidity >=0.5.6 <0.6.0;

import "../libraries/SafeMath.sol";

library VotesKeeperLib {
    using SafeMath for uint;

    struct VoteCampain {
        uint248 campaignId;
        bool isOngoing;
        uint256 gatheredVotesWeight;
        uint256 validUntil;
        // TODO: Fix this add supp count
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
            require(votingCampaignId != 0);
            require(self.campains[accAddress].campaignId == votingCampaignId);
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

    function _isCampaignSupported (
            VotesKeeper storage self,
            address accAddress,
            uint votingCampaignId) 
        view
        internal 
        onlyOngoingCampaign(self, accAddress, votingCampaignId)
    returns(bool) {
        return self.campains[accAddress].isOngoing;
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

        self.campains[accAddress].campaignId = uint248(votingCampaignId);
        self.campains[accAddress].isOngoing = true;
        self.campains[accAddress].validUntil = validUntil;

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

        if (self.campains[accAddress].isOngoing) {
            _resetCampaign(self, accAddress);

            emit LogVotingCampaignRevoked(msg.sender, accAddress, votingCampaignId);
        } else {

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
        require(supporter != accAddress);

        require((votesWeightProportion > 0 && votesWeightProportion < 1000)
                || (votesMemberProportion > 0 && votesMemberProportion < 1000));

        uint importance = self.voteWeightMap[supporter];

        if(importance > 0 ||
            _isCampaignSupported(self, accAddress, votingCampaignId) || 
            !self.campains[accAddress].supportersTracker[supporter]) {
                return false;
        }
        
        uint currentVotesWeight = self.campains[accAddress].gatheredVotesWeight.add(importance);
        uint currentNomOfSupporters = self.campains[accAddress].supporters.length;

        require(currentNomOfSupporters <= maxNumOfVoters);
        
        emit LogVotingCampaignSupported(supporter, accAddress);
        
        if((votesWeightProportion != 0 && currentVotesWeight >= self.overallVotesWeight.div(votesWeightProportion)) 
            || (votesMemberProportion != 0 && self.campains[accAddress].supporters.length >= self.overallVotesWeight.div(votesMemberProportion))) {

            _resetCampaign(self, accAddress);

            self.campains[accAddress].campaignId = votingCampaignId;
            
            emit PropositionAccepted(accAddress, votingCampaignId);
        } 
        else{
            self.campains[accAddress].gatheredVotesWeight = uint248(currentVotesWeight);
            self.campains[accAddress].supportersTracker[supporter] = true;
            
            self.campains[accAddress].supporters.push(supporter);
        }

        return true;
    }
    
    // Fix this
    function _resetCampaign(VotesKeeper storage self, address accAddress) private {
        delete self.campains[accAddress];

        address[] memory supporters = self.campains[accAddress].supporters;
        
        for (uint i = 0; i < supporters.length; i++) {
            delete self.campains[accAddress].supportersTracker[supporters[i]];
        }
        
        delete self.campains[accAddress].supporters;
    }
}