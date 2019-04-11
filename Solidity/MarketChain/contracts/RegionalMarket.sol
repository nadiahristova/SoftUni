pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./BaseMarket.sol";
import "./Pausable.sol";
import "./AdministrableByRegion.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../interfaces/ProducerBaseInterface.sol";
import "../interfaces/RegionalMarketInterface.sol";

// Under Construction
contract RegionalMarket is InvoiceProductPurchaseValidator, AdministrableByRegion, BaseMarket, RegionalMarketInterface {
    // min time period between 2 donation campaigns for given region
    uint256 _donationRoundTimePeriod;

    // funding campaigns per province hashed key
    mapping(bytes32 => Funding) _fundings;

    // additinal member information relevant to the curren implementation of the market
    mapping(address => MemberInfo) _additionalInfo; 
    // gathered funds through donation and fees for given region 
    mapping(bytes32 => uint) _fundsPerRegion;
    
    uint constant TIME_BETWEEN_WON_CANDIDACIES = 356 days;
    uint constant MAX_NUM_CANDIDATES_FOR_FUNDING = 15; 
    uint constant MIN_AMOUNT_OF_FUNDS_BEFORE_DISTRIBUTION = 1000 * 1 ether;

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
    
    event LogProfitRetrieved(address storeOwner);
    event LogSendFunding(address storeOwner, uint amount);
    event LogDonationDistributionFinished(bytes2 iSOCode, bytes30 province);
    event LogStoreOwnerSuggestedForFunding(address storeOwner, address admin, bytes2 iSOCode, bytes30 province);
    event LogFundingCampaignActivated(bytes2 iSOCode, bytes30 province);
    event LogRegisteredDonationForRegion(bytes2 iSOCode, bytes30 province, uint256 donation, uint256 overAllFunds);

    ///@dev Initializes constants needed for some ground rules and limitations
    ///@param defaultCampaignTimePeriods fixed size array - first value: time duration of membership granting campaign, second: time duration of membership revocation campaign
    ///@param profit_fee percents kept by the market from the sales 
    ///@param decisiveVoteWeightProportion denominator used for determining how much of overall vote weight is needed for a campaign to be successful
    ///@param decisiveVoteCountProportion denominator used for determining how much of overall supporters member count is needed for a given campaign to be successful
    ///@param initialOwnerVoteWeight vote weight of the owner by default
    ///@notice Profit fee is between 0 and 99 percents
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

        BaseMarket._initialize(defaultCampaignTimePeriods, profit_fee, decisiveVoteWeightProportion, decisiveVoteCountProportion, 
        initialOwnerVoteWeight, campaignNames, campaignTimePeriods);
    }

    ///@dev Candidate members should register themselves
    ///@notice Province name should be between 1 and 30 letters
    ///@param location ISO Code of country and Province name
    function registerMember(Location memory location)
        public
        onlyValidLocation(location)
        onlyWhenInitialized
        onlyWhenMember(msg.sender, false)
    returns (bool) {

        address accAddress = msg.sender;

        _additionalInfo[accAddress].location = location;

        VotingMemberBase._registerMember(accAddress);

        return true;
    }

    ///@dev Owner fires campaign for membership revocation
    ///@notice Only owner has this right
    ///@param accAddress Account Address
    function launchMembershipRevocationCampaign (address accAddress) 
        public 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyOwner 
        onlyWhenMember(accAddress, true)
    returns(bool) {

        return VotingMemberBase._launchCampaign(accAddress, 2);
    }

    /// @dev Opens store in the market environment
    /// @param producerBase address of the producer base where the actual store data is managed 
    /// @notice market should be affiliated with the producer base, 
    /// sender should be member of the producer base and the market
    function openStore (address producerBase, bytes32 name) 
        onlyWhenInitialized
        whenNotPaused
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
        whenNotPaused
        onlyAdmin 
        onlyPartnerProducerBase(memberBase)
        onlyWhenStoreOwner(storeOwner, memberBase, true)
    {
        address admin = msg.sender;

        require(admin != storeOwner);

        MemberInfo memory info = _additionalInfo[storeOwner];
        bytes32 regionKey = AdministrableByRegion._returnLocationKey(info.location);

        require(_fundings[regionKey].created != 0); // campaign is active

        require(AdministrableByRegion._returnLocationKey(_additionalInfo[admin].location) == regionKey); // Admin should be of same region

        require(!info.isValidFundingCandidate);//already proposed
        require(info.lastlyWonFunding + TIME_BETWEEN_WON_CANDIDACIES <= now, 'Wait time');

        require(_fundings[regionKey].candidates.push(storeOwner) <= MAX_NUM_CANDIDATES_FOR_FUNDING, 'No spots');

        _additionalInfo[storeOwner].isValidFundingCandidate = true;

        VotingMemberBase._launchCampaign(storeOwner, 3);

        emit LogStoreOwnerSuggestedForFunding(storeOwner, admin, info.location.iSOCode, info.location.province);
    }

    /// @dev Member supports fellow member in certain campaign
    /// @param member Target of the campaign 
    /// @param votingCampaignId Local id of the campaign 
    function supportMember( 
            address member,
            uint248 votingCampaignId)
        external 
        onlyValidAddress(member) 
        onlyNaturalNumber(votingCampaignId)
        onlyWhenInitialized
        onlyMember
    {
        address supporter = msg.sender;

        require(supporter != member);

        if(votingCampaignId == 3) {

            MemberInfo memory info = _additionalInfo[member];
            bytes32 regionKey = AdministrableByRegion._returnLocationKey(info.location);
            
            require(_fundings[regionKey].created != 0); // campaign is active

            require(AdministrableByRegion._returnLocationKey(_additionalInfo[supporter].location) != regionKey); // Supporter and supported should be from different regions

            require(info.isValidFundingCandidate);//proposed by admin
        }

        VotingMemberBase._supportMember(member, votingCampaignId);
    } 

    ///@dev Used to trigger funding distribution in case of successfully ended campaign  
    ///@param location ISO Code of country and Province name
    function triggerFundingDistribution(Location memory location) 
        public 
        onlyValidLocation(location)
        onlyWhenInitialized
    returns (bool) {
        
        bytes32 regionKey = AdministrableByRegion._returnLocationKey(location);

        require(_fundings[regionKey].created != 0);
        require(_fundsPerRegion[regionKey] > MIN_AMOUNT_OF_FUNDS_BEFORE_DISTRIBUTION); // not enough funds donated

        Funding memory funding = _fundings[regionKey];

        require(funding.created + _donationRoundTimePeriod <= now, 'Wait time'); // Funding campaign had finished

        uint candidateCount = _fundings[regionKey].candidates.length;
        address[] memory candidates = _fundings[regionKey].candidates;

        address[MAX_NUM_CANDIDATES_FOR_FUNDING] memory approvedCandidates; 
        uint approvedCandidateCount;

        for(uint i; i < candidateCount; i++) {
            address candidate = candidates[i];
            if(VotingMemberBase._memberBase._isCampaignSupported(3, candidate)) {
                approvedCandidates[approvedCandidateCount] = candidate;
                approvedCandidateCount++;
            }

            VotingMemberBase._resetVotingCampaingn(3, candidate);

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

    /// @dev Start a funding campaign
    /// @notice Only owner can trigger this function
    /// @param location The name of a food to evaluate (English)
    /// @return true if Bugs will eat it, false otherwise
    function activateFundingCampaign (Location memory location) 
        public
        onlyValidLocation(location)  
        onlyWhenInitialized
        whenNotPaused
        onlyOwner {
        bytes32 regionKey = AdministrableByRegion._returnLocationKey(location);
        require(_fundings[regionKey].created == 0, 'Already activated');

        _fundings[regionKey].created = now;

        emit LogFundingCampaignActivated(location.iSOCode, location.province);
    }

    /// @dev Returns view of the candidates for donation
    /// @param location ISO Code of country and Province name
    /// @return dynamic array of addresses of the candidates for donation
    function getDonationCandidates (Location memory location) 
        public 
        view 
        onlyValidLocation(location)  
        onlyWhenInitialized
    returns(address[] memory) {
        return _fundings[AdministrableByRegion._returnLocationKey(location)].candidates;
    }

    /// @dev Return gathered funds per country region
    /// @param location ISO Code of country and Province name
    /// @return Funds gathered through fees and donations per country region in wei
    function getAccumulatedFundsPerRegion (Location memory location) 
        public 
        view 
        onlyWhenInitialized
    returns(uint) {
        return _fundsPerRegion[AdministrableByRegion._returnLocationKey(location)];
    }

    /// @dev Used by the participants in a funding campaign to retrieve funds
    /// @notice Campaign has to be successful and funds are destributed between campaign participants
    /// @return Gathered funds in wei
    function retrieveProfit () 
        external
        payable 
        onlyWhenInitialized {

        address payable storeOwner = msg.sender;

        uint accumulatedProfitFromSales = _accumulatedProfit[storeOwner];

        require(accumulatedProfitFromSales > 0, '19');

        uint profit = accumulatedProfitFromSales.mul(100 - _profit_fee).div(100); // take market fee

        // save market fee for donation
        uint marketFee = accumulatedProfitFromSales - profit;

        bytes32 regionKey = AdministrableByRegion._returnLocationKey(_additionalInfo[storeOwner].location);

        _fundsPerRegion[regionKey] += marketFee;

        delete _accumulatedProfit[storeOwner];

        storeOwner.transfer(profit);

        emit LogProfitRetrieved(storeOwner);
    }

    /// @dev Make a donation to a given province in country
    /// @notice Everyone can make a donation, no records are kept
    /// @param location country ISO Code and Province name
    function donateToProvince(Location memory location) 
        public 
        payable 
        onlyValidLocation(location)
        onlyWhenInitialized
        whenNotPaused {
        uint sendFunds = msg.value;
        address sender = msg.sender;

        require(sendFunds > 0);

        require(AdministrableByRegion.returnAdminsPerProvince(location).length > 0, 'Not supported');

        if(isMember(sender) && sendFunds >= 1 ether) {
            VotingMemberBase._upMemberVoteWeight(sender, 5);
        }

        uint currentAvailableFunds = _fundsPerRegion[AdministrableByRegion._returnLocationKey(location)];

        currentAvailableFunds += sendFunds;

        _fundsPerRegion[AdministrableByRegion._returnLocationKey(location)] = currentAvailableFunds;

        emit LogRegisteredDonationForRegion(location.iSOCode, location.province, sendFunds, currentAvailableFunds);
    }

    /// @dev Retrieve market membership information for given member
    /// @param accAddress Account address of the mmeber
    /// @return is member, is admin, is owner in market
    function getMembershipInfo(address accAddress) 
        public 
        view 
        onlyValidAddress(accAddress) 
        onlyWhenInitialized
    returns (bool isMember, bool isAdmin,bool isOwner){
        (bool _member, bool _owner) = VotingMemberBase._getMembershipInfo(accAddress);
        bool _isAdmin = _adminRepository.contains(accAddress);

        return (_member, _isAdmin, _owner);
    }

    /// @dev A client purchases a product with invoice issued by the seller
    /// @notice Invoices have expiration date
    /// @param invoice Invoice details
    /// @param nonce seller's nonce
    /// @param signature Signed invoice
    /// @return True on success, false otherwise
    function buyProduct (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        payable
        onlyNaturalNumber(nonce)
        onlyValidInvoice(invoice)
        onlyWhenInitialized
        whenNotPaused
        onlyWhenMember(invoice.seller, true)
        onlyPartnerProducerBase(invoice.producerBase)
    returns(bool) {

        address payable buyer = msg.sender;

        require(invoice.buyer == buyer, '30');
        
        // todo make it through delegate call
        require(_validateProductPurchase(invoice, nonce, signature), '22');

        uint productPrice = invoice.amount.mul(invoice.pricePerUnit);

        uint256 excessPayment = msg.value.sub(productPrice);// Safe Math is assuring that msg.value >= productPrice

        BaseMarket._registerSale(invoice.seller, productPrice);

        require(ProducerBaseInterface(invoice.producerBase).registerPurchaseWithInvoice(invoice, nonce, signature), '23');// validate storeFrontId and product Id

        if(excessPayment > 0) {
            buyer.transfer(excessPayment);
        }

        VotingMemberBase._upMemberVoteWeight(buyer, 1);
        VotingMemberBase._upMemberVoteWeight(invoice.seller, 2);

        emit PurchaseRegistered(buyer, invoice.seller, invoice.producerBase, invoice.productId);

        return true;
    }

    // all donations should be done via donate function
    function() external payable {
        revert();
    }
}