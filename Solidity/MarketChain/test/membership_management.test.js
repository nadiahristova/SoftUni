var ProducerBase = artifacts.require("ProducerBase");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers')
const { shared_func, MSG, ENUMS } = require('./_utils.js')
const { VOTING_CAMPAIGN } = ENUMS
//const { expect } = require('chai');

contract('VotingMemberBase', function ([owner, candidateMember, member1, member2, member3]) {

    let member_base;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        member_base = await ProducerBase.new()

        await shared_func.initializeProducerBase(member_base)
    });
    
    it("should mark owner as member", async () => {
        const result = await member_base.isMember.call(owner);

        assert.equal(result, true);
    })
    
    it("should be able to register member", async () => {
        await member_base.requestMembership({ from: candidateMember });
        await member_base.launchMembershipGrantingCampaign(candidateMember, { from: owner });

        let result = await member_base.supportMember(candidateMember, 1, { from: owner });

        expectEvent.inLogs(result.logs, 'LogVotingCampaignSupported', { supporter: owner, supported: candidateMember });
        expectEvent.inLogs(result.logs, 'PropositionAccepted', { accAddress: candidateMember, campaignId: VOTING_CAMPAIGN.GRANT_MEMBERSHIP });

        result = await member_base.registerMember(candidateMember, { from: owner });

        expectEvent.inLogs(result.logs, 'LogMemberRegistration', { accAddress: candidateMember });

        result = await member_base.isMember.call(candidateMember, { from: candidateMember });

        assert.equal(result, true);
    })

    it("should be able to register member on base of vote weigh", async () => {
        await shared_func.registerMember(owner, member1, member_base);
        await shared_func.registerMember(owner, member2, member_base);
        await shared_func.registerMember(owner, member3, member_base);

        const result1 = await member_base.isMember.call(member1);
        const result2 = await member_base.isMember.call(member2);
        const result3 = await member_base.isMember.call(member3);

        assert.equal(result1, true);
        assert.equal(result2, true);
        assert.equal(result3, true);
    })

    it("should be able to register member on base of voters count", async () => {
        await shared_func.registerMember(owner, member1, member_base);
        await shared_func.registerMember(owner, member2, member_base);

        await member_base.requestMembership({ from: candidateMember });
        await member_base.launchMembershipGrantingCampaign(candidateMember, { from: member1 });

        await member_base.supportMember(candidateMember, 1, { from: member1 });
        await member_base.supportMember(candidateMember, 1, { from: member2 });

        await member_base.registerMember(candidateMember, { from: member2 });

        const result = await member_base.isMember.call(candidateMember);

        assert.equal(result, true);
    })

    it("should be able to revoke membership", async () => {
        await shared_func.registerMember(owner, member1, member_base);

        await member_base.launchMembershipRevocationCampaign(member1, { from: owner });
        
        await member_base.supportMember(member1, 2, { from: owner });

        //expectEvent.inLogs(result.logs, 'LogVotingCampaignSupported', { supporter: owner, supported: candidateMember });

        await member_base.triggerMembershipRevocation(member1, { from: owner });

        const result = await member_base.isMember.call(candidateMember);

        assert.equal(result, false);
    })

    it("should be able to revoke membership on owner's request immediately without voting", async () => {
        await shared_func.registerMember(owner, member1, member_base);

        const result_revokeMembership = await member_base.revokeMembershipImmediately(member1, { from: owner });
        const result_isMember = await member_base.isMember.call(candidateMember);

        assert.equal(result_revokeMembership.receipt.status, true);
        assert.equal(result_isMember, false);
    })
})