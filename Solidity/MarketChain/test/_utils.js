
const { BN, time } = require('openzeppelin-test-helpers');

module.exports = {
  shared_const: function () {
    let isoCodeInt = web3.utils.asciiToHex('BG', 2)
    let provinceInt = web3.utils.fromAscii('Veliko Tarnovo', 30)

    return {
      default_IsoCode: isoCodeInt,
      default_province: provinceInt,
      default_location: { iSOCode: isoCodeInt, province: provinceInt }
    }
  }(),
  shared_func: {
    grant_membership_ative_period: time.duration.days(30),
    revoke_membership_active_period: time.duration.days(20),
    donation_round_period: time.duration.days(90),   
    profitprofit_fee: 2,
    decisive_vote_weight_proportion: 2,
    decisive_vote_count_proportion: 3,
    initial_owner_vote_weight: 50,

    default_product: {
      specificationId: new BN(1),
      pricePerUnit: new BN(100),
      amount: new BN(15),
      hasNegotiablePrice: false
    },

    product1: {
      specificationId: new BN(1),
      pricePerUnit: new BN(150),
      amount: new BN(1000),
      hasNegotiablePrice: true
    },

    initializeProducerBase: async function (memberBase) {

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
    registerMarket: async function (owner, market, memberBase) {

      return await memberBase.addMarketPartner(market.address, { from: owner })
    },
    addProduct: async function (storeFrontId, storeOwner, memberBase, product) {
        
      return await memberBase.addProductToStoreFront(storeFrontId, product.specificationId, product.pricePerUnit, 
        product.amount, product.hasNegotiablePrice, { from: storeOwner });
    },
    registerMarketMember: async function (supporter, candidateMember, location, memberBase){
      await memberBase.requestMembership({ from: candidateMember });

      await memberBase.launchMembershipGrantingCampaign(candidateMember, { from: supporter });

      await memberBase.supportMember(candidateMember, 1, { from: supporter });

      await memberBase.registerMember(location, { from: candidateMember });
    },
    affiliateMarketWithMemberBase: async function (owner, memberBase, market) {
        
      return await market.affiliateProducerBase(memberBase, { from: owner })
    },
    openMarketStore: async function (owner, member, name, memberBase, market) {
        
      await market.affiliateProducerBase(memberBase, { from: owner })

      return await market.openStore(memberBase, name, { from: member });
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
    NOT_INITIALIZED: '0',
    INITIALIZED: 'Init',
    ONLY_OWNER: '2',
    UNAUTHORISED: '3',
    REGISTERED: 'Registered',
    NOT_A_PARTNER: 'Not partner',
    NOT_EXISTING_STORE_FRONT: '6',
    NOT_EXISTING_STORE_PRODUCT: '7',
    ENABLED_STORE_FRONT: '8',
    ALREADY_AFFILIATED: '9', 
    EMPTY_STRING: '10',
    NOT_AFFILIATED: '11',
    STORE_OWNER: '12',
    WAIT_TIME: 'Wait time',

  }
}