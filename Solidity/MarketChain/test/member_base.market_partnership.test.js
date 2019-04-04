var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("BaseMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers');
const { expect } = require('chai');

// TODO Pausable membership
contract('MarketMemberBase', function ([owner, member, not_a_member]) {

    let memberBase;
    let market;

    const votingCampaigns = {
        GRANT_MEMBERSHIP: new BN(1),
        REVOKE_MEMBERSHIP: new BN(2)
    }

    const gmCampaignActivePeriod = time.duration.days(30)
    const rmCampaignActivePeriod = time.duration.days(20)

    const onlyOwner_msg = 'Only owner'
    const unauthorised_msg = 'Unauthorised'
    const registered_msg = 'Registered'
    const not_a_partner_msg = 'Not partner'

    const registerMember = async function (supporter, candidateMember, memberBase){

        await memberBase.requestMembership({ from: candidateMember })
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: supporter })

        await memberBase.supportMember(candidateMember, 1, { from: supporter })

        await memberBase.registerMember(candidateMember, { from: supporter })
    }

    const registerMarket = async function (owner, memberBase) {

        return await memberBase.addMarketPartner(market.address, { from: owner })
    }

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await Market.deployed()

        memberBase = await ProducerBase.new()
        await memberBase.initialize([gmCampaignActivePeriod, rmCampaignActivePeriod], 2, 3, 50, [], [])
    });


    it("should be able to add a market partner on owner request", async () => {

        const result = await registerMarket(owner, memberBase)

        assert.equal(result.receipt.status, true)

        expectEvent.inLogs(result.logs, 'LogPartnerEntityAdded', { partnerAddress: market.address })
    })

    it("should be able to check for market partnership correctly", async () => {

        let result = await memberBase.isPartner.call(market.address)

        assert.equal(result, false)

        await registerMarket(owner, memberBase)
        
        result = await memberBase.isPartner.call(market.address)

        assert.equal(result, true)
    })

    it("should NOT be able to add a market partner on other-than-owner member request", async () => {

        registerMember(owner, member, memberBase);

        await shouldFail.reverting(memberBase.addMarketPartner(market.address, { from: member }), onlyOwner_msg)

        const result = await memberBase.isPartner.call(market.address)

        assert.equal(result, false)
    })
 

 
    it("should Not be able to add a market partner on not-a-member's request", async () => {

        await shouldFail.reverting(memberBase.addMarketPartner(market.address, { from: not_a_member }), onlyOwner_msg)

        const result = await memberBase.isPartner.call(market.address)

        assert.equal(result, false)
    })
 
    it("should NOT be able to add a market partner on owner request twice", async () => {

        await registerMarket(owner, memberBase)

        await shouldFail.reverting(memberBase.addMarketPartner(market.address, { from: owner }), registered_msg)
    })
  
    it("should NOT grant market membership by default", async () => {
        await registerMember(owner, member, memberBase);
        await registerMarket(owner, memberBase);

        const result = await memberBase.hasMarketMembership(member, market.address);

        assert.equal(result, false);
    })
 
    it("should be able to remove partner market on owner's request", async () => {

        await registerMarket(owner, memberBase)

        const result_removeMarket = await memberBase.removeMarketPartner(market.address); 

        const result_isPartner = await memberBase.isPartner.call(market.address); 

        assert.equal(result_removeMarket.receipt.status, true);
        expectEvent.inLogs(result_removeMarket.logs, 'LogPartnerEntityRemoved', { partnerAddress: market.address })

        assert.equal(result_isPartner, false);
    })

    it("should be able to grant market membership to a member on request", async () => {

        await registerMarket(owner, memberBase)

        const result_requestMembership = await memberBase.requestMarketMembership(market.address)
        const result_isMember = await memberBase.hasMarketMembership(owner, market.address)

        expectEvent.inLogs(result_requestMembership.logs, 'LogAffiliationWithPartner', { accAddress: owner, partnerAddress: market.address })

        assert.equal(result_requestMembership.receipt.status, true)
        assert.equal(result_isMember, true)
    })

    it("should NOT allow to grant membership to non existing market ", async () => {

        await registerMember(owner, member, memberBase);
        await registerMarket(owner, memberBase)

        await shouldFail.reverting(memberBase.requestMarketMembership(owner, { from: member }), not_a_partner_msg)
    })

    it("should be able to revoke market membership on member's request", async () => {

        await registerMarket(owner, memberBase)

        await memberBase.requestMarketMembership(market.address)

        const result_revokedMembership = await memberBase.revokeMarketMembership(market.address)

        expectEvent.inLogs(result_revokedMembership.logs, 'LogAffiliationWithPartnerRevoked', { accAddress: owner, partnerAddress: market.address })

        const result_isMember = await memberBase.hasMarketMembership(owner, market.address)

        assert.equal(result_revokedMembership.receipt.status, true)
        assert.equal(result_isMember, false)
    })
})

// TODO check _revokeAllMarketMembership