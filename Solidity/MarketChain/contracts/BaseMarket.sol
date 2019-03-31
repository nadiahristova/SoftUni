pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./AdministrableByRegion.sol";
import "./VotingMemberBase.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../libraries/SafeMath.sol";

import "../interfaces/MarketInterface.sol";
import "../interfaces/MemberBaseInterface.sol";
import "../interfaces/ClientBaseInterface.sol";
import "../interfaces/ProducerBaseInterface.sol";

// TODO: delegate call fro factory contracts solidity


// TODO: upgradable pattern here
// TODO: Donation 
// TODO: STANDART Contract in producer base => standart name
// TODO: remove membership drama
// TODO: product return by 10's


// TODO: delegate call with store add/remove
// TODO: market catches notifications on store change and item change
// maybe implement it off-chain
// TODO: add mortal


// Note: voting should be pausable

// onlyWhenInitialized, onlyValidAddress
contract BaseMarket is MarketInterface, VotingMemberBase, AdministrableByRegion, InvoiceProductPurchaseValidator {
    using SafeMath for uint256;

    ///@dev keep track of participants in the market chain and their vote weight
    // address of Member Base Contract => member address => member info - maybe turn to bool ?

    struct Store {
        bytes32 name;
        uint[] storeFronts;
    }

    mapping(bytes32 => Store) _openedStoreFrtontsByMember;
    ///@dev maps storefront key to index of value of _openedStoreFrtontsByMember
    ///@notice in case value 0 - this means that a storeFront is not opened
    mapping(bytes32 => mapping(uint => uint)) _isStoreFrontToStoreFrontIdMap;
    //mapping(address => mapping(address => bool)) _approvedClientBase; ??

    mapping(address => uint) _accumulatedProfit;

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 30;

    uint _profit_fee; // percents

    // const cost for publishing store fronts getter
    // cost for notifiying new item added

    mapping(address => bool) _associatedClientMemberBase;
    mapping(address => bool) _associatedProducerMemberBase;


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

    event LogClientBaseAssigned(address indexed clientBaseAddress);
    event LogProducerBaseAssigned(address indexed producerBaseAddress);

    event LogNewStoreOpened(address indexed memberAddress, address indexed producerBaseAddress);
    event LogMemberAddedStoreFront(address indexed memberAddress, address indexed producerBaseAddress, uint storeFrontId);
    event LogMemberRemovedStoreFront(address indexed memberAddress, address indexed producerBaseAddress, uint storeFrontId);

    event PurchaseRegistered(address indexed buyer, address indexed seller, address indexed producerBase, uint productId);


    modifier onlyMarketMember (address accAddress) {
        require(isMember(accAddress));
        _;
    }
    
    modifier onlyPartnerClientBase (address memberBase) {
        require(_associatedClientMemberBase[memberBase]);
        _;
    }

    modifier onlyPartnerProducerBase (address memberBase) {
        require(_associatedProducerMemberBase[memberBase]);
        _;
    }

    modifier onlyMemberBaseCall () {
        require(_associatedClientMemberBase[msg.sender] || _associatedProducerMemberBase[msg.sender] );
        _;
    }

    ///@dev Checks whether given account is a member to a registered memberbase
    ///@param accountAddress 
    
    
    ///@dev Initializes constants needed for some ground rules and limitations
    function initialize (uint[2] memory defaultCampaignTimePeriods, uint profit_fee, uint decisiveVoteWeightProportion, uint decisiveVoteCountProportion, 
    bytes32[] memory campaignNames, uint[] memory campaignTimePeriods) onlyOwner public returns(bool) {
        require(!_isInitialized);

        require(profit_fee > 0 && profit_fee <= 99);

        uint numOfCampaigns = campaignNames.length;
        require(numOfCampaigns == campaignTimePeriods.length && numOfCampaigns <= 25);

        _profit_fee = profit_fee;

        for (uint i = 0; i < numOfCampaigns; i++) {
            require(campaignNames[i] != 0x0 && campaignTimePeriods[i] > 5 minutes);

            _availableCampaigns[i].name = campaignNames[i];
            _availableCampaigns[i].activeTimespan = campaignTimePeriods[i];
        }
        
        super.initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion);
    }

    ///@dev Affiliate client base to the market environment
    ///@param memberBase Client Base Contract
    function affiliateClientBase (ClientBaseInterface memberBase) external onlyWhenInitialized onlyOwner {
        address memberBaseAddress = address(memberBase);

        require(!_associatedClientMemberBase[memberBaseAddress]);

        _associatedClientMemberBase[memberBaseAddress] = true;

        emit LogClientBaseAssigned(memberBaseAddress);
    }

    ///@dev Affiliate client base to the market environment
    ///@param memberBase Producer Base Contract
    function affiliateProducerBase (ProducerBaseInterface memberBase) external onlyWhenInitialized onlyOwner {
        address memberBaseAddress = address(memberBase);

        require(!_associatedProducerMemberBase[memberBaseAddress]);

        _associatedProducerMemberBase[memberBaseAddress] = true;

        emit LogProducerBaseAssigned(memberBaseAddress);
    }

    // todo change so empty address can also register store
    function openStore (ProducerBaseInterface memberBase, bytes32 name) 
        onlyMember
        public { 

        require(name != 0x0); // use name as a marker
        address storeOwner = msg.sender;
        address producerBase = address(memberBase);

        require(!hasStore(storeOwner, producerBase));


        bytes32 storeFrontHashedKey = _returnStoreLocatorKey(storeOwner, producerBase);

        _openedStoreFrtontsByMember[storeFrontHashedKey].name = name;

        emit LogNewStoreOpened(storeOwner, producerBase);
    }

    function hasStore (address storeOwner, address producerBase) 
        view
        private  
    returns(bool) {
        return _openedStoreFrtontsByMember[_returnStoreLocatorKey(storeOwner, producerBase)].name != 0x0;
    }

    function hasStoreFront (bytes32 storeLocatorKey, uint256 storeFrontId) 
        view
        private  
    returns(bool) {
        return _openedStoreFrtontsByMember[storeLocatorKey].name != 0x0 && _isStoreFrontToStoreFrontIdMap[storeLocatorKey][storeFrontId] != 0;
    }

    function addStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) 
        external 
        onlyPartnerProducerBase(address(memberBase)) // TODO maybe remove
        onlyMember
    returns (bool) {
        address storeOwner = msg.sender;

        require(!hasStoreFront(_returnStoreLocatorKey(storeOwner, address(memberBase)), storeFrontId));

        return _addStoreFront(storeOwner, storeFrontId, address(memberBase));
    }  

    function memberBaseAddStoreFront (address storeOwner, uint storeFrontId) 
        public 
        onlyPartnerProducerBase(msg.sender)
        onlyMarketMember(storeOwner)
    returns (bool) {

        address memberBase = msg.sender;

        require(!hasStoreFront(_returnStoreLocatorKey(storeOwner, memberBase), storeFrontId));
        
        return _addStoreFront(storeOwner, storeFrontId, memberBase);
    }  

    function removeStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) 
        external
        onlyPartnerProducerBase(address(memberBase))
        onlyMember
    returns (bool) {
        address storeOwner = msg.sender;

        require(hasStoreFront(_returnStoreLocatorKey(storeOwner, address(memberBase)), storeFrontId));

        return _removeStoreFront(storeOwner, storeFrontId, address(memberBase));
    }  

    function notifyForStoreFrontDeletion (address storeOwner, uint storeFrontId) 
        public
        onlyPartnerProducerBase(msg.sender)
        onlyMarketMember(storeOwner)
    returns (bool) {

        address memberBase = msg.sender;

        require(!hasStoreFront(_returnStoreLocatorKey(storeOwner, memberBase), storeFrontId));

        return _removeStoreFront(storeOwner, storeFrontId, memberBase);
    } 


    function buyProduct (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        payable
        onlyNaturalNumber(nonce)
        onlyValidAddress(invoice.producerBase)
        onlyPartnerProducerBase(invoice.producerBase)
        onlyMarketMember(invoice.seller)
    returns(bool) {

        require(_hasValidState(invoice));

        require(hasStoreFront(_returnStoreLocatorKey(invoice.seller, invoice.producerBase), invoice.storeFrontId));

        address payable buyer = msg.sender;

        require(invoice.buyer == buyer);
        
        bool isValidInvoice = _validateProductPurchase(invoice, nonce, signature);
        
        require(isValidInvoice);

        uint256 pricePaid = msg.value;
        uint256 productPrice = invoice.amount.mul(invoice.pricePerUnit);

        require(pricePaid >= productPrice);

        bool successfulRegistration = ProducerBaseInterface(invoice.producerBase).registerPurchaseWithInvoice(invoice, nonce, signature);

        require(successfulRegistration);

        address seller = invoice.seller;
        uint256 productId = invoice.productId;

        _accumulatedProfit[seller] = _accumulatedProfit[seller].add(productPrice);

        uint256 excessPaymet = pricePaid.sub(productPrice);

        if(excessPaymet > 0) {
            buyer.transfer(excessPaymet);
        }

        upMemberVoteWeight(buyer, 1);
        upMemberVoteWeight(seller, 2);

        emit PurchaseRegistered(buyer, seller, invoice.producerBase, productId);

        return true;
    }

    function retrieveProfit () 
        external
        payable
        onlyMember 
    returns (bool) {
        address payable storeOwner = msg.sender;

        uint accumulatedProfitFromSales = _accumulatedProfit[storeOwner];

        if (accumulatedProfitFromSales > 0) {
            uint profit = accumulatedProfitFromSales.mul(100 - _profit_fee).div(100); // take market fee

            storeOwner.transfer(profit);

            delete _accumulatedProfit[storeOwner];

            return true;
        }

        return false;
    }



    function _addStoreFront (address storeOwner, uint storeFrontId, address producerBase) private returns(bool) {
        bytes32 storeLocatorKey = _returnStoreLocatorKey(storeOwner, producerBase);

        uint alreadyClaimedStoreFrontSlotCount = _openedStoreFrtontsByMember[storeLocatorKey].storeFronts.length;

        require(alreadyClaimedStoreFrontSlotCount < MAX_STOREFRONTS_PER_STORE);

        // newIndexAccordingToOccupiedSlot = index of occupied slot in _openedStoreFrtontsByMember + 1
        uint newIndexAccordingToOccupiedSlot = _openedStoreFrtontsByMember[storeLocatorKey].storeFronts.push(storeFrontId);
        _isStoreFrontToStoreFrontIdMap[storeLocatorKey][storeFrontId] = newIndexAccordingToOccupiedSlot;

        emit LogMemberAddedStoreFront(storeOwner, producerBase, storeFrontId);
    }

    function _removeStoreFront (address storeOwner, uint storeFrontId, address producerBase) private returns(bool) {
        bytes32 storeLocatorKey = _returnStoreLocatorKey(storeOwner, producerBase);

        uint storeFrontCount = _openedStoreFrtontsByMember[storeLocatorKey].storeFronts.length;
        uint storeFrontIndex_deleted = _isStoreFrontToStoreFrontIdMap[storeLocatorKey][storeFrontId] - 1;

        require(storeFrontIndex_deleted < storeFrontCount);

        if(storeFrontCount > 1) {
            _openedStoreFrtontsByMember[storeLocatorKey].storeFronts[storeFrontIndex_deleted] = 
                _openedStoreFrtontsByMember[storeLocatorKey].storeFronts[storeFrontCount - 1];
        }

        _openedStoreFrtontsByMember[storeLocatorKey].storeFronts.pop();
        delete _isStoreFrontToStoreFrontIdMap[storeLocatorKey][storeFrontId];

        emit LogMemberRemovedStoreFront(storeOwner, producerBase, storeFrontId);

        return true;
    }

    function _returnStoreLocatorKey (address storeOwner, address memberBase) 
        pure
        private  
    returns(bytes32) {
        return keccak256(abi.encodePacked(storeOwner, memberBase));
    }
}