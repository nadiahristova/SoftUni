var ProducerBase = artifacts.require("ProducerBase");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers');

const { shared_func, MSG, ENUMS } = require('./_utils.js')
const { VOTING_CAMPAIGN } = ENUMS;

contract('MemberBase', function ([owner, candidateMember, member, notMember]) {

    let member_base;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        member_base = await ProducerBase.new();

        await shared_func.initializeProducerBase(member_base)
    });
  

    it("should mark owner as member", async () => {
        const result = await member_base.isMember.call(owner);

        assert.equal(result, true);
    })

    it("should be able to request for membership if not member", async () => {

        const { logs } = await member_base.requestMembership({ from: candidateMember });
        
        expectEvent.inLogs(logs, 'LogMembershipRequested', { accAddress: candidateMember });
        expectEvent.inLogs(logs, 'LogMembershipRequestPending', { accAddress: candidateMember });
    })

    it("should NOT be able to request for membership if member", async () => {

        await shouldFail.reverting(member_base.requestMembership({ from: owner })); 
    })

    it("should NOT be able to request for membership twice", async () => {
        await member_base.requestMembership({ from: candidateMember });

        await shouldFail.reverting(member_base.requestMembership({ from: candidateMember })); 
    })

    it("should be able to launch membership granting campaign", async () => {
        await member_base.requestMembership({ from: candidateMember });
        
        const { logs } = await member_base.launchMembershipGrantingCampaign(candidateMember, { from: owner });
        
        expectEvent.inLogs(logs, 'LogOpenedNewVotingCampaign', { accAddress: candidateMember, campaignId: VOTING_CAMPAIGN.GRANT_MEMBERSHIP });
    })

    it("should NOT be able to launch membership granting campaign when not member", async () => {
        await member_base.requestMembership({ from: candidateMember });
        
        await shouldFail.reverting(member_base.launchMembershipGrantingCampaign(candidateMember, { from: notMember }), MSG.UNAUTHORISED);
    })

    it("should NOT be able to launch membership granting campaign without request", async () => {
        
        await shouldFail.reverting(member_base.launchMembershipGrantingCampaign(notMember, { from: owner }), 'Not requested');
    })
    
    it("should keep track of active campaigns per member or candidate member", async () => {
        
        let result = await member_base.hasActiveCampaigns(candidateMember);
        
        assert.equal(result, false);
        
        await member_base.requestMembership({ from: candidateMember });
        await member_base.launchMembershipGrantingCampaign(candidateMember, { from: owner });
        
        result = await member_base.hasActiveCampaigns(candidateMember);
        
        assert.equal(result, true);
    })

    it("should NOT be able to launch membership revoking campaign on not-a-member request", async () => {

        await shouldFail.reverting(member_base.launchMembershipGrantingCampaign(notMember, { from: owner }), MSG.UNAUTHORISED);
    })
    
    it("should NOT be able to launch membership revoking campaign if not owner", async () => {
        await shared_func.registerMember(owner, member, member_base)

        await shouldFail.reverting(member_base.launchMembershipRevocationCampaign(owner, { from: member }), MSG.ONLY_OWNER);
    })
})