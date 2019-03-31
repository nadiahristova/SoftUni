pragma solidity >=0.5.2 <0.6.0;

pragma experimental ABIEncoderV2;

import "./MarketMemberBase.sol";
import "./InvoiceProductPurchaseValidator.sol";

import "../interfaces/MarketInterface.sol";
import "../interfaces/ProducerBaseInterface.sol";

contract ProducerBase is MarketMemberBase, InvoiceProductPurchaseValidator {

    uint constant MAX_STOREFRONTS_PER_STORE = 25;
    uint constant INVENTORY_CAP_PER_STOREFRONT = 255;//check option for readonly
    uint constant TIME_BETWEEN_UPDATES = 2 days;// move to init or make one more layer on top of product base

    uint _productIds;
    uint _storeFrontIds;

    enum MeasurementUnit { 
        Count, // number of items
        Moles,
        Grams, 
        Milimeters,
        Milliliters, 
        SquareMilimeters,
        CubicMilimeters 
    }

    mapping(uint => bool) recognisedProducts; 

    struct ProductInfo {// these two in another contract
        bool isSupported;
        bytes32 name;
        bytes32 category;
        bytes32 description;
        // buyers
    }

    mapping(bytes32 => uint) productDescriptionSignatureToProductId; // this one too
    //mapping(uint => ProductInfo) recognisedProducts; // these two in another contract

    struct Store {
        StoreFront[] storeFronts;
        // map StoreFrontId to index in storeFronts + 1, 0 - indicates that such StoreFront does not exist
        mapping (uint => uint) storeFrontsMap;
    }

    struct StoreFront {
        uint id;
        bool isDisabled;
        uint248 createdAt;
        bytes32 name;
        // turn into dynamic 2 dimentional array
        Product[] products;
        mapping (uint => uint) productsMap;
    }

    struct Product {
        //bytes32 name; 
        //bytes32 about; // hashed name, category and description => maybe move in some 
        // kind of library or move it to market contract
        //MeasurementUnit measurementUnit;
        uint id;
        uint productDescriptionId;
        uint editedAt;
        uint amount;
        uint248 pricePerUnit;
        bool hasNegotiablePrice;
        //ProductStandartInterface productStandart;
    }
        
    mapping(address => Store) stores;

    modifier onlyExistingStoreFront (uint storeFrontId) {
         require(stores[msg.sender].storeFrontsMap[storeFrontId] != 0);
         _;
    }

    modifier onlyOnNotDisabledStoreFront(uint storeFronId) {
        require(!stores[msg.sender].storeFronts[storeFronId].isDisabled);
        _;
    }

    modifier onlyExistingProduct (uint storeFrontId, uint productId) {
         require(stores[msg.sender].storeFronts[storeFrontId].productsMap[productId] != 0);
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
        uint productLocalId); //, uint indexed productDescriptionId
    event LogProductPricePerUnitUpdated(
        address indexed ownerAddress, uint indexed storeFrontId, uint indexed productLocalId, 
        uint oldPrice, uint newPrice);
    event LogProductProducedAmountUpdated(
        address indexed ownerAddress, uint indexed storeFrontId, uint indexed productLocalId, 
        uint oldAmount, uint newAmount);
    event LogProductPriceNegotiabilityUpdated(
        address indexed ownerAddress, uint indexed storeFrontId, uint indexed productLocalId, 
        bool priceIsNegotiable);

    event LogPurchase(address client, address producer, address market, uint256 productId);



    /** Store Front Logic */
    function addStoreFront(bytes32 name) onlyMember external {
        require(name != 0x0);
        
        address storeOwner = msg.sender;

        require(stores[storeOwner].storeFronts.length < MAX_STOREFRONTS_PER_STORE);

        uint newStoreFrontId = _storeFrontIds + 1;

        StoreFront memory newStoreFront;

        newStoreFront.id = newStoreFrontId;
        newStoreFront.createdAt = uint248(now);
        newStoreFront.name = name;

        uint storeFrontMapIndex = stores[storeOwner].storeFronts.push(newStoreFront) + 1;
        stores[storeOwner].storeFrontsMap[newStoreFrontId] = storeFrontMapIndex;

        _storeFrontIds = newStoreFrontId;

        upMemberVoteWeight(storeOwner, 2);

        emit LogStoreFrontAdded(storeOwner, newStoreFrontId);
    }

    //notify markets that store front is added, removed
    // notify markets that item is added, removed
    function removeStoreFront(uint storeFrontId) 
        external 
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;
        uint storeFrontCount = stores[storeOwner].storeFronts.length;

        uint storeFrontIndex_removed = stores[storeOwner].storeFrontsMap[storeFrontId] - 1;

        assert(storeFrontIndex_removed < storeFrontCount);

        // swap places of storefronts
        if(storeFrontCount > 1) {
            StoreFront storage endStoreFront = stores[storeOwner].storeFronts[storeFrontCount - 1];

            uint endStoreFrontId = endStoreFront.id;

            stores[storeOwner].storeFrontsMap[endStoreFrontId] = storeFrontIndex_removed + 1;
            stores[storeOwner].storeFronts[storeFrontIndex_removed] = endStoreFront;
        }

        // remove last element from the stack
        stores[storeOwner].storeFronts.pop();
        delete stores[storeOwner].storeFrontsMap[storeFrontId];

        downMemberVoteWeight(storeOwner, 5);

        emit LogStoreFrontRemoved(storeOwner, storeFrontId);
    }

    function disableStoreFront(uint storeFrontId) 
        external 
        onlyMember 
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;

        stores[storeOwner].storeFronts[storeFrontId].isDisabled = true;

        emit LogStoreFrontDisabled(storeOwner, storeFrontId);
    }

    function enableStoreFront(uint storeFrontId) 
        external 
        onlyMember
        onlyExistingStoreFront(storeFrontId)
    {
        address storeOwner = msg.sender;
        
        require(stores[storeOwner].storeFronts[storeFrontId].isDisabled);

        delete stores[storeOwner].storeFronts[storeFrontId].isDisabled;

        emit LogStoreFrontEnabled(storeOwner, storeFrontId);
    }

    function pushStoreFrontToMarket(MarketInterface market, uint storeFrontId) 
            external
            onlyMember  
            onlyExistingStoreFront(storeFrontId)
            onlyOnNotDisabledStoreFront(storeFrontId)
            onlyOnValidMarketMembership(msg.sender, address(market))
    {
        address storeOwner = msg.sender;

        require(market.notifyForStoreFrontDeletion(storeOwner, storeFrontId));

        upMemberVoteWeight(msg.sender, 5);
    }

    /** Product Logic */
    // add member => add first store front
    function addProductToStoreFront(
            uint storeFrontId,
            uint productDescriptionId, 
            uint pricePerUnit, 
            uint amountProduced, 
            bool hasNegotiablePrice) 
        external
        onlyValidItemEntry(pricePerUnit, amountProduced)
        onlyMember
        onlyExistingStoreFront(storeFrontId)
        onlyOnNotDisabledStoreFront(storeFrontId)
        returns(uint) {
            //require(recognisedProducts[productDescriptionId]);

            address storeOwner = msg.sender;
            
            uint newProductIndex = stores[storeOwner].storeFronts[storeFrontId].products.length;

            assert(newProductIndex < INVENTORY_CAP_PER_STOREFRONT);

            uint newProductId = _productIds;

            stores[storeOwner].storeFronts[storeFrontId].products.push(
                Product({
                    id: ++newProductId,
                    productDescriptionId: productDescriptionId, 
                    amount: amountProduced, 
                    editedAt: now,
                    pricePerUnit: uint248(pricePerUnit), 
                    hasNegotiablePrice: hasNegotiablePrice}));

            stores[storeOwner].storeFronts[storeFrontId].productsMap[newProductId] = newProductIndex + 1;
            _productIds = newProductId;

            upMemberVoteWeight(storeOwner, 1);

            emit LogProductAddedToStoreFront(storeOwner, storeFrontId, newProductId);

            return newProductId;
    }

    // function getProductInfo(address accAddress, uint256 storeFrontId, uint256 productId)
    //     public 
    //     onlyExistingStoreFront(accAddress, storeFrontId)
    //     onlyOnNotDisabledStoreFront(accAddress, storeFrontId)
    //     onlyExistingProduct(accAddress, storeFrontId, productId)
    // returns (Product memory){

    // }

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
        uint productCount = stores[storeOwner].storeFronts[storeFrontId].products.length;

        uint productIndex_deleted = stores[storeOwner].storeFronts[storeFrontId].productsMap[productId] - 1;

        assert(productIndex_deleted < productCount);

        if(productCount > 1) {
            Product memory endProduct = stores[storeOwner].storeFronts[storeFrontId].products[productCount - 1];

            uint endProductId = endProduct.id;

            stores[storeOwner].storeFronts[storeFrontId].productsMap[endProductId] = productIndex_deleted + 1;

            stores[storeOwner].storeFronts[storeFrontId].products[productIndex_deleted] = endProduct;
        }

        stores[storeOwner].storeFronts[storeFrontId].products.pop();
        delete stores[storeOwner].storeFronts[storeFrontId].productsMap[productId];

        downMemberVoteWeight(storeOwner, 1);

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
        address storeOwner = msg.sender;

        uint productIndex = stores[storeOwner].storeFronts[storeFrontId].productsMap[productId] - 1;
        require(stores[storeOwner].storeFronts[storeFrontId].products[productIndex].editedAt + TIME_BETWEEN_UPDATES < now);

        Product memory product = stores[storeOwner].storeFronts[storeFrontId].products[productIndex];

        if(product.amount != amountProduced) {
            stores[storeOwner].storeFronts[storeFrontId].products[productIndex].amount = amountProduced;

            emit LogProductPricePerUnitUpdated(storeOwner, storeFrontId, productId, product.amount, amountProduced);
        }
      
        if(product.pricePerUnit != pricePerUnit) {
            stores[storeOwner].storeFronts[storeFrontId].products[productIndex].pricePerUnit = uint248(pricePerUnit);

            emit LogProductProducedAmountUpdated(storeOwner, storeFrontId, productId, product.pricePerUnit, pricePerUnit);
        }

        if(product.hasNegotiablePrice != hasNegotiablePrice) {
            stores[storeOwner].storeFronts[storeFrontId].products[productIndex].hasNegotiablePrice = hasNegotiablePrice;

            emit LogProductPriceNegotiabilityUpdated(storeOwner, storeFrontId, productId, hasNegotiablePrice);
        }

        stores[storeOwner].storeFronts[storeFrontId].products[productIndex].editedAt = now;
    }

    function registerPurchaseWithInvoice (
            address producer, 
            address buyer,
            uint256 productId,
            uint256 storeFrontId,
            uint256 amount,
            uint256 pricePerUnit,
            uint256 validUntil,
            uint256 nonce, 
            bytes memory signature) 
        public 
        onlyNaturalNumber(amount)
        onlyNaturalNumber(pricePerUnit)
        onlyNaturalNumber(nonce)
        onlyValidAddress(producer)
        onlyValidAddress(buyer)
        onlyOnValidMarketMembership(producer, msg.sender)
    returns (bool) {
        require(isMember(producer));

        //require(stores[producer].storeFrontsMap[storeFrontId] != 0);
        require(!stores[producer].storeFronts[storeFrontId].isDisabled);
        require(stores[producer].storeFronts[storeFrontId].productsMap[productId] != 0);

        address market = msg.sender;
        
        uint productIndex = stores[producer].storeFronts[storeFrontId].productsMap[productId] - 1;

        bool isValidInvoice = _validateProductPurchase(producer, buyer, 
            market, productId, storeFrontId, amount, pricePerUnit, validUntil, nonce, signature);
        
        require(isValidInvoice);

          // TODO: test
        Product storage product = stores[producer].storeFronts[storeFrontId].products[productIndex];

        uint availableAmount = product.amount;

        require(availableAmount >= amount);

        availableAmount -= amount;

        product.amount = availableAmount;

        upMemberVoteWeight(producer, 5);

        emit LogPurchase(buyer, producer, market, productId);

        return true;
    }

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function revokeMembership(address accAddress) 
        public 
        onlyOwner
        onlyValidAddress(accAddress)
    returns (bool) {
        return _revokeMembership(accAddress);
    } 

    function _getProduct (address accAddress, uint256 storeFrontId, uint productId) private returns(Product memory) {
        uint productIndex = stores[accAddress].storeFronts[storeFrontId].productsMap[productId] - 1;

        require(productIndex >= 0);

        return stores[accAddress].storeFronts[storeFrontId].products[productIndex];
    }
}
        