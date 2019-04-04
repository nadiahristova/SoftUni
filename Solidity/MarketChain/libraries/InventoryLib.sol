pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

library InventoryLib {

    struct Store {
        StoreFront[] storeFronts;
        // map StoreFrontId to index in storeFronts + 1, 0 - indicates that such StoreFront does not exist
        mapping (uint => uint) storeFrontsMap;
    }

    struct StoreFront {
        uint256 id;
        bool isDisabled;
        uint248 createdAt;
    }

    struct StoreFronts {
        uint256 _storeFrontIds;

        mapping(address => Store) stores;
    }


    struct Product {
        //address producer;
        uint256 specificationId;
        //uint256 standartId;
        uint256 editedAt;
        uint256 amount;
        uint248 pricePerUnit;
        bool hasNegotiablePrice;
    }
   
    struct ProductStock {
        uint256 _productIds;

        mapping (uint => Product) _products; // product id to product
        mapping (uint => uint) _productIdsToInventoryIndexMap; // product id to local index in _storeFrontInventory
        mapping (uint => uint[]) _storeFrontInventory; // store front id to product ids
    }
    
    // TODO add standart!

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


    /** Store Front Logic */
    function _addStoreFront(StoreFronts storage self, address storeOwner, uint maxStoreFrontPerStore) 
        internal 
    returns (bool) {

        require(self.stores[storeOwner].storeFronts.length < maxStoreFrontPerStore);

        uint newStoreFrontId = self._storeFrontIds + 1;

        StoreFront memory newStoreFront = StoreFront({ id: newStoreFrontId, isDisabled: false, createdAt: uint248(now) });

        uint256 storeFrontMapIndex = self.stores[storeOwner].storeFronts.push(newStoreFront);
        self.stores[storeOwner].storeFrontsMap[newStoreFrontId] = storeFrontMapIndex;

        self._storeFrontIds = newStoreFrontId;

        emit LogStoreFrontCreated(storeOwner, newStoreFrontId);

        return true;
    }
    

    function _removeStoreFront(StoreFronts storage self, address storeOwner, uint storeFrontId) internal {

        Store storage store = self.stores[storeOwner];

        uint storeFrontCount = store.storeFronts.length;

        uint storeFrontIndex_removed = store.storeFrontsMap[storeFrontId] - 1;

        assert(storeFrontIndex_removed < storeFrontCount);

        // swap places of storefronts
        if(storeFrontCount > 1) {
            StoreFront memory endStoreFront = store.storeFronts[storeFrontCount - 1];

            store.storeFrontsMap[endStoreFront.id] = storeFrontIndex_removed + 1;
            store.storeFronts[storeFrontIndex_removed] = endStoreFront;
        }

        // remove last element from the stack
        store.storeFronts.pop();
        delete store.storeFrontsMap[storeFrontId];

        emit LogStoreFrontRemoved(storeOwner, storeFrontId);
    }

    function _disableStoreFront(StoreFronts storage self, address storeOwner, uint storeFrontId) internal {
        Store storage sf = self.stores[storeOwner];
        
        uint sfIndex = sf.storeFrontsMap[storeFrontId] - 1;
        
        sf.storeFronts[sfIndex].isDisabled = true;

        emit LogStoreFrontDisabled(storeOwner, storeFrontId);
    }

    function _enableStoreFront(StoreFronts storage self, address storeOwner, uint storeFrontId) internal {
        Store storage sf = self.stores[storeOwner];
        
        uint sfIndex = sf.storeFrontsMap[storeFrontId] - 1;
        
        delete sf.storeFronts[sfIndex].isDisabled;

        emit LogStoreFrontEnabled(storeOwner, storeFrontId);
    }

    function _isStoreFrontExisting (StoreFronts storage self, address storeOwner, uint storeFrontId) 
        view
        internal 
    returns (bool) {

        return self.stores[storeOwner].storeFrontsMap[storeFrontId] != 0; // index from the mapping should not be 0
    }

    function _isStoreFrontDiabled (StoreFronts storage self, address storeOwner, uint storeFrontId) 
        view
        internal 
    returns (bool) {
        Store storage sf = self.stores[storeOwner];
        
        uint sfIndex = sf.storeFrontsMap[storeFrontId] - 1;

        return sf.storeFronts[sfIndex].isDisabled;
    }

    /** Product Logic */
    function _addProductToStoreFront (
            ProductStock storage self,
            address storeOwner,
            uint storeFrontId,
            uint specificationId, 
            uint pricePerUnit, 
            uint amount, 
            bool hasNegotiablePrice,
            uint256 maxNumProductsForStoreFront) 
        internal
    returns(uint) {
            
        uint newProductSFIndex = self._storeFrontInventory[storeFrontId].length;

        assert(newProductSFIndex < maxNumProductsForStoreFront);

        uint newProductId = self._productIds + 1;

        self._storeFrontInventory[storeFrontId].push(newProductId);
        self._products[newProductId] = Product({
                //producer: storeOwner,
                specificationId: specificationId, 
                amount: amount, 
                editedAt: now,
                pricePerUnit: uint248(pricePerUnit), 
                hasNegotiablePrice: hasNegotiablePrice});

        self._productIdsToInventoryIndexMap[newProductId] = newProductSFIndex + 1;
        self._productIds = newProductId;

        emit LogProductAddedToStoreFront(storeOwner, storeFrontId, newProductId);

        return newProductId;
    }

    function _removeProductFromStoreFront(
            ProductStock storage self,
            address storeOwner,
            uint storeFrontId,
            uint productId) 
        internal {
            
        uint productCount = self._storeFrontInventory[storeFrontId].length;

        uint productIndex_deleted = self._productIdsToInventoryIndexMap[productId] - 1;

        assert(productIndex_deleted < productCount);

        if(productCount > 1) {
            Product memory endProduct = self._products[productId];

            uint endProductId = self._storeFrontInventory[storeFrontId][productCount - 1];

            self._productIdsToInventoryIndexMap[endProductId] = productIndex_deleted + 1;

            self._products[productIndex_deleted] = endProduct;
        }

        self._storeFrontInventory[storeFrontId].pop();
        delete self._productIdsToInventoryIndexMap[productId];

        emit LogProductRemovedFromStoreFront(storeOwner, storeFrontId, productId);
    }

    function _updateProduct (
            ProductStock storage self,
            uint productId, 
            uint pricePerUnit, 
            uint amountProduced, 
            bool hasNegotiablePrice,
            uint256 timeBetweenUpdates) 
        internal {

        Product memory product = self._products[productId];

        require(product.editedAt + timeBetweenUpdates < now, 'Wait time');

        if(product.amount != amountProduced) {
            self._products[productId].amount = amountProduced;

            emit LogProductProducedAmountUpdated(productId, product.amount, amountProduced);
        }
      
        if(product.pricePerUnit != pricePerUnit) {
            self._products[productId].pricePerUnit = uint248(pricePerUnit);

            emit LogProductPricePerUnitUpdated(productId, product.pricePerUnit, pricePerUnit);
        }

        if(product.hasNegotiablePrice != hasNegotiablePrice) {
            self._products[productId].hasNegotiablePrice = hasNegotiablePrice;

            emit LogProductPriceNegotiabilityUpdated(productId, hasNegotiablePrice);
        }

        self._products[productId].editedAt = now;
    }

    function _decreaseAmount (ProductStock storage self, uint256 productId, uint256 amount) 
        internal {

        uint availableAmount = self._products[productId].amount;

        require(availableAmount >= amount);

        availableAmount -= amount;

        self._products[productId].amount = availableAmount;
    }

    function _getProduct (ProductStock storage self, uint productId) 
        view
        internal 
    returns (Product memory) {
        return self._products[productId];
    }
    
    function _isProductExisting (ProductStock storage self, uint productId) 
        view
        internal 
    returns (bool) {
        return self._productIdsToInventoryIndexMap[productId] != 0;
    }
}