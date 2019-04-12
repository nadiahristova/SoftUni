var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("RegionalMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers');
const { shared_func, MSG, ENUMS } = require('./_utils.js')
const { VOTING_CAMPAIGN } = ENUMS


contract('ProducerBase', function ([owner, storeOwner, storeOwner2, not_a_member]) {

    let member_base;
    let market;

    let default_product = shared_func.default_product;
    let product = shared_func.product1;


    const updateProduct = async function (storeFrontId, productId, storeOwner, member_base) {
        return await member_base.updateProduct(storeFrontId, productId, product.pricePerUnit, 
            product.amount, product.hasNegotiablePrice, { from: storeOwner });
    }

    const getFirstIndexValue =  (item) => { return parseInt(item[0]) }

    const areArraysIdentical = function (a, b) {
        var i = a.length;
        if (i != b.length) return false;
        while (i--) {
            if (a[i] !== b[i]) return false;
        }
        return true;
    };

    const compareProductInfo = async function (product1, product2) {
        await assert.equal(product1.specificationId == product2.specificationId, true, "specification id did not match")
        await assert.equal(product1.amount == product2.amount, true, "amount did not match")
        await assert.equal(product1.pricePerUnit == product2.pricePerUnit, true, "pricePerUnit did not match")
        await assert.equal(product1.hasNegotiablePrice == product2.hasNegotiablePrice, true, "hasNegotiablePrice did not match") 
    }

    before(() => {
        web3.eth.defaultAccount = storeOwner;
    });

    beforeEach(async () => {
        market = await Market.deployed()

        member_base = await ProducerBase.new()
        await shared_func.initializeProducerBase(member_base)

        await shared_func.registerMember(owner, storeOwner, member_base)
        await shared_func.registerMarket(owner, market, member_base)
    });
    
    it("should be able to open store front", async () => {
        const expectedStoreFrontId = new BN(1);

        const result = await member_base.addStoreFront({ from: storeOwner });

        expectEvent.inLogs(result.logs, 'LogStoreFrontCreated', { ownerAddress: storeOwner, storeFrontId: expectedStoreFrontId })
    })

    it("should be able to increase store front ids correctly", async () => {
        const expectedStoreFrontId = new BN(3);

        await member_base.addStoreFront({ from: storeOwner });
        await member_base.addStoreFront({ from: owner });
        const result = await member_base.addStoreFront({ from: storeOwner });

        expectEvent.inLogs(result.logs, 'LogStoreFrontCreated', { ownerAddress: storeOwner, storeFrontId: expectedStoreFrontId })
    })

    it("should be able to increase store front ids correctly", async () => {
        const expectedStoreFrontId = new BN(3);

        await member_base.addStoreFront({ from: storeOwner });
        await member_base.addStoreFront({ from: owner });
        const result = await member_base.addStoreFront({ from: storeOwner });

        expectEvent.inLogs(result.logs, 'LogStoreFrontCreated', { ownerAddress: storeOwner, storeFrontId: expectedStoreFrontId })
    })

    it("should NOT be able to create store front on not-a-member request", async () => {
        
        await shouldFail.reverting(member_base.addStoreFront({ from: not_a_member }), MSG.UNAUTHORISED);
    })

    it("should be able to remove existing store front on Store Front's owner request", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        const result = await member_base.removeStoreFront(expected_removedSFId, { from: storeOwner });

        expectEvent.inLogs(result.logs, 'LogStoreFrontRemoved', { ownerAddress: storeOwner, storeFrontId: expected_removedSFId })
    })

    it("should NOT be able to remove existing store front on not-a-member request", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(member_base.removeStoreFront(expected_removedSFId, { from: not_a_member }), MSG.UNAUTHORISED);
    })

    it("should NOT be able to remove existing store front on other than SF owner request", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(member_base.removeStoreFront(expected_removedSFId, { from: owner }), MSG.NOT_EXISTING_STORE_FRONT);
    })

    it("should NOT be able to remove none existing store front", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(member_base.removeStoreFront(expected_removedSFId + 1, { from: storeOwner }), MSG.NOT_EXISTING_STORE_FRONT);
    })

    it("should be able to disable store front", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        const { logs } = await member_base.disableStoreFront(expected_removedSFId, { from: storeOwner });

        expectEvent.inLogs(logs, 'LogStoreFrontDisabled', { ownerAddress: storeOwner, storeFrontId: expected_removedSFId })
    })

    it("should NOT be able to disable none existing store front", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(member_base.removeStoreFront(expected_removedSFId + 1, { from: storeOwner }), MSG.NOT_EXISTING_STORE_FRONT);
    })

    it("should NOT be able to disable existing store front on other than SF owner's request", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(member_base.removeStoreFront(expected_removedSFId, { from: owner }), MSG.NOT_EXISTING_STORE_FRONT);
    })

    it("should NOT be able to enable already enabled store front", async () => {
        const expected_SFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(member_base.enableStoreFront(expected_SFId, { from: storeOwner }), MSG.ENABLED_STORE_FRONT);
    })

    it("should be able to enable already disabled store front", async () => {
        const expected_removedSFId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        await member_base.disableStoreFront(expected_removedSFId, { from: storeOwner });

        const { logs } = await member_base.enableStoreFront(expected_removedSFId, { from: storeOwner });

        expectEvent.inLogs(logs, 'LogStoreFrontEnabled', { ownerAddress: storeOwner, storeFrontId: expected_removedSFId })
    })

    it("should be able to add product", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });

        const { logs } = await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        expectEvent.inLogs(logs, 'LogProductAddedToStoreFront', { ownerAddress: storeOwner, storeFrontId: expected_SFId, productId: expected_ProductId })
    })

    it("should NOT be able to enable to add product when not a member", async () => {
        const expected_SFId = 1;

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(shared_func.addProduct(expected_SFId, not_a_member, member_base, default_product), MSG.UNAUTHORISED)
    })

    it("should NOT be able to add product to none existing store front", async () => {
        const expected_SFId = 1;

        await member_base.addStoreFront({ from: storeOwner });

        await shouldFail.reverting(shared_func.addProduct(expected_SFId + 1, not_a_member, member_base, default_product), MSG.UNAUTHORISED)
    })

    it("should NOT be able to add product to disabled store front", async () => {
        const expected_SFId = 1;

        await member_base.addStoreFront({ from: storeOwner });
        await member_base.disableStoreFront(expected_SFId, { from: storeOwner });

        await shouldFail.reverting(shared_func.addProduct(expected_SFId + 1, not_a_member, member_base, default_product))
    })

    it("should be able to remove product", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        const { logs } = await member_base.removeProductFromStoreFront(expected_SFId, expected_ProductId, { from: storeOwner });

        expectEvent.inLogs(logs, 'LogProductRemovedFromStoreFront', { ownerAddress: storeOwner, storeFrontId: expected_SFId, productId: expected_ProductId })
    })

    it("should NOT be able to remove product on other than SF's owner request", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(member_base.removeProductFromStoreFront(expected_SFId, expected_ProductId, { from: owner }), MSG.UNAUTHORISED);
    })

    it("should NOT be able to remove product when Store Front is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(member_base.removeProductFromStoreFront(expected_SFId + 1, expected_ProductId, { from: storeOwner }), MSG.NOT_EXISTING_STORE_FRONT);
    })

    it("should NOT be able to remove product when product is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(member_base.removeProductFromStoreFront(expected_SFId, expected_ProductId + 1, { from: storeOwner }), MSG.NOT_EXISTING_STORE_FRONT);
    })
    
    it("should be able to update product", async () => {
        const expected_SFId = 1;
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await time.increase(time.duration.seconds(5 * 60 + 1));

        const { logs } = await updateProduct(expected_SFId, expected_ProductId, storeOwner, member_base) 

        expectEvent.inLogs(logs, 'LogProductPricePerUnitUpdated', { productId: expected_ProductId, oldPrice: default_product.pricePerUnit, newPrice: product.pricePerUnit })
        expectEvent.inLogs(logs, 'LogProductProducedAmountUpdated', { productId: expected_ProductId, oldAmount: default_product.amount, newAmount: product.amount })
        expectEvent.inLogs(logs, 'LogProductPriceNegotiabilityUpdated', { productId: expected_ProductId, priceIsNegotiable: product.hasNegotiablePrice })
    })

    it("should NOT be able to update product before 5 mins had passed", async () => {
        const expected_SFId = 1;
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(updateProduct(expected_SFId, expected_ProductId, storeOwner, member_base), MSG.WAIT_TIME) 
    })

    it("should NOT be able to update product on other than SF's owner request", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(updateProduct(expected_SFId, expected_ProductId, owner, member_base), MSG.UNAUTHORISED);
    })

    it("should NOT be able to update product when Store Front is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(updateProduct(expected_SFId + 1, expected_ProductId, storeOwner, member_base), MSG.NOT_EXISTING_STORE_FRONT);
    })

    it("should NOT be able to update product when product is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await member_base.addStoreFront({ from: storeOwner });
        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);

        await shouldFail.reverting(updateProduct(expected_SFId, expected_ProductId + 1, storeOwner, member_base), MSG.NOT_EXISTING_STORE_FRONT)
    })

    it("should be able to retrieve product information", async () => {
        const expected_SFId = 1;
        const expected_ProductId = 1;
        const expected_product_info = default_product;

        await member_base.addStoreFront({ from: storeOwner });

        await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);
        const result = await member_base.getProductById(expected_ProductId);

        await compareProductInfo(result, expected_product_info)
    })

    it("should be able to retrieve product ids corretly by paging ", async () => {
        const expected_SFId = 1;
        const expected_product_info = default_product;

        await member_base.addStoreFront({ from: storeOwner });

        let expected_Product_Ids_P0 = [...Array(11).keys()]
        expected_Product_Ids_P0.shift()

        let expected_Product_Ids_P1 = Array(10).fill(0)
        expected_Product_Ids_P1[0] = 11
        expected_Product_Ids_P1[1] = 12

        for(let i = 0; i < 12; i++) {
            await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);
        }

        const result_P0 = await member_base.getProductIdsByPageNum(storeOwner, expected_SFId, 0);
        const received_Product_Ids_P0 = result_P0.map((id) => parseInt(id.toString()))

        const result_P1 = await member_base.getProductIdsByPageNum(storeOwner, expected_SFId, 1); 
        const received_Product_Ids_P1 = result_P1.map((id) => parseInt(id.toString()))

        assert.equal(areArraysIdentical(expected_Product_Ids_P0, received_Product_Ids_P0), true, 'Product ids for first page are not correct')
        assert.equal(areArraysIdentical(expected_Product_Ids_P1, received_Product_Ids_P1), true, 'Product ids for second page are not correct')
    })

    it("should be able to retrieve products in correct order by paging ", async () => {
        const expected_SFId = 1;
        const expected_product_info = default_product;

        await member_base.addStoreFront({ from: storeOwner });

        let expected_Product_Ids_P0 = [...Array(11).keys()]
        expected_Product_Ids_P0.shift()

        let expected_Product_Ids_P1 = Array(10).fill(0)
        expected_Product_Ids_P1[0] = 11
        expected_Product_Ids_P1[1] = 12

        for(let i = 0; i < 12; i++) {
            await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);
        }

        const result_P0 = await member_base.getProductsByPageNum(storeOwner, expected_SFId, 0); // create store front
        const received_Product_Ids_P0 = result_P0.map(getFirstIndexValue)

        const result_P1 = await member_base.getProductsByPageNum(storeOwner, expected_SFId, 1); // create store front
        const received_Product_Ids_P1 = result_P1.map(getFirstIndexValue)

        assert.equal(areArraysIdentical(expected_Product_Ids_P0, received_Product_Ids_P0), true, 'Products for first page are not in correct order')
        assert.equal(areArraysIdentical(expected_Product_Ids_P1, received_Product_Ids_P1), true, 'Products for second page are not in correct order')
    })

    it("should be able to keep track of product ids corretly with deletion", async () => {
        const expected_SFId = 1;
        const expected_product_info = default_product;
        const removed_productId_1 = 2
        const removed_productId_2 = 5

        await member_base.addStoreFront({ from: storeOwner });

        let expected_Product_Ids_P0 = [...Array(11).keys()]
        expected_Product_Ids_P0.shift()

        let expected_Product_Ids_P1 = Array(10).fill(0)

        for(let i = 0; i < 12; i++) {
            await shared_func.addProduct(expected_SFId, storeOwner, member_base, default_product);
        }

        await member_base.removeProductFromStoreFront(expected_SFId, removed_productId_1, { from: storeOwner });
        await member_base.removeProductFromStoreFront(expected_SFId, removed_productId_2, { from: storeOwner });

        expected_Product_Ids_P0[removed_productId_1-1] = 12
        expected_Product_Ids_P0[removed_productId_2-1] = 11

        const result_P0 = await member_base.getProductIdsByPageNum(storeOwner, expected_SFId, 0);
        const received_Product_Ids_P0 = result_P0.map((id) => parseInt(id.toString()))

        const result_P1 = await member_base.getProductIdsByPageNum(storeOwner, expected_SFId, 1); 
        const received_Product_Ids_P1 = result_P1.map((id) => parseInt(id.toString()))

        assert.equal(areArraysIdentical(expected_Product_Ids_P0, received_Product_Ids_P0), true, 'Product ids for first page are not correct')
        assert.equal(areArraysIdentical(expected_Product_Ids_P1, received_Product_Ids_P1), true, 'Product ids for second page are not correct')
    })
    
    it("should allow a member with a store to retrieve page of 10 store front information at a time", async () => {

        let expected_SF_Ids_P0 = [...Array(11).keys()]
        expected_SF_Ids_P0.shift()

        let expected_SF_Ids_P1 = Array(10).fill(0)
        expected_SF_Ids_P1[0] = 11
        expected_SF_Ids_P1[1] = 12

        for(let i = 0; i < 12; i++) {
            await member_base.addStoreFront({ from: storeOwner }); // create store front
        }

        const result_P0 = await member_base.getStoreFrontsByPageNum(storeOwner, 0); // create store front
        const received_SF_Ids_P0 = result_P0.map(getFirstIndexValue)

        const result_P1 = await member_base.getStoreFrontsByPageNum(storeOwner, 1); // create store front
        const received_SF_Ids_P1 = result_P1.map(getFirstIndexValue)

        assert.equal(areArraysIdentical(expected_SF_Ids_P0, received_SF_Ids_P0), true, 'SF ids for first page are not correct')
        assert.equal(areArraysIdentical(expected_SF_Ids_P1, received_SF_Ids_P1), true, 'SF ids for second page are not correct')
    })

    it("should NOT allow a not-a-member to publish store front to a market", async () => {
        await shouldFail.reverting(member_base.publishStoreFrontToMarket(market.address, 2, { from: not_a_member }), MSG.UNAUTHORISED)
    })

    it("should NOT allow a member to publish store front that does not exist", async () => {
        await shouldFail.reverting(member_base.publishStoreFrontToMarket(market.address, 2, { from: storeOwner }), MSG.NOT_EXISTING_STORE_FRONT)
    })

    it("should NOT allow a member to publish disabled store front", async () => {
        await member_base.addStoreFront({ from: storeOwner });
        await member_base.disableStoreFront(1, { from: storeOwner });

        await shouldFail.reverting(member_base.publishStoreFrontToMarket(market.address, 1, { from: storeOwner }))
    })
})