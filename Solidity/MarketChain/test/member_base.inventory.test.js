var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("BaseMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers');
const { expect } = require('chai');


contract('ProducerBase', function ([owner, storeOwner1, storeOwner2, not_a_member]) {

    let memberBase;
    let market;

    const votingCampaigns = {
        GRANT_MEMBERSHIP: new BN(1),
        REVOKE_MEMBERSHIP: new BN(2)
    }

    const gmCampaignActivePeriod = time.duration.days(30)
    const rmCampaignActivePeriod = time.duration.days(20)

    const defaultProduct = {
        specificationId: new BN(1),
        pricePerUnit: new BN(100),
        amount: new BN(15),
        hasNegotiablePrice: false
    }

    const product = {
        specificationId: new BN(1),
        pricePerUnit: new BN(150),
        amount: new BN(1000),
        hasNegotiablePrice: true
    }

    const onlyOwner_msg = 'Only owner'
    const unauthorised_msg = 'Unauthorised'
    const registered_msg = 'Registered'
    const not_a_partner_msg = 'Not partner'
    const not_existing_SF = 'Not existing'
    const enabled_SF_msg = 'Enabled'
    const wait_time_msg = 'Wait time'

    const registerMember = async function (supporter, candidateMember, memberBase){

        await memberBase.requestMembership({ from: candidateMember })
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: supporter })

        await memberBase.supportMember(candidateMember, 1, { from: supporter })

        await memberBase.registerMember(candidateMember, { from: supporter })
    }

    const registerMarket = async function (owner, memberBase) {

        return await memberBase.addMarketPartner(market.address, { from: owner })
    }

    const addDefaultProduct = async function (storeFrontId, storeOwner, memberBase) {
        
        return await memberBase.addProductToStoreFront(storeFrontId, defaultProduct.specificationId, defaultProduct.pricePerUnit, 
            defaultProduct.amount, defaultProduct.hasNegotiablePrice, { from: storeOwner });
    }

    const updateProduct = async function (storeFrontId, productId, storeOwner, memberBase) {
        
        return await memberBase.updateProduct(storeFrontId, productId, product.pricePerUnit, 
            product.amount, product.hasNegotiablePrice, { from: storeOwner });
    }

    before(() => {
        web3.eth.defaultAccount = storeOwner1;
    });

    beforeEach(async () => {
        market = await Market.deployed()

        memberBase = await ProducerBase.new()
        await memberBase.initialize([gmCampaignActivePeriod, rmCampaignActivePeriod], 2, 3, 50, [], [])

        await registerMember(owner, storeOwner1, memberBase)
        await registerMarket(owner, memberBase)
    });

    it("should be able to open store front", async () => {
        const expectedStoreFrontId = new BN(1);

        const result = await memberBase.addStoreFront({ from: storeOwner1 });

        expectEvent.inLogs(result.logs, 'LogStoreFrontCreated', { ownerAddress: storeOwner1, storeFrontId: expectedStoreFrontId })
    })

    it("should be able to increase store front ids correctly", async () => {
        const expectedStoreFrontId = new BN(3);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await memberBase.addStoreFront({ from: owner });
        const result = await memberBase.addStoreFront({ from: storeOwner1 });

        expectEvent.inLogs(result.logs, 'LogStoreFrontCreated', { ownerAddress: storeOwner1, storeFrontId: expectedStoreFrontId })
    })

    it("should be able to increase store front ids correctly", async () => {
        const expectedStoreFrontId = new BN(3);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await memberBase.addStoreFront({ from: owner });
        const result = await memberBase.addStoreFront({ from: storeOwner1 });

        expectEvent.inLogs(result.logs, 'LogStoreFrontCreated', { ownerAddress: storeOwner1, storeFrontId: expectedStoreFrontId })
    })

    it("should NOT be able to create store front on not-a-member request", async () => {
        
        await shouldFail.reverting(memberBase.addStoreFront({ from: not_a_member }), unauthorised_msg);
    })

    it("should be able to remove existing store front on Store Front's owner request", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        const result = await memberBase.removeStoreFront(expected_removedSFId, { from: storeOwner1 });

        expectEvent.inLogs(result.logs, 'LogStoreFrontRemoved', { ownerAddress: storeOwner1, storeFrontId: expected_removedSFId })
    })

    it("should NOT be able to remove existing store front on not-a-member request", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(memberBase.removeStoreFront(expected_removedSFId, { from: not_a_member }), unauthorised_msg);
    })

    it("should NOT be able to remove existing store front on other than SF owner request", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(memberBase.removeStoreFront(expected_removedSFId, { from: owner }), not_existing_SF);
    })

    it("should NOT be able to remove none existing store front", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(memberBase.removeStoreFront(expected_removedSFId + 1, { from: storeOwner1 }), not_existing_SF);
    })

    it("should be able to disable store front", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        const { logs } = await memberBase.disableStoreFront(expected_removedSFId, { from: storeOwner1 });

        expectEvent.inLogs(logs, 'LogStoreFrontDisabled', { ownerAddress: storeOwner1, storeFrontId: expected_removedSFId })
    })

    it("should NOT be able to disable none existing store front", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(memberBase.removeStoreFront(expected_removedSFId + 1, { from: storeOwner1 }), not_existing_SF);
    })

    it("should NOT be able to disable existing store front on other than SF owner's request", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(memberBase.removeStoreFront(expected_removedSFId, { from: owner }), not_existing_SF);
    })

    it("should NOT be able to enable already enabled store front", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(memberBase.enableStoreFront(expected_removedSFId, { from: storeOwner1 }), enabled_SF_msg);
    })

    it("should be able to enable already disabled store front", async () => {
        const expected_removedSFId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        await memberBase.disableStoreFront(expected_removedSFId, { from: storeOwner1 });

        const { logs } = await memberBase.enableStoreFront(expected_removedSFId, { from: storeOwner1 });

        expectEvent.inLogs(logs, 'LogStoreFrontEnabled', { ownerAddress: storeOwner1, storeFrontId: expected_removedSFId })
    })

    it("should be able to add product", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });

        const { logs } = await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        expectEvent.inLogs(logs, 'LogProductAddedToStoreFront', { ownerAddress: storeOwner1, storeFrontId: expected_SFId, productId: expected_ProductId })
    })

    it("should NOT be able to enable to add product when not a member", async () => {
        const expected_SFId = 1;

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(addDefaultProduct(expected_SFId, not_a_member, memberBase), unauthorised_msg)
    })

    it("should NOT be able to add product to none existing store front", async () => {
        const expected_SFId = 1;

        await memberBase.addStoreFront({ from: storeOwner1 });

        await shouldFail.reverting(addDefaultProduct(expected_SFId + 1, storeOwner1, memberBase, { from: not_a_member }), unauthorised_msg)
    })

    it("should NOT be able to add product to disabled store front", async () => {
        const expected_SFId = 1;

        await memberBase.addStoreFront({ from: storeOwner1 });
        await memberBase.disableStoreFront(expected_SFId, { from: storeOwner1 });

        await shouldFail.reverting(addDefaultProduct(expected_SFId + 1, storeOwner1, memberBase, { from: not_a_member }))
    })

    it("should be able to remove product", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        const { logs } = await memberBase.removeProductFromStoreFront(expected_SFId, expected_ProductId, { from: storeOwner1 });

        expectEvent.inLogs(logs, 'LogProductRemovedFromStoreFront', { ownerAddress: storeOwner1, storeFrontId: expected_SFId, productId: expected_ProductId })
    })

    it("should NOT be able to remove product on other than SF's owner request", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(memberBase.removeProductFromStoreFront(expected_SFId, expected_ProductId, { from: owner }), unauthorised_msg);
    })

    it("should NOT be able to remove product when Store Front is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(memberBase.removeProductFromStoreFront(expected_SFId + 1, expected_ProductId, { from: storeOwner1 }), not_existing_SF);
    })

    it("should NOT be able to remove product when product is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(memberBase.removeProductFromStoreFront(expected_SFId, expected_ProductId + 1, { from: storeOwner1 }), not_existing_SF);
    })

    it("should be able to update product", async () => {
        const expected_SFId = 1;
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await time.increase(time.duration.seconds(5 * 60 + 1));

        const { logs } = await updateProduct(expected_SFId, expected_ProductId, storeOwner1, memberBase) 

        expectEvent.inLogs(logs, 'LogProductPricePerUnitUpdated', { productId: expected_ProductId, oldPrice: defaultProduct.pricePerUnit, newPrice: product.pricePerUnit })
        expectEvent.inLogs(logs, 'LogProductProducedAmountUpdated', { productId: expected_ProductId, oldAmount: defaultProduct.amount, newAmount: product.amount })
        expectEvent.inLogs(logs, 'LogProductPriceNegotiabilityUpdated', { productId: expected_ProductId, priceIsNegotiable: product.hasNegotiablePrice })
    })

    it("should NOT be able to update product before 5 mins had passed", async () => {
        const expected_SFId = 1;
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(updateProduct(expected_SFId, expected_ProductId, storeOwner1, memberBase), wait_time_msg) 
    })

    it("should NOT be able to update product on other than SF's owner request", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(updateProduct(expected_SFId, expected_ProductId, owner, memberBase), unauthorised_msg);
    })

    it("should NOT be able to update product when Store Front is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(updateProduct(expected_SFId + 1, expected_ProductId, storeOwner1, memberBase), not_existing_SF);
    })

    it("should NOT be able to update product when product is non existing", async () => {
        const expected_SFId = new BN(1);
        const expected_ProductId = new BN(1);

        await memberBase.addStoreFront({ from: storeOwner1 });
        await addDefaultProduct(expected_SFId, storeOwner1, memberBase);

        await shouldFail.reverting(updateProduct(expected_SFId, expected_ProductId + 1, storeOwner1, memberBase), not_existing_SF)
    })
})