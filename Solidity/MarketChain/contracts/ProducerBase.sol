pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./MarketMemberBase.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../libraries/InventoryLib.sol";

import "../interfaces/BaseMarketInterface.sol";
import "../interfaces/ProducerBaseInterface.sol";


// todo upgradable member base
contract ProducerBase is ProducerBaseInterface, MarketMemberBase, InvoiceProductPurchaseValidator {

    using InventoryLib for InventoryLib.StoreFronts;
    using InventoryLib for InventoryLib.ProductStock;

    InventoryLib.StoreFronts _storeFronts;
    InventoryLib.ProductStock _inventory;

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 255;

    uint constant TIME_BETWEEN_UPDATES = 5 minutes;


    modifier onlyExistingStoreFront (uint storeFrontId) {
         require(_storeFronts._isStoreFrontExisting(msg.sender, storeFrontId), '6');
         _;
    }

    modifier onlyOnNotDisabledStoreFront(uint storeFronId) {
        require(!_storeFronts._isStoreFrontDiabled(msg.sender, storeFronId), '8');
        _;
    }

    modifier onlyExistingProduct (uint storeFrontId, uint productId) {
         require(_inventory._isProductExisting(productId), '7');
         _;
    }

    modifier onlyValidItemEntry(uint pricePerUnit, uint amountProduced) {
        require(pricePerUnit >= 0 && amountProduced > 0);
        _;
    }

    event LogStoreFrontCreated(address indexed ownerAddress, uint indexed storeFrontId); 
    event LogStoreFrontRemoved(address indexed ownerAddress, uint indexed storeFrontId); 
    event LogStoreFrontDisabled(address indexed ownerAddress, uint indexed storeFrontId);
    event LogStoreFrontEnabled(address indexed ownerAddress, uint indexed storeFrontId);
    event LogProductRemovedFromStoreFront(address indexed ownerAddress, uint indexed storeFrontId, 
        uint productId);  
    event LogProductAddedToStoreFront(address indexed ownerAddress, uint indexed storeFrontId, 
        uint indexed productId); 
    event LogProductPricePerUnitUpdated(uint indexed productId, uint oldPrice, uint newPrice);
    event LogProductProducedAmountUpdated(uint indexed productId, uint oldAmount, uint newAmount);
    event LogProductPriceNegotiabilityUpdated(uint indexed productId, bool priceIsNegotiable);
    
    event LogStoreFrontShared(address storeOwner, uint256 storeFrontId, address market);

    event LogPurchaseRegistered(address client, address producer, address market, uint256 productId);

    ///@dev Initializes constants needed for some ground rules and limitations
    ///@param defaultCampaignTimePeriods fixed size array - first value: time duration of membership granting campaign, second: time duration of membership revocation campaign
    ///@param decisiveVoteWeightProportion denominator used for determining how much of overall vote weight is needed for a campaign to be successful
    ///@param decisiveVoteCountProportion denominator used for determining how much of overall supporters member count is needed for a given campaign to be successful
    ///@param initialOwnerVoteWeight vote weight of the owner by default
    function initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint initialOwnerVoteWeight) 
        public
        onlyOwner {

        require(!_isInitialized, '1');
        
        MarketMemberBase._initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, 
            initialOwnerVoteWeight, new bytes32[](0),  new uint[](0));
    }

    ///@dev Used by the owner to forcefully remove member
    ///@param accAddress Address of owner 
    function revokeMembershipImmediately(address accAddress) 
        external 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyOwner
        onlyWhenMember(accAddress, true) {

        _memberBase._revokeMembershipForced(accAddress);

        MarketMemberBase._revokeAllMarketMembership(accAddress);
    } 

    /// @dev Launches campaign for membership revocation
    /// @notice Only owner can do this, campaign id should be > 2
    /// @param accAddress Member address
    /// @return true if campaign was launched, false otherwise
    function launchMembershipRevocationCampaign (address accAddress) 
        external 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyOwner 
        onlyWhenMember(accAddress, true)
    returns(bool) {

        return VotingMemberBase._launchCampaign(accAddress, 2);
    }

    /// @dev Registers member
    /// @notice Only already existing member can register other members
    /// @param accAddress Member address
    function registerMember(address accAddress) 
        external 
        onlyWhenInitialized
        onlyMember
        onlyWhenMember(accAddress, false) {

        VotingMemberBase._registerMember(accAddress);
    }

    /// @dev Support member in campaign
    /// @param accAddress Account Address of the member
    /// @param votingCampaignId Id of the campaign
    function supportMember( // support campaign
            address accAddress,
            uint248 votingCampaignId)
        external 
        onlyValidAddress(accAddress) 
        onlyNaturalNumber(votingCampaignId)
        onlyWhenInitialized
        onlyMember {

        address supporter = msg.sender;
        
        require(supporter != accAddress);

        VotingMemberBase._supportMember(accAddress, votingCampaignId);
    } 

    /** Store Front Logic */
    /// @dev Member opens new store front
    function addStoreFront() onlyWhenInitialized onlyMember external {
        
        address storeOwner = msg.sender;

        _storeFronts._addStoreFront(storeOwner, MAX_STOREFRONTS_PER_STORE);

        VotingMemberBase._upMemberVoteWeight(storeOwner, 2);
    }

    /// @dev Remove store front from store
    /// @param storeFrontId Id of the store front
    function removeStoreFront(uint storeFrontId) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyMember 
        onlyExistingStoreFront(storeFrontId) {

        address storeOwner = msg.sender;

        _storeFronts._removeStoreFront(storeOwner, storeFrontId);

        VotingMemberBase._downMemberVoteWeight(storeOwner, 2);
    }

    /// @dev Disable store front from store
    /// @param storeFrontId Id of the store front
    function disableStoreFront(uint storeFrontId) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId) {

        address storeOwner = msg.sender;

        _storeFronts._disableStoreFront(storeOwner, storeFrontId);
    }


    /// @dev Enable store front from store
    /// @param storeFrontId Id of the store front
    function enableStoreFront(uint storeFrontId) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyMember
        onlyExistingStoreFront(storeFrontId) {

        address storeOwner = msg.sender;

        _storeFronts._enableStoreFront(storeOwner, storeFrontId);
    }

    /// @dev Publish existing store front to market 
    /// @param market Contract address of the market
    /// @param storeFrontId Id of the store front
    function publishStoreFrontToMarket(address market, uint storeFrontId) 
            external
            onlyMember  
            onlyExistingStoreFront(storeFrontId)
            onlyOnNotDisabledStoreFront(storeFrontId)
            onlyOnValidMarketMembership(msg.sender, market) {

        address storeOwner = msg.sender;

        require(BaseMarketInterface(market).memberBaseAddStoreFront(storeOwner, storeFrontId));

        VotingMemberBase._upMemberVoteWeight(storeOwner, 5);

        emit LogStoreFrontShared(storeOwner, storeFrontId, market);
    }

    /** Product Logic */

    /// @dev Add product to store front
    /// @param storeFrontId Id of the store front
    /// @param specificationId Id of full specification and category of product
    /// @param pricePerUnit Price per product unit
    /// @param amount Amount of units
    /// @param hasNegotiablePrice Can price of the product be negotiated
    /// @return true if Bugs will eat it, false otherwise
    function addProductToStoreFront(
            uint storeFrontId,
            uint specificationId, 
            uint pricePerUnit, 
            uint amount, 
            bool hasNegotiablePrice) 
        external
        onlyNaturalNumber(storeFrontId)
        onlyValidItemEntry(pricePerUnit, amount)
        onlyMember
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId) {
        //require(recognisedProducts[productDescriptionId]);

        address storeOwner = msg.sender; 
        
        uint256 newProductId = _inventory._addProductToStoreFront(storeOwner, storeFrontId, specificationId, 
            pricePerUnit, amount, hasNegotiablePrice, INVENTORY_CAP_PER_STOREFRONT);

        VotingMemberBase._upMemberVoteWeight(storeOwner, 1);

        emit LogProductAddedToStoreFront(storeOwner, storeFrontId, newProductId);
    }

    /// @dev Remove product from store front
    /// @param storeFrontId Id of the store front
    /// @param productId Product Id
    function removeProductFromStoreFront(
            uint storeFrontId,
            uint productId) 
        external
        onlyNaturalNumber(storeFrontId)
        onlyNaturalNumber(productId)
        onlyWhenInitialized
        onlyMember
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
        onlyExistingProduct(storeFrontId, productId) {

        address storeOwner = msg.sender;
        
        _inventory._removeProductFromStoreFront(storeOwner, storeFrontId, productId);

        VotingMemberBase._downMemberVoteWeight(storeOwner, 1);

        emit LogProductRemovedFromStoreFront(storeOwner, storeFrontId, productId);
    }

    /// @dev Updates product information
    /// @param storeFrontId Id of the store front
    /// @param specificationId Id of full specification and category of product
    /// @param pricePerUnit Price per product unit
    /// @param amount Amount of units
    /// @param hasNegotiablePrice Can price of the product be negotiated
    function updateProduct(
            uint storeFrontId,
            uint productId, 
            uint pricePerUnit, 
            uint amountProduced, 
            bool hasNegotiablePrice) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyNaturalNumber(productId)
        onlyWhenInitialized
        onlyValidItemEntry(pricePerUnit, amountProduced)
        onlyMember
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
        onlyExistingProduct(storeFrontId, productId) {

        _inventory._updateProduct(productId, pricePerUnit, amountProduced, hasNegotiablePrice, TIME_BETWEEN_UPDATES);
    }


    /// @dev Market registers purchase in the member base
    /// @notice Invoices have expiration date
    /// @param invoice Invoice details
    /// @param nonce seller's nonce
    /// @param signature Signed invoice
    /// @return True on success, false otherwise
    function registerPurchaseWithInvoice (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        onlyNaturalNumber(nonce)
        onlyValidInvoice(invoice)
        onlyOnValidMarketMembership(invoice.seller, msg.sender)
    returns (bool) {

        require(_storeFronts._isStoreFrontExisting(invoice.seller, invoice.storeFrontId) 
            && !_storeFronts._isStoreFrontDiabled(invoice.seller, invoice.storeFrontId));
            
        require(_inventory._isProductExisting(invoice.productId), '7');

        require(_validateProductPurchase(invoice, nonce, signature));

        _inventory._decreaseAmount(invoice.productId, invoice.amount);

        VotingMemberBase._upMemberVoteWeight(invoice.seller, 5);

        emit LogPurchaseRegistered(invoice.buyer, invoice.seller, msg.sender, invoice.productId);

        return true;
    }

    /// @dev Retrieve all store fronts by page number
    /// @notice Max store fronts per page number is 10. Pages begin from 0.
    /// @param storeOwner Account address of the store owner
    /// @param pageNum Page number
    /// @return Array of up to 10 store fronts
    function getStoreFrontsByPageNum (address storeOwner, uint pageNum) 
        public 
        view 
        onlyValidAddress(storeOwner)
        onlyNaturalNumber(pageNum)
        onlyWhenInitialized
    returns(InventoryLib.StoreFront[] memory) {
        return _storeFronts._getStoreFronts(storeOwner, pageNum);
    }

    /// @dev Retrieve store front information by store front id
    /// @param storeOwner Account address of the store owner
    /// @param storeFrontId Id of the store front
    /// @return Store Front information
    function getStoreFrontById (address storeOwner, uint storeFrontId) 
        public 
        view 
        onlyValidAddress(storeOwner)
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
    returns(InventoryLib.StoreFront memory) {
        return _storeFronts._getStoreFrontById(storeOwner, storeFrontId);
    }

    /// @dev Retrieve all products by page number
    /// @param storeOwner Account address of the store owner
    /// @param storeFrontId Id of the store front
    /// @param pageNum Number of the page
    /// @return Array of up to 10 products
    function getProductsByPageNum (address storeOwner, uint storeFrontId, uint pageNum) 
        public 
        view 
        onlyNaturalNumber(storeFrontId)
        onlyNaturalNumber(pageNum)
        onlyValidAddress(storeOwner)
        onlyWhenInitialized
    returns(InventoryLib.Product[] memory) {
        require(_storeFronts._isStoreFrontExisting(storeOwner, storeFrontId), '6');

        return _inventory._getProductsByPageNum(storeFrontId, pageNum);
    }

    /// @dev Retrieve all products Ids by page number
    /// @param storeOwner Account address of the store owner
    /// @param storeFrontId Id of the store front
    /// @param pageNum Number of the page
    /// @return Array of up to 10 product Ids
    function getProductIdsByPageNum (address storeOwner, uint storeFrontId, uint pageNum) 
        public 
        view 
        onlyNaturalNumber(storeFrontId)
        onlyNaturalNumber(pageNum)
        onlyWhenInitialized
    returns(uint[] memory) {
        require(_storeFronts._isStoreFrontExisting(storeOwner, storeFrontId), '6');

        return _inventory._getProductIdsByPageNum(storeFrontId, pageNum);
    }

    /// @dev Retrieve product information by it's id
    /// @param productId id of the product
    /// @return Information associated with product
    function getProductById (uint productId) onlyNaturalNumber(productId) public view returns(InventoryLib.Product memory) {
        return _inventory._products[productId];
    }

    /// @dev Retrieve membership info
    /// @param accAddress Account address of the owner
    /// @return Is member, is the owner
    function getMembershipInfo(address accAddress) external view returns (bool isMember, bool isOwner){
        (isMember, isOwner) = VotingMemberBase._getMembershipInfo(accAddress);
    }

    function () payable external {
        revert();
    }
}
        