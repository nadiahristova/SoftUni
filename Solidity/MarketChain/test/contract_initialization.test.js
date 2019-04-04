

const ProducerBase = artifacts.require("ProducerBase");
const RegionalMarket = artifacts.require("RegionalMarket");

const { shared_func, MSG } = require('./_utils.js')
const { shouldFail } = require('openzeppelin-test-helpers');

contract('ProducerBase', function ([owner]) {

    let member_base;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        member_base = await ProducerBase.deployed()

        await shared_func.initializeMemberBase(member_base)
    });

    it("should already be initialized", async () => {

        await shouldFail.reverting(shared_func.initializeMemberBase(member_base), MSG.INITIALIZED)
    })
})


contract('RegionalMarket', function ([owner]) {

    let market;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await RegionalMarket.deployed()

        await shared_func.initializeRegionalMarket(market)
    });

    it("should already be initialized", async () => {

        await shouldFail.reverting(shared_func.initializeRegionalMarket(market), MSG.INITIALIZED)
    })
})