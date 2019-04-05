var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("RegionalMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers')
const { shared_func, MSG, ENUMS } = require('./_utils.js')
const { VOTING_CAMPAIGN } = ENUMS

// TODO Pausable membership
contract('MarketMemberBase', function ([owner, member, not_a_member]) {

    let member_base;
    let market;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await Market.deployed()

        member_base = await ProducerBase.new()
        await shared_func.initializeProducerBase(member_base)
    });


    it("should be able to add a market partner on owner request", async () => {

        const result = await shared_func.registerMarket(owner, market, member_base)

        assert.equal(result.receipt.status, true)

        expectEvent.inLogs(result.logs, 'LogPartnerEntityAdded', { partnerAddress: market.address })
    })

    it("should be able to check for market partnership correctly", async () => {

        let result = await member_base.isPartner.call(market.address)

        assert.equal(result, false)

        await shared_func.registerMarket(owner, market, member_base)
        
        result = await member_base.isPartner.call(market.address)

        assert.equal(result, true)
    })

    it("should NOT be able to add a market partner on other-than-owner member request", async () => {

        await shared_func.registerMember(owner, member, member_base);

        await shouldFail.reverting(member_base.addMarketPartner(market.address, { from: member }), VOTING_CAMPAIGN.ONLY_OWNER)

        const result = await member_base.isPartner.call(market.address)

        assert.equal(result, false)
    })
 
    it("should Not be able to add a market partner on not-a-member's request", async () => {

        await shouldFail.reverting(member_base.addMarketPartner(market.address, { from: not_a_member }), VOTING_CAMPAIGN.ONLY_OWNER)

        const result = await member_base.isPartner.call(market.address)

        assert.equal(result, false)
    })
 
    it("should NOT be able to add a market partner on owner request twice", async () => {

        await shared_func.registerMarket(owner, market, member_base)

        await shouldFail.reverting(member_base.addMarketPartner(market.address, { from: owner }), VOTING_CAMPAIGN.REGISTERED)
    })
  
    it("should NOT grant market membership by default", async () => {
        await shared_func.registerMember(owner, member, member_base);
        await shared_func.registerMarket(owner, market, member_base);

        const result = await member_base.hasMarketMembership(member, market.address);

        assert.equal(result, false);
    })
 
    it("should be able to remove partner market on owner's request", async () => {

        await shared_func.registerMarket(owner, market, member_base)

        const result_removeMarket = await member_base.removeMarketPartner(market.address); 

        const result_isPartner = await member_base.isPartner.call(market.address); 

        assert.equal(result_removeMarket.receipt.status, true);
        expectEvent.inLogs(result_removeMarket.logs, 'LogPartnerEntityRemoved', { partnerAddress: market.address })

        assert.equal(result_isPartner, false);
    })

    it("should be able to grant market membership to a member on request", async () => {

        await shared_func.registerMarket(owner, market, member_base)

        const result_requestMembership = await member_base.requestMarketMembership(market.address)
        const result_isMember = await member_base.hasMarketMembership(owner, market.address)

        expectEvent.inLogs(result_requestMembership.logs, 'LogAffiliationWithPartner', { accAddress: owner, partnerAddress: market.address })

        assert.equal(result_requestMembership.receipt.status, true)
        assert.equal(result_isMember, true)
    })

    it("should NOT allow to grant membership to non existing market ", async () => {

        await shared_func.registerMember(owner, member, member_base);
        await shared_func.registerMarket(owner, market, member_base)

        await shouldFail.reverting(member_base.requestMarketMembership(owner, { from: member }), VOTING_CAMPAIGN.NOT_A_PARTNER)
    })

    it("should be able to revoke market membership on member's request", async () => {

        await shared_func.registerMarket(owner, market, member_base)

        await member_base.requestMarketMembership(market.address)

        const result_revokedMembership = await member_base.revokeMarketMembership(market.address)

        expectEvent.inLogs(result_revokedMembership.logs, 'LogAffiliationWithPartnerRevoked', { accAddress: owner, partnerAddress: market.address })

        const result_isMember = await member_base.hasMarketMembership(owner, market.address)

        assert.equal(result_revokedMembership.receipt.status, true)
        assert.equal(result_isMember, false)
    })
})

// TODO check _revokeAllMarketMembership