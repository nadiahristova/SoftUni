pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./MarketMemberBase.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../libraries/InventoryLib.sol";

import "../interfaces/BaseMarketInterface.sol";
import "../interfaces/ProducerBaseInterface.sol";


// todo upgradable member base
contract ProducerBase is MarketMemberBase, InvoiceProductPurchaseValidator {

    using InventoryLib for InventoryLib.StoreFronts;
    using InventoryLib for InventoryLib.ProductStock;

    InventoryLib.StoreFronts _storeFronts;
    InventoryLib.ProductStock _inventory;

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 255;//check option for readonly

    uint constant TIME_BETWEEN_UPDATES = 5 minutes;// move to init or make one more layer on top of product base


    modifier onlyExistingStoreFront (uint storeFrontId) {
         require(_storeFronts._isStoreFrontExisting(msg.sender, storeFrontId), 'Not existing');
         _;
    }

    modifier onlyOnNotDisabledStoreFront(uint storeFronId) {
        require(!_storeFronts._isStoreFrontDiabled(msg.sender, storeFronId), 'Enabled');
        _;
    }

    modifier onlyExistingProduct (uint storeFrontId, uint productId) {
         require(_inventory._isProductExisting(productId), 'Not existing');
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


    function initialize (
            uint[2] memory defaultCampaignTimePeriods, 
            uint decisiveVoteWeightProportion, 
            uint decisiveVoteCountProportion,
            uint initialOwnerVoteWeight) 
        public
        onlyOwner {

        require(!_isInitialized);
        
        super._initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, 
            initialOwnerVoteWeight, new bytes32[](0),  new uint[](0));
    }

    ///@dev Used by the owner to forcefully remove member
    ///@param accAddress Address of owner 
    function revokeMembershipImmediately(address accAddress) 
        external 
        onlyValidAddress(accAddress)
        onlyWhenInitialized
        onlyOwner
        onlyWhenMember(accAddress, true)
    returns (bool) {

        require(_memberBase._revokeMembershipForced(accAddress));

        require(_revokeAllMarketMembership(accAddress));

        return true;
    } 

    /// @dev Launches not basic campaign for given member
    /// @notice Only owner can do this, campaign id should be > 2
    /// @param accAddress Member address
    /// @return true if campaign was launched, false otherwise
    function launchCampaign (address accAddress, uint campaignId) 
        public 
        onlyValidAddress(accAddress)
        onlyNaturalNumber(campaignId)
        onlyWhenInitialized
        onlyOwner 
        onlyWhenMember(accAddress, true)
    returns(bool) {
        require(campaignId > 1); 
        require(_availableCampaigns[campaignId].name != 0x0); // campaign is existing

        return _launchCampaign(accAddress, campaignId);
    }

    function registerMember(address accAddress) 
        external 
        onlyWhenInitialized
        onlyMember
        onlyWhenMember(accAddress, false)
    returns (bool) {

        _registerMember(accAddress);

        return true;
    }

    function supportMember( // support campaign
            address accAddress,
            uint248 votingCampaignId)
        external 
        onlyValidAddress(accAddress) 
        onlyNaturalNumber(votingCampaignId)
        onlyWhenInitialized
        onlyMember
    {
        address supporter = msg.sender;
        
        require(supporter != accAddress);

        _supportMember(accAddress, votingCampaignId);
    } 

    /** Store Front Logic */
    function addStoreFront() onlyWhenInitialized onlyMember external returns (bool) {
        
        address storeOwner = msg.sender;

        require(_storeFronts._addStoreFront(storeOwner, MAX_STOREFRONTS_PER_STORE));

        _upMemberVoteWeight(storeOwner, 2);

        return true;
    }

    //notify markets that store front is added, removed !!!!
    function removeStoreFront(uint storeFrontId) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;

        _storeFronts._removeStoreFront(storeOwner, storeFrontId);

        _downMemberVoteWeight(storeOwner, 2);
    }

    function disableStoreFront(uint storeFrontId) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyWhenInitialized
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;

        _storeFronts._disableStoreFront(storeOwner, storeFrontId);
    }

    function enableStoreFront(uint storeFrontId) 
        external 
        onlyNaturalNumber(storeFrontId)
        onlyMember
        onlyExistingStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;
        
        require(_storeFronts._isStoreFrontDiabled(storeOwner, storeFrontId));

        _storeFronts._enableStoreFront(storeOwner, storeFrontId);
    }

    function pushStoreFrontToMarket(BaseMarketInterface market, uint storeFrontId) 
            external
            onlyMember  
            onlyExistingStoreFront(storeFrontId)
            onlyOnNotDisabledStoreFront(storeFrontId)
            onlyOnValidMarketMembership(msg.sender, address(market))
    {
        address storeOwner = msg.sender;

        require(market.memberBaseAddStoreFront(storeOwner, storeFrontId));

        _upMemberVoteWeight(storeOwner, 5);

        emit LogStoreFrontShared(storeOwner, storeFrontId, address(market));
    }

    /** Product Logic */
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
        onlyOnNotDisabledStoreFront(storeFrontId)
    returns(bool) {
        //require(recognisedProducts[productDescriptionId]);

        address storeOwner = msg.sender; 
        
        uint256 newProductId = _inventory._addProductToStoreFront(storeOwner, storeFrontId, specificationId, 
            pricePerUnit, amount, hasNegotiablePrice, INVENTORY_CAP_PER_STOREFRONT);

        _upMemberVoteWeight(storeOwner, 1);

        emit LogProductAddedToStoreFront(storeOwner, storeFrontId, newProductId);
        
        return true;
    }

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
        onlyExistingProduct(storeFrontId, productId)
    {
        address storeOwner = msg.sender;
        
        _inventory._removeProductFromStoreFront(storeOwner, storeFrontId, productId);

        _downMemberVoteWeight(storeOwner, 1);

        emit LogProductRemovedFromStoreFront(storeOwner, storeFrontId, productId);
    }

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
        onlyExistingProduct(storeFrontId, productId)
    {
        _inventory._updateProduct(productId, pricePerUnit, amountProduced, hasNegotiablePrice, TIME_BETWEEN_UPDATES);
    }

    function registerPurchaseWithInvoice (
            address client,
            uint256 storeFronId,
            uint256 amount,
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        onlyNaturalNumber(nonce)
        onlyOnValidMarketMembership(invoice.seller, msg.sender)
    returns (bool) {

        require(_hasValidState(invoice));

        require(isMember(invoice.seller));
        
        require(_storeFronts._isStoreFrontExisting(invoice.seller, storeFronId) 
            && !_storeFronts._isStoreFrontDiabled(invoice.seller, storeFronId));
        require(_inventory._isProductExisting(invoice.productId));

        address market = msg.sender;

        bool isValidInvoice = _validateProductPurchase(nonce, invoice, signature);
        
        require(isValidInvoice);

        _inventory._decreaseAmount(invoice.productId, amount);

        _upMemberVoteWeight(invoice.seller, 5);

        emit LogPurchaseRegistered(client, invoice.seller, market, invoice.productId);

        return true;
    }

    function _getProduct (uint productId) private view returns(InventoryLib.Product memory) {
        return _inventory._products[productId];
    }

    function () payable external {
        revert();
    }
}
        