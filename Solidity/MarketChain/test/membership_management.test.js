var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("BaseMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers');
const { expect } = require('chai');

contract('VotingMemberBase', function ([owner, candidateMember, member1, member2, member3]) {

    let memberBase;

    const votingCampaigns = {
        GRANT_MEMBERSHIP: new BN(1),
        REVOKE_MEMBERSHIP: new BN(2)
    }

    const gmCampaignActivePeriod = time.duration.days(30);
    const rmCampaignActivePeriod = time.duration.days(20);

    const onlyOwner_msg = 'Only owner';
    const unauthorised_msg = 'Unauthorised';

    const registerMember = async function (supporter, candidateMember, memberBase){
        await memberBase.requestMembership({ from: candidateMember });
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: supporter });

        await memberBase.supportMember(candidateMember, 1, { from: supporter });

        await memberBase.registerMember(candidateMember, { from: supporter });
    }

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        memberBase = await ProducerBase.new();
        await memberBase.initialize([gmCampaignActivePeriod, rmCampaignActivePeriod], 2, 3, 50, [], []);
    });
    /** */
    
    it("should mark owner as member", async () => {
        const result = await memberBase.isMember.call(owner);

        assert.equal(result, true);
    })
    
    it("should be able to register member", async () => {
        await memberBase.requestMembership({ from: candidateMember });
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: owner });

        let result = await memberBase.supportMember(candidateMember, 1, { from: owner });

        expectEvent.inLogs(result.logs, 'LogVotingCampaignSupported', { supporter: owner, supported: candidateMember });
        expectEvent.inLogs(result.logs, 'PropositionAccepted', { accAddress: candidateMember, campaignId: votingCampaigns.GRANT_MEMBERSHIP });

        result = await memberBase.registerMember(candidateMember, { from: owner });

        expectEvent.inLogs(result.logs, 'LogMemberRegistration', { accAddress: candidateMember });

        result = await memberBase.isMember.call(candidateMember, { from: candidateMember });

        assert.equal(result, true);
    })

    it("should be able to register member on base of vote weigh", async () => {
        await registerMember(owner, member1, memberBase);
        await registerMember(owner, member2, memberBase);
        await registerMember(owner, member3, memberBase);

        const result1 = await memberBase.isMember.call(member1);
        const result2 = await memberBase.isMember.call(member2);
        const result3 = await memberBase.isMember.call(member3);

        assert.equal(result1, true);
        assert.equal(result2, true);
        assert.equal(result3, true);
    })

    it("should be able to register member on base of voters count", async () => {
        await registerMember(owner, member1, memberBase);
        await registerMember(owner, member2, memberBase);

        await memberBase.requestMembership({ from: candidateMember });
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: member1 });

        await memberBase.supportMember(candidateMember, 1, { from: member1 });
        await memberBase.supportMember(candidateMember, 1, { from: member2 });

        await memberBase.registerMember(candidateMember, { from: member2 });

        const result = await memberBase.isMember.call(candidateMember);

        assert.equal(result, true);
    })

    it("should be able to revoke membership", async () => {
        await registerMember(owner, member1, memberBase);

        await memberBase.launchCampaign(member1, 2, { from: owner });
        
        await memberBase.supportMember(member1, 2, { from: owner });

        //expectEvent.inLogs(result.logs, 'LogVotingCampaignSupported', { supporter: owner, supported: candidateMember });

        await memberBase.triggerMembershipRevocation(member1, { from: owner });

        const result = await memberBase.isMember.call(candidateMember);

        assert.equal(result, false);
    })
   


    it("should be able to revoke membership on owner's request immediately without voting", async () => {
        await registerMember(owner, member1, memberBase);

        const result_revokeMembership = await memberBase.revokeMembershipImmediately(member1, { from: owner });
        const result_isMember = await memberBase.isMember.call(candidateMember);

        assert.equal(result_revokeMembership.receipt.status, true);
        assert.equal(result_isMember, false);
    })
   
/**   */
})