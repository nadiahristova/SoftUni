
const { BN, time } = require('openzeppelin-test-helpers');

module.exports = {
  shared_func: {
    grant_membership_ative_period: time.duration.days(30),
    revoke_membership_active_period: time.duration.days(20),
    donation_round_period: time.duration.days(90),   
    profitprofit_fee: 2,
    decisive_vote_weight_proportion: 2,
    decisive_vote_count_proportion: 3,
    initial_owner_vote_weight: 50,

    initializeMemberBase: async function (memberBase) {

      await memberBase.initialize([this.grant_membership_ative_period, this.revoke_membership_active_period], this.decisive_vote_weight_proportion, this.decisive_vote_count_proportion, this.initial_owner_vote_weight);
    },
    initializeRegionalMarket: async function (market) {

       await market.initialize([this.grant_membership_ative_period, this.revoke_membership_active_period], this.profitprofit_fee, this.decisive_vote_weight_proportion, this.decisive_vote_count_proportion, this.donation_round_period, this.initial_owner_vote_weight);
    },
    registerMember: async function (supporter, candidateMember, memberBase){

        await memberBase.requestMembership({ from: candidateMember });
        await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: supporter });

        await memberBase.supportMember(candidateMember, 1, { from: supporter });

        await memberBase.registerMember(candidateMember, { from: supporter });
    },
    registerMarket: async function (owner, memberBase) {

      return await memberBase.addMarketPartner(market.address, { from: owner })
    },
    addProduct: async function (storeFrontId, storeOwner, memberBase, product) {
        
      return await memberBase.addProductToStoreFront(storeFrontId, product.specificationId, product.pricePerUnit, 
        product.amount, product.hasNegotiablePrice, { from: storeOwner });
    },
    updateProduct: async function (storeFrontId, productId, storeOwner, memberBase, product) {
        
      return await memberBase.updateProduct(storeFrontId, productId, product.pricePerUnit, 
          product.amount, product.hasNegotiablePrice, { from: storeOwner });
    }
  },
  ENUMS: {
    VOTING_CAMPAIGN : {
        GRANT_MEMBERSHIP: new BN(1),
        REVOKE_MEMBERSHIP: new BN(2),
        DONATION: new BN(3)
    }
  },
  MSG: {
    NOT_INITIALIZED: 'Not init',
    INITIALIZED: 'Init',
    ONLY_OWNER: 'Only owner',
    UNAUTHORISED: 'Unauthorised',
    REGISTERED: 'Registered',
    NOT_A_PARTNER: 'Not partner',
    NOT_EXISTING_STORE_FRONT: 'Not existing',
    ENABLED_STORE_FRONT: 'Enabled',
    WAIT_TIME: 'Wait time'
  }
}