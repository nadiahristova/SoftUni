pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./BaseMarket.sol";
import "./AdministrableByRegion.sol";

// Under Construction
contract RegionalMarket is AdministrableByRegion, BaseMarket {

    // DonationRelevant info
    struct MemberInfo {
        Location location;
        uint248 lastlyWonFunding;
        bool isValidFundingCandidate;
    }

    struct Funding {
        uint created; // flag for activity status of Funding
        address[] candidates;
    }

    mapping(bytes32 => Funding) _fundings;

    mapping(address => MemberInfo) _additionalInfo; 
    mapping(bytes32 => uint) _fundsPerRegion; 

    uint256 _donationRoundTimePeriod;
    
    uint constant TIME_BETWEEN_WON_CANDIDACIES = 356 days;
    uint constant MAX_NUM_CANDIDATES_FOR_FUNDING = 15;

    
    event LogProfitRetrieved(address storeOwner);
    event LogSendFunding(address storeOwner, uint amount);
    event LogDonationDistributionFinished(bytes2 iSOCode, bytes30 province);
    event LogStoreOwnerSuggestedForFunding(address storeOwner, address admin, bytes2 iSOCode, bytes30 province);
    event LogFundingCampaignActivated(bytes2 iSOCode, bytes30 province);
    event LogRegisteredDonationForRegion(bytes2 iSOCode, bytes30 province, uint256 donation, uint256 overAllFunds);

    function initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint profit_fee, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint donationRoundTimePeriod, 
            uint initialOwnerVoteWeight) 
        public 
        onlyOwner
    {
        require(!_isInitialized, '1');

        require(donationRoundTimePeriod >= 30 days, 'Round period');
        _donationRoundTimePeriod = donationRoundTimePeriod;

        bytes32[] memory campaignNames = new bytes32[](1);
        campaignNames[0] = 0x446f6e6174696f6e000000000000000000000000000000000000000000000000; //Donation

        uint[] memory campaignTimePeriods = new uint[](1);
        campaignTimePeriods[0] = donationRoundTimePeriod;

        _initialize(defaultCampaignTimePeriods, profit_fee, decisiveVoteWeightProportion, decisiveVoteCountProportion, 
        initialOwnerVoteWeight, campaignNames, campaignTimePeriods);
    }

    ///@dev Candidate member should register him/herself
    ///@notice Province name should be between 1 and 30 letters
    ///@param location ISO Code of country and Province name
    function registerMember(Location memory location)
        public
        onlyWhenInitialized
        onlyValidLocation(location)
        onlyWhenMember(msg.sender, false)
    returns (bool) {

        address accAddress = msg.sender;

        _additionalInfo[accAddress].location = location;

        VotingMemberBase._registerMember(accAddress);

        return true;
    }
    
    function launchMembershipRevocationCampaign (address accAddress) 
        public 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyOwner 
        onlyWhenMember(accAddress, true)
    returns(bool) {

        return _launchCampaign(accAddress, 2);
    }

    /// @dev Opens store in the market environment
    /// @param producerBase address of the producer base where the actual store data is managed 
    /// @notice market should be affiliated with the producer base, 
    /// sender should be member of the producer base and the market
    function openStore (address producerBase, bytes32 name) 
        onlyMember
        onlyPartnerProducerBase(producerBase)
        onlyWhenStoreOwner(msg.sender, producerBase, false)
        external { 
        require(name != 0x0, '10'); // use name as a marker
        address storeOwner = msg.sender;

        BaseMarket._openStore(producerBase, storeOwner, name);
    }

    /// @dev Removes store front from the market
    /// @notice Can be done only by the store front owner
    /// @param memberBase address of the producer base affiliate
    /// @param storeFrontId id used for identifying store front in the environment of producer base affiliate
    function removeStoreFront (address memberBase, uint256 storeFrontId) 
        external
        onlyValidAddress(memberBase)
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyMember
        onlyWhenStoreOwner(msg.sender, memberBase, true)
        onlyWhenStoreFrontOwner(msg.sender, memberBase, storeFrontId, true)
    {

        BaseMarket._removeStoreFront(msg.sender, storeFrontId, memberBase);
    }  
    
    /// @dev Suggest given store owner for funding
    /// @param storeOwner producer account address
    /// @param memberBase address of the producer base the store owner is member of
    /// @notice Can be executed only by the admin. Store owner has to be member of producer base affiliate
    /// @return true if Bugs will eat it, false otherwise
    function suggestForFunding (address storeOwner, address memberBase) 
        external 
        onlyValidAddress(storeOwner)
        onlyValidAddress(memberBase)
        onlyWhenInitialized 
        onlyAdmin 
        onlyPartnerProducerBase(memberBase)
        onlyWhenStoreOwner(storeOwner, memberBase, true)
    {
        address admin = msg.sender;

        require(admin != storeOwner);

        MemberInfo memory info = _additionalInfo[storeOwner];
        bytes32 regionKey = _returnLocationKey(info.location);

        require(_fundings[regionKey].created != 0); // campaign is active

        require(_returnLocationKey(_additionalInfo[admin].location) == regionKey); // Admin should be of same region

        require(!info.isValidFundingCandidate);//already proposed
        require(info.lastlyWonFunding + TIME_BETWEEN_WON_CANDIDACIES <= now, 'Wait time');

        require(_fundings[regionKey].candidates.push(storeOwner) <= MAX_NUM_CANDIDATES_FOR_FUNDING, 'No spots');

        _additionalInfo[storeOwner].isValidFundingCandidate = true;

        _launchCampaign(storeOwner, 3);

        emit LogStoreOwnerSuggestedForFunding(storeOwner, admin, info.location.iSOCode, info.location.province);
    }


    function supportMember( // support campaign
            address storeOwner,
            uint248 votingCampaignId)
        external 
        onlyValidAddress(storeOwner) 
        onlyNaturalNumber(votingCampaignId)
        onlyWhenInitialized
        onlyMember
    {
        address supporter = msg.sender;

        require(supporter != storeOwner);

        if(votingCampaignId == 3) {

            MemberInfo memory info = _additionalInfo[storeOwner];
            bytes32 regionKey = _returnLocationKey(info.location);
            
            require(_fundings[regionKey].created != 0); // campaign is active

            require(_returnLocationKey(_additionalInfo[supporter].location) != regionKey); // Supporter and supported should be from different regions

            require(info.isValidFundingCandidate);//proposed by admin
        }

        _supportMember(storeOwner, votingCampaignId);
    } 

    ///@dev Used for a foreceful revokation of membership 
    ///@return Membership revocation status
    function triggerFundingDistribution(Location memory location) 
        public 
        onlyValidLocation(location)
        onlyWhenInitialized
    returns (bool) {
        
        bytes32 regionKey = _returnLocationKey(location);

        require(_fundings[regionKey].created != 0);
        require(_fundsPerRegion[regionKey] > 1000); // not enough funds donated

        Funding memory funding = _fundings[regionKey];

        require(funding.created + _donationRoundTimePeriod <= now, 'Wait time'); // Funding campaign had finished

        uint candidateCount = _fundings[regionKey].candidates.length;
        address[] memory candidates = _fundings[regionKey].candidates;

        address[MAX_NUM_CANDIDATES_FOR_FUNDING] memory approvedCandidates; 
        uint approvedCandidateCount;

        for(uint i; i < candidateCount; i++) {
            address candidate = candidates[i];
            if(_memberBase._isCampaignSupported(3, candidate)) {
                approvedCandidates[approvedCandidateCount] = candidate;
                approvedCandidateCount++;
            }

            _resetVotingCampaingn(3, candidate);

            delete _additionalInfo[candidate].isValidFundingCandidate;

        }

        if(approvedCandidateCount > 0) {
            uint fundsPerPerson = _fundsPerRegion[regionKey].div(approvedCandidateCount);


            for(uint i; i < approvedCandidateCount; i++) {
                _accumulatedProfit[approvedCandidates[i]] += fundsPerPerson;
                
                emit LogSendFunding(approvedCandidates[i], fundsPerPerson);
            }
        }

        delete _fundsPerRegion[regionKey];
        delete _fundings[regionKey];
        delete _fundings[regionKey].candidates;

        emit LogDonationDistributionFinished(location.iSOCode, location.province);

        return true;
    } 

    function activateFundingCampaign (Location memory location) public onlyOwner{
        bytes32 regionKey = _returnLocationKey(location);
        require(_fundings[regionKey].created == 0, 'Already activated');

        _fundings[regionKey].created = now;

        emit LogFundingCampaignActivated(location.iSOCode, location.province);
    }

    function getDonationCandidates (Location memory location) public view returns(address[] memory) {
        return _fundings[_returnLocationKey(location)].candidates;
    }

    function getAccumulatedFundsPerRegion (Location memory location) public view returns(uint) {
        return _fundsPerRegion[_returnLocationKey(location)];
    }

    function retrieveProfit () 
        external
        payable
    returns (bool) {
        address payable storeOwner = msg.sender;

        uint accumulatedProfitFromSales = _accumulatedProfit[storeOwner];

        require(accumulatedProfitFromSales > 0, '19');

        uint profit = accumulatedProfitFromSales.mul(100 - _profit_fee).div(100); // take market fee

        // save market fee for donation
        uint marketFee = accumulatedProfitFromSales - profit;

        bytes32 regionKey = _returnLocationKey(_additionalInfo[storeOwner].location);

        _fundsPerRegion[regionKey] += marketFee;

        delete _accumulatedProfit[storeOwner];

        storeOwner.transfer(profit);

        emit LogProfitRetrieved(storeOwner);

        return true;
    }

    function donateToProvince(Location memory location) public payable {
        uint sendFunds = msg.value;
        address sender = msg.sender;

        require(sendFunds > 0);

        require(AdministrableByRegion.returnAdminsPerProvince(location).length > 0, 'Not supported');

        if(isMember(sender) && sendFunds >= 1 ether) {
            _upMemberVoteWeight(sender, 5);
        }

        uint currentAvailableFunds = _fundsPerRegion[_returnLocationKey(location)];

        currentAvailableFunds += sendFunds;

        _fundsPerRegion[_returnLocationKey(location)] = currentAvailableFunds;

        emit LogRegisteredDonationForRegion(location.iSOCode, location.province, sendFunds, currentAvailableFunds);
    }

    // TODO: make is Member private?
    function getMembershipInfo(address accAddress) public view returns (bool isMember, bool isAdmin,bool isOwner){
        (bool _member, bool _owner) = _getMembershipInfo(accAddress);
        bool _isAdmin = _adminRepository.contains(accAddress);

        return (_member, _isAdmin, _owner);
    }

    function() external payable {
        revert();
    }
}