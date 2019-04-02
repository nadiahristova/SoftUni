pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./MarketMemberBase.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../libraries/InventoryLib.sol";

import "../interfaces/BaseMarketInterface.sol";
import "../interfaces/ProducerBaseInterface.sol";

contract ProducerBase is MarketMemberBase, InvoiceProductPurchaseValidator {

    using InventoryLib for InventoryLib.StoreFronts;
    using InventoryLib for InventoryLib.ProductStock;

    InventoryLib.StoreFronts _storeFronts;
    InventoryLib.ProductStock _inventory;

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 255;//check option for readonly

    uint constant TIME_BETWEEN_UPDATES = 2 days;// move to init or make one more layer on top of product base


    modifier onlyExistingStoreFront (uint storeFrontId) {
         require(_storeFronts._isStoreFrontExisting(msg.sender, storeFrontId));
         _;
    }

    modifier onlyOnNotDisabledStoreFront(uint storeFronId) {
        require(!_storeFronts._isStoreFrontDiabled(msg.sender, storeFronId));
        _;
    }

    modifier onlyExistingProduct (uint storeFrontId, uint productId) {
         require(_inventory._isProductExisting(productId));
         _;
    }

    modifier onlyValidItemEntry(uint pricePerUnit, uint amountProduced) {
        require(pricePerUnit >= 0 && amountProduced > 0);
        _;
    }

    event LogStoreFrontAdded(address indexed ownerAddress, uint indexed storeFrontId); 
    event LogStoreFrontRemoved(address indexed ownerAddress, uint indexed storeFrontId); 
    event LogStoreFrontDisabled(address indexed ownerAddress, uint indexed storeFrontId);
    event LogStoreFrontEnabled(address indexed ownerAddress, uint indexed storeFrontId);
    event LogProductRemovedFromStoreFront(address indexed ownerAddress, uint indexed storeFrontId, 
        uint productLocalId);  
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
            uint initialOwnerVoteWeight, 
            bytes32[] memory campaignNames, 
            uint[] memory campaignTimePeriods) 
        public
        onlyOwner {

        require(!_isInitialized);
        
        super._initialize(defaultCampaignTimePeriods, decisiveVoteWeightProportion, decisiveVoteCountProportion, 
            initialOwnerVoteWeight, campaignNames, campaignTimePeriods);
    }

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function revokeMembership(address accAddress) 
        external 
        onlyOwner
        onlyValidAddress(accAddress)
    returns (bool) {
        return _revokeMembership(accAddress);
    } 

    /** Store Front Logic */
    function addStoreFront(bytes32 name) onlyMember external {
        require(name != 0x0);
        
        address storeOwner = msg.sender;

        uint256 newStoreFrontId = _storeFronts._addStoreFront(storeOwner, name, MAX_STOREFRONTS_PER_STORE);

        _upMemberVoteWeight(storeOwner, 2);

        emit LogStoreFrontAdded(storeOwner, newStoreFrontId);
    }

    //notify markets that store front is added, removed !!!!
    function removeStoreFront(uint storeFrontId) 
        external 
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;

        _storeFronts._removeStoreFront(storeOwner, storeFrontId);

        _downMemberVoteWeight(storeOwner, 5);

        emit LogStoreFrontRemoved(storeOwner, storeFrontId);
    }

    function disableStoreFront(uint storeFrontId) 
        external 
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;

        _storeFronts._disableStoreFront(storeOwner, storeFrontId);

        emit LogStoreFrontDisabled(storeOwner, storeFrontId);
    }

    function enableStoreFront(uint storeFrontId) 
        external 
        onlyMember
        onlyExistingStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;
        
        require(_storeFronts._isStoreFrontDiabled(storeOwner, storeFrontId));

        _storeFronts._enableStoreFront(storeOwner, storeFrontId);

        emit LogStoreFrontEnabled(storeOwner, storeFrontId);
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

        _upMemberVoteWeight(msg.sender, 5);

        emit LogStoreFrontShared(storeOwner, storeFrontId, address(market));
    }

    /** Product Logic */
    // add member => add first store front
    function addProductToStoreFront(
            uint storeFrontId,
            uint specificationId, 
            uint pricePerUnit, 
            uint amount, 
            bool hasNegotiablePrice) 
        external
        onlyValidItemEntry(pricePerUnit, amount)
        onlyMember
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
        returns(uint) {
            //require(recognisedProducts[productDescriptionId]);

            address storeOwner = msg.sender; 
            
            uint256 newProductId = _inventory._addProductToStoreFront(storeOwner, storeFrontId, specificationId, 
                pricePerUnit, amount, hasNegotiablePrice, INVENTORY_CAP_PER_STOREFRONT);

            _upMemberVoteWeight(storeOwner, 1);

            emit LogProductAddedToStoreFront(storeOwner, storeFrontId, newProductId);

            return newProductId;
    }
//notifyForStoreFrontDeletion !!!
    function removeProductFromStoreFront(
            uint storeFrontId,
            uint productId) 
        external
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
        onlyValidItemEntry(pricePerUnit, amountProduced)
        onlyMember
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
        onlyExistingProduct(storeFrontId, productId)
    {
        _inventory._updateProduct(productId, pricePerUnit, amountProduced, hasNegotiablePrice, TIME_BETWEEN_UPDATES);
    }

    function registerPurchaseWithInvoice (
            InvoiceDetails memory invoice,
            uint256 nonce, 
            bytes memory signature) 
        public 
        onlyNaturalNumber(nonce)
        onlyOnValidMarketMembership(invoice.seller, msg.sender)
    returns (bool) {

        require(_hasValidState(invoice));

        require(isMember(invoice.seller));
        
        require(_storeFronts._isStoreFrontExisting(invoice.seller, invoice.storeFrontId) 
            && !_storeFronts._isStoreFrontDiabled(invoice.seller, invoice.storeFrontId));
        require(_inventory._isProductExisting(invoice.productId));

        address market = msg.sender;

        bool isValidInvoice = _validateProductPurchase(invoice, nonce, signature);
        
        require(isValidInvoice);

        _inventory._decreaseAmount(invoice.productId, invoice.amount);

        _upMemberVoteWeight(invoice.seller, 5);

        emit LogPurchaseRegistered(invoice.buyer, invoice.seller, market, invoice.productId);

        return true;
    }

    function _getProduct (uint productId) private view returns(InventoryLib.Product memory) {
        return _inventory._products[productId];
    }
}
        