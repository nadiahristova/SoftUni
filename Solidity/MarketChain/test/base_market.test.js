var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("RegionalMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers')
const { shared_func, shared_const, MSG } = require('./_utils.js')

contract('BaseMarket', function ([owner, producer, client, not_a_member]) {

    //shared parameters
    let market; 
    let producer_base;
    let producer_base_adr;

    const default_store_name = web3.utils.fromAscii('Really cool store', 32);
    const location = shared_const.default_location

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await Market.new()
        producer_base = await ProducerBase.deployed()
        producer_base_adr = producer_base.address;

        await shared_func.initializeRegionalMarket(market);

        await shared_func.registerMarketMember(owner, producer, location, market) // register producer address in market
    });

    it("should be able to be affiliated with a producer base ", async () => {
        const expectedEventResult = {
            memberBaseAddress : producer_base.address
        };

        const result = await market.affiliateProducerBase(producer_base.address, { from: owner });

        expectEvent.inLogs(result.logs, 'LogProducerBaseAssigned', { producerBaseAddress: expectedEventResult.memberBaseAddress }, 'LogProducerBaseAssigned event producer_baseAddress property not emitted or correct, check affiliateProducerBase method')
    })

    it("should NOT be able to be affiliated with a producer more than once", async () => {

        await market.affiliateProducerBase(producer_base.address, { from: owner });

        await shouldFail.reverting(market.affiliateProducerBase(producer_base.address, { from: owner }), MSG.ALREADY_AFFILIATED)
    })

    it("should NOT be able to be affiliated with a producer base on other than owners request", async () => {

        await shouldFail.reverting(market.affiliateProducerBase(producer_base.address, { from: producer }), MSG.ONLY_OWNER)

        await shouldFail.reverting(market.affiliateProducerBase(producer_base.address, { from: not_a_member }), MSG.ONLY_OWNER)
    })

    it("should be able to open new store on member's request", async () => {
        
        const expectedEventResult = {
            account : producer,
            member_base: producer_base_adr
        };

        await shared_func.affiliateMarketWithMemberBase(owner, producer_base_adr, market)

        const result = await market.openStore(producer_base_adr, default_store_name, { from: producer });
        
        expectEvent.inLogs(result.logs, 'LogNewStoreOpened', { memberAddress: expectedEventResult.account, producerBaseAddress: expectedEventResult.member_base }, 
            'LogNewStoreOpened event account property not emitted or correct, check openStore method');

        assert.equal(result.receipt.status, true, 'was not able to register new store')
    })

    it("should NOT to open store on not-a-member's request", async () => {

        await shared_func.affiliateMarketWithMemberBase(owner, producer_base_adr, market)

        await shouldFail.reverting(market.openStore(producer_base_adr, default_store_name, { from: not_a_member }), MSG.UNAUTHORISED)
    })
    it("should NOT to open store on member's request with unspecified name", async () => {

        await shared_func.affiliateMarketWithMemberBase(owner, producer_base_adr, market)

        await shouldFail.reverting(market.openStore(producer_base_adr, web3.utils.fromAscii('', 32), { from: producer }), MSG.EMPTY_STRING)
    })

    it("should NOT be able to open store when producer base is not affiliated", async () => {

        await shouldFail.reverting(market.openStore(producer_base_adr, default_store_name, { from: producer }), MSG.NOT_AFFILIATED)
    })

    it("should NOT allow one member to one more than one store", async () => {

        await shared_func.affiliateMarketWithMemberBase(owner, producer_base_adr, market)

        await market.openStore(producer_base_adr, default_store_name, { from: producer })
        await shouldFail.reverting(market.openStore(producer_base_adr, default_store_name, { from: producer }), MSG.STORE_OWNER)
    })
})