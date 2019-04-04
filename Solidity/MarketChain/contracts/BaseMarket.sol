pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./AdministrableByRegion.sol";
import "./VotingMemberBase.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../libraries/SafeMath.sol";

import "../interfaces/BaseMarketInterface.sol";
import "../interfaces/MemberBaseInterface.sol";
import "../interfaces/ClientBaseInterface.sol";
import "../interfaces/ProducerBaseInterface.sol";

// TODO: delegate call fro factory contracts solidity

// TODO: upgradable pattern here
// TODO: STANDART Contract in producer base => standart name

// TODO: delegate call with store add/remove
// TODO: market catches notifications on store change and item change
// maybe implement it off-chain


// Note: voting should be pausable

// onlyWhenInitialized, onlyValidAddress
contract BaseMarket is InvoiceProductPurchaseValidator, BaseMarketInterface, VotingMemberBase  {
    using SafeMath for uint256;

    struct Store {
        bytes32 name;
        uint[] storeFronts;
    }

    mapping(bytes32 => Store) _openedStoreFrtontsByMember;
    ///@dev maps storefront key to index of value of _openedStoreFrtontsByMember
    ///@notice in case value 0 - this means that a storeFront is not opened
    mapping(bytes32 => mapping(uint => uint)) _isStoreFrontToStoreFrontIdMap;
    //mapping(address => mapping(address => bool)) _approvedClientBase; 

    mapping(address => uint) _accumulatedProfit;

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 30;

    uint _profit_fee; // percents

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
        require(_associatedClientMemberBase[msg.sender] || _associatedProducerMemberBase[msg.sender]);
        _;
    }

     modifier onlyWhenStoreOwner (address storeOwner, address producerBase, bool isOwner) {
        require(isMember(storeOwner));
        require((_openedStoreFrtontsByMember[_returnStoreLocatorKey(storeOwner, producerBase)].name != 0x0) == isOwner);
        _;
    }

     modifier onlyWhenStoreFrontOwner (address storeOwner, address producerBase, uint storeFrontId, bool isOwner) {
        require((_isStoreFrontToStoreFrontIdMap[_returnStoreLocatorKey(storeOwner, producerBase)][storeFrontId] != 0) == isOwner);
        _;
    }
    
    
    ///@dev Initializes constants needed for some ground rules and limitations
    function _initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint profit_fee, uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion, 
            uint initialOwnerVoteWeight, 
            bytes32[] memory campaignNames, 
            uint[] memory campaignTimePeriods) 
        internal
    {
        require(profit_fee > 0 && profit_fee <= 99);

        uint numOfCampaigns = campaignNames.length;
        require(numOfCampaigns == campaignTimePeriods.length && numOfCampaigns <= 25);

        _profit_fee = profit_fee;

        for (uint i = 0; i < numOfCampaigns; i++) {
            require(campaignNames[i] != 0x0 && campaignTimePeriods[i] > 5 minutes);

            _availableCampaigns[i].name = campaignNames[i];
            _availableCampaigns[i].activeTimespan = campaignTimePeriods[i];
        }
        
        super._initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, initialOwnerVoteWeight);
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
        onlyWhenStoreOwner(msg.sender, address(memberBase), false)
        external { 

        require(name != 0x0); // use name as a marker
        address storeOwner = msg.sender;
        address producerBase = address(memberBase);

        bytes32 storeFrontHashedKey = _returnStoreLocatorKey(storeOwner, producerBase);

        _openedStoreFrtontsByMember[storeFrontHashedKey].name = name;

        emit LogNewStoreOpened(storeOwner, producerBase);
    }


    function addStoreFront (ProducerBaseInterface memberBase, uint storeFrontId) 
        external 
        onlyPartnerProducerBase(address(memberBase)) // TODO maybe remove
        onlyWhenStoreOwner(msg.sender, address(memberBase), true)
        onlyWhenStoreFrontOwner(msg.sender, address(memberBase), storeFrontId, false)
    returns (bool) {

        return _addStoreFront(msg.sender, storeFrontId, address(memberBase));
    }  

    function memberBaseAddStoreFront (address storeOwner, uint storeFrontId) 
        public 
        onlyPartnerProducerBase(msg.sender)
        onlyWhenStoreOwner(storeOwner, msg.sender, true)
        onlyWhenStoreFrontOwner(storeOwner, msg.sender, storeFrontId, true)
    returns (bool) {
        
        return _addStoreFront(storeOwner, storeFrontId, msg.sender);
    }  

    function removeStoreFront (ProducerBaseInterface memberBase, uint256 storeFrontId) 
        external
        onlyPartnerProducerBase(address(memberBase))
        onlyWhenStoreOwner(msg.sender, address(memberBase), true)
        onlyWhenStoreFrontOwner(msg.sender, address(memberBase), storeFrontId, true)
    returns (bool) {

        return _removeStoreFront(msg.sender, storeFrontId, address(memberBase));
    }  

    function notifyForStoreFrontDeletion (address storeOwner, uint storeFrontId) 
        public
        onlyPartnerProducerBase(msg.sender)
        onlyWhenStoreOwner(storeOwner, msg.sender, true)
        onlyWhenStoreFrontOwner(storeOwner, msg.sender, storeFrontId, true)
    returns (bool) {

        return _removeStoreFront(storeOwner, storeFrontId, msg.sender);
    } 

    function buyProduct (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        payable
        onlyNaturalNumber(nonce)
        onlyValidInvoice(invoice)
        onlyPartnerProducerBase(invoice.producerBase)
    returns(bool) {

        address payable buyer = msg.sender;

        require(invoice.buyer == buyer);
        
        require(_validateProductPurchase(invoice, nonce, signature));

        uint productPrice = invoice.amount.mul(invoice.pricePerUnit);

        uint256 excessPaymet = msg.value.sub(productPrice);// Safe Math is assuring that msg.value >= productPrice

        require(ProducerBaseInterface(invoice.producerBase).registerPurchaseWithInvoice(invoice, nonce, signature));// validate storeFrontId and product Id

        _accumulatedProfit[invoice.seller] = _accumulatedProfit[invoice.seller].add(productPrice);

        if(excessPaymet > 0) {
            buyer.transfer(excessPaymet);
        }

        _upMemberVoteWeight(buyer, 1);
        _upMemberVoteWeight(invoice.seller, 2);

        emit PurchaseRegistered(buyer, invoice.seller, invoice.producerBase, invoice.productId);

        return true;
    }

    function retrieveProfit () 
        external
        payable
    returns (bool) {
        return true;
    }

    function getAccumolatedProfit () external view onlyMember returns(uint) {
        return _accumulatedProfit[msg.sender];
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

    function _hasStore (address storeOwner, address producerBase) 
        view
        private  
    returns(bool) {
        return _openedStoreFrtontsByMember[_returnStoreLocatorKey(storeOwner, producerBase)].name != 0x0;
    }

    function _hasStoreFront (bytes32 storeLocatorKey, uint256 storeFrontId) 
        view
        private  
    returns(bool) {
        return _openedStoreFrtontsByMember[storeLocatorKey].name != 0x0 && _isStoreFrontToStoreFrontIdMap[storeLocatorKey][storeFrontId] != 0;
    }
}