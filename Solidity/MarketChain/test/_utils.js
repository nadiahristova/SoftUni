
const {  BN } = require('openzeppelin-test-helpers');
const {  utils } = require('./_utils')
const utils = {
    registerMember : async function (supporter, candidateMember, memberBase){
        await memberBase.requestMembership({ from: candidateMember });
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: supporter });

        await memberBase.supportMember(candidateMember, 1, { from: supporter });

        await memberBase.registerMember(candidateMember, { from: supporter });
    }
  };

  const enums = {
    votingCampaigns : {
        GRANT_MEMBERSHIP: new BN(1),
        REVOKE_MEMBERSHIP: new BN(2),
        DONATION: new BN(3)
    }
  }

  const msg_constants = {
    onlyOwner_msg : 'Only owner',
    unauthorised_msg : 'Unauthorised'
  }