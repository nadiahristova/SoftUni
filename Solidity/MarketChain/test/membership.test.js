var ProducerBase = artifacts.require("ProducerBase");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers');

contract('MemberBase', function ([owner, candidateMember, member, notMember]) {

    let memberBase;

    const votingCampaigns = {
        GRANT_MEMBERSHIP: new BN(1),
        REVOKE_MEMBERSHIP: new BN(2)
    }

    const gmCampaignActivePeriod = time.duration.days(30);
    const rmCampaignActivePeriod = time.duration.days(20);

    const onlyOwner_msg = 'Only owner';
    const unauthorised_msg = 'Unauthorised';

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        memberBase = await ProducerBase.new();
        await memberBase.initialize([gmCampaignActivePeriod, rmCampaignActivePeriod], 2, 3, 50, [], []);
    });
  

    it("should mark owner as member", async () => {
        const result = await memberBase.isMember.call(owner);

        assert.equal(result, true);
    })

    it("should be able to request for membership if not member", async () => {

        const { logs } = await memberBase.requestMembership({ from: candidateMember });
        
        expectEvent.inLogs(logs, 'LogMembershipRequested', { accAddress: candidateMember });
        expectEvent.inLogs(logs, 'LogMembershipRequestPending', { accAddress: candidateMember });
    })

    it("should NOT be able to request for membership if member", async () => {

        await shouldFail.reverting(memberBase.requestMembership({ from: owner })); 
    })

    it("should NOT be able to request for membership twice", async () => {
        await memberBase.requestMembership({ from: candidateMember });

        await shouldFail.reverting(memberBase.requestMembership({ from: candidateMember })); 
    })

    it("should be able to launch membership granting campaign", async () => {
        await memberBase.requestMembership({ from: candidateMember });
        
        const { logs } = await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: owner });
        
        expectEvent.inLogs(logs, 'LogOpenedNewVotingCampaign', { accAddress: candidateMember, campaignId: votingCampaigns.GRANT_MEMBERSHIP });
    })

    it("should NOT be able to launch membership granting campaign when not member", async () => {
        await memberBase.requestMembership({ from: candidateMember });
        
        await shouldFail.reverting(memberBase.launchMembershipGrantingCampaign(candidateMember, { from: notMember }), unauthorised_msg);
    })

    it("should NOT be able to launch membership granting campaign without request", async () => {
        
        await shouldFail.reverting(memberBase.launchMembershipGrantingCampaign(notMember, { from: owner }), 'Not requested');
    })
    
    it("should keep track of active campaigns per member or candidate member", async () => {
        
        let result = await memberBase.hasActiveCampaigns(candidateMember);
        
        assert.equal(result, false);
        
        await memberBase.requestMembership({ from: candidateMember });
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: owner });
        
        result = await memberBase.hasActiveCampaigns(candidateMember);
        
        assert.equal(result, true);
    })
    
    it("should not be able to launch not a basic campaign if not owner", async () => {

        await shouldFail.reverting(memberBase.launchCampaign(owner, 3, { from: notMember }), onlyOwner_msg);
    })

    it("should not be able to launch not a basic campaign if proposed account is not member", async () => {

        await shouldFail.reverting(memberBase.launchCampaign(notMember, 3, { from: owner }), unauthorised_msg);
    })

    it("should not be able to launch campaign if campaign is not basic", async () => {

        await shouldFail.reverting(memberBase.launchCampaign(owner, 1, { from: owner }));
        await shouldFail.reverting(memberBase.launchCampaign(owner, 2, { from: owner }));
    })

    it("should not be able to launch campaign if campaign does not exists", async () => {

        await shouldFail.reverting(memberBase.launchCampaign(owner, 3, { from: owner }));
    })
})