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

contract BaseMarket is InvoiceProductPurchaseValidator, BaseMarketInterface, VotingMemberBase  {
    using SafeMath for uint256;

    uint _profit_fee; // percents

    mapping(bytes32 => Store) _openedStoreFrtontsByMember;

    //dev - maps storefront key to index of value of _openedStoreFrtontsByMember
    //notice - when value is 0 - this means storeFront with given id is does not exists yet
    mapping(bytes32 => mapping(uint => uint)) _isStoreFrontToStoreFrontIdMap;
    //mapping(address => mapping(address => bool)) _approvedClientBase; 

    // account address to accumulated profit from the sales in wei
    mapping(address => uint) _accumulatedProfit;

    //address of the member base => associated/not
    mapping(address => bool) _associatedClientMemberBase;
    mapping(address => bool) _associatedProducerMemberBase;

    struct Store {
        // name of the store, can be used as a flag if 0x0 => store is not yet created
        bytes32 name; 
        // dynamic array of store front ids
        uint[] storeFronts;
    }

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 30;

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

    
    // modifier onlyPartnerClientBase (address memberBase) {
    //     require(_associatedClientMemberBase[memberBase]);
    //     _;
    // }

    modifier onlyPartnerProducerBase (address memberBase) {
        require(_associatedProducerMemberBase[memberBase], '11');
        _;
    }

     modifier onlyWhenStoreOwner (address storeOwner, address producerBase, bool isOwner) {
        require((_openedStoreFrtontsByMember[_returnStoreLocatorKey(storeOwner, producerBase)].name != 0x0) == isOwner, '12');
        _;
    }

     modifier onlyWhenStoreFrontOwner (address storeOwner, address producerBase, uint storeFrontId, bool isOwner) {
        require(hasStoreFront(storeOwner, producerBase, storeFrontId) == isOwner);
        _;
    }
    
    ///@dev Initializes constants needed for some ground rules and limitations
    ///@param defaultCampaignTimePeriods fixed size array - first value: time duration of membership granting campaign, second: time duration of membership revocation campaign
    ///@param profit_fee percents kept by the market from the sales 
    ///@param decisiveVoteWeightProportion denominator used for determining how much of overall vote weight is needed for a given campaign to be successful
    ///@param decisiveVoteCountProportion denominator used for determining how much of overall supporters member count is needed for a given campaign to be successful
    ///@param initialOwnerVoteWeight vote weight of the owner by default
    ///@param campaignNames names of the campaigns supported by this contract
    ///@param campaignTimePeriods time span through which given campaign is active
    ///@notice campaignNames and campaignTimePeriods should have the same length, profit fee is between 0 and 99 percents
    function _initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint profit_fee, 
            uint decisiveVoteWeightProportion, 
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
        
        _initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, initialOwnerVoteWeight);
    }

    ///@dev Affiliate client base to the market environment
    ///@param memberBaseAddress Client Base Contract address
    function affiliateClientBase (address memberBaseAddress) 
        external 
        onlyValidAddress(memberBaseAddress)
        onlyWhenInitialized 
        onlyOwner {

        require(!_associatedClientMemberBase[memberBaseAddress],'9');

        _associatedClientMemberBase[memberBaseAddress] = true;

        emit LogClientBaseAssigned(memberBaseAddress);
    }

    ///@dev Affiliate producer/salesmen base to the market environment
    ///@param memberBaseAddress Producer Base Contract address
    function affiliateProducerBase (address memberBaseAddress) 
        external 
        onlyValidAddress(memberBaseAddress)
        onlyWhenInitialized 
        onlyOwner {

        require(!_associatedProducerMemberBase[memberBaseAddress],'9');

        _associatedProducerMemberBase[memberBaseAddress] = true;

        emit LogProducerBaseAssigned(memberBaseAddress);
    }
    
    function _openStore (address producerBase, address storeOwner,  bytes32 name) internal { 
        bytes32 storeFrontHashedKey = _returnStoreLocatorKey(storeOwner, producerBase);

        _openedStoreFrtontsByMember[storeFrontHashedKey].name = name;

        emit LogNewStoreOpened(storeOwner, producerBase);
    }

    /// @dev Used by a producer affiliate for store front publishing
    /// @notice Store owner has to be member of producer base affiliate and the market itself
    /// @param storeOwner account address of the store owner
    /// @param storeFrontId id used for identifying store front in the environment of producer base affiliate
    /// @return True on success, false otherwise
    function memberBaseAddStoreFront (address storeOwner, uint storeFrontId) 
        public 
        onlyValidAddress(storeOwner)
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyWhenMember(storeOwner, true)
        onlyPartnerProducerBase(msg.sender)
        onlyWhenStoreOwner(storeOwner, msg.sender, true)
        onlyWhenStoreFrontOwner(storeOwner, msg.sender, storeFrontId, false)
    returns (bool) {

        _addStoreFront(storeOwner, storeFrontId, msg.sender);

        return true;
    } 

    /// @dev Used by the producer base affiliate for notifying of removal of store front
    /// @notice Used only by the producer base affiliate
    /// @param storeOwner account address of the store owner
    /// @param storeFrontId id used for identifying store front in the environment of producer base affiliate
    /// @return True on success, false otherwise
    function notifyForStoreFrontDeletion (address storeOwner, uint storeFrontId) 
        public
        onlyValidAddress(storeOwner)
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyPartnerProducerBase(msg.sender)
        onlyWhenMember(storeOwner, true)
        onlyWhenStoreOwner(storeOwner, msg.sender, true)
        onlyWhenStoreFrontOwner(storeOwner, msg.sender, storeFrontId, true)
    returns (bool) {

        return _removeStoreFront(storeOwner, storeFrontId, msg.sender);
    } 

    function _registerSale(address seller, uint fundsWei) internal {
        _accumulatedProfit[seller] = _accumulatedProfit[seller].add(fundsWei);
    }

    // function buyProduct (
    //         InvoiceDetails memory invoice,
    //         uint256 nonce, 
    //         bytes memory signature) 
    //     public 
    //     payable
    //     onlyNaturalNumber(nonce)
    //     onlyValidInvoice(invoice)
    //     onlyWhenMember(invoice.seller, true)
    //     onlyPartnerProducerBase(invoice.producerBase)
    // returns(bool) {

    //     address payable buyer = msg.sender;

    //     require(invoice.buyer == buyer, '30');
        
    //     require(_validateProductPurchase(invoice, nonce, signature), '22');

    //     uint productPrice = invoice.amount.mul(invoice.pricePerUnit);

    //     uint256 excessPayment = msg.value.sub(productPrice);// Safe Math is assuring that msg.value >= productPrice

    //     require(ProducerBaseInterface(invoice.producerBase).registerPurchaseWithInvoice(invoice, nonce, signature), '23');// validate storeFrontId and product Id

    //     _accumulatedProfit[invoice.seller] = _accumulatedProfit[invoice.seller].add(productPrice);

    //     if(excessPayment > 0) {
    //         buyer.transfer(excessPayment);
    //     }

    //     _upMemberVoteWeight(buyer, 1);
    //     _upMemberVoteWeight(invoice.seller, 2);

    //     emit PurchaseRegistered(buyer, invoice.seller, invoice.producerBase, invoice.productId);

    //     return true;
    // }

    function retrieveProfit () 
        external
        payable
    returns (bool) {
        return true;
    }

    function getAccumolatedProfit () external view onlyMember returns(uint) {
        return _accumulatedProfit[msg.sender];
    }

    function hasStoreFront(address storeOwner, address producerBase, uint storeFrontId)
        view
        public 
        onlyNaturalNumber(storeFrontId)
        onlyValidAddress(storeOwner)
        onlyWhenInitialized
        onlyPartnerProducerBase(producerBase)
    returns (bool) {

        return _isStoreFrontToStoreFrontIdMap[_returnStoreLocatorKey(storeOwner, producerBase)][storeFrontId] != 0;
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

    function _removeStoreFront (address storeOwner, uint storeFrontId, address producerBase) internal returns(bool) {
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