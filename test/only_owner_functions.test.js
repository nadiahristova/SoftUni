var MarketChain = artifacts.require("./MarketChain.sol");

const catchRevert = require('../utils/exceptions').catchRevert;

contract('MarketChain', function ([owner, nonOwner]) {

    //shared parameters
    let market; 

    const isoCode = web3.utils.asciiToHex('BG', 2);
    const province = web3.utils.fromAscii('Veliko Tarnovo', 30);

    // Initial Setup
    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await MarketChain.new();
    });


    // Unit tests
    it("should set max number of admins per country", async () => {

        const result = await market.assignAdminToProvince(nonOwner, isoCode, province, { from: owner });

        assert.equal(result.receipt.status, true, 'owner did not had the permissions');
    })

    it("should NOT allow to set max number of admins per country", async () => {

        await catchRevert(market.assignAdminToProvince(nonOwner, isoCode, province, { from: nonOwner }), "only owner should be able to use this function");
    })

    it("should allow the owner to remove admin from province", async () => {

        await market.assignAdminToProvince(nonOwner, isoCode, province, { from: owner });
        const result = await market.removeAdminFromProvince(nonOwner, isoCode, province, { from: owner });

        assert.equal(result.receipt.status, true, 'owner did not had the permissions');
    })

    it("should NOT allow non owner to remove admin from province", async () => {

        await market.assignAdminToProvince(nonOwner, isoCode, province, { from: owner });
        await catchRevert(market.removeAdminFromProvince(nonOwner, isoCode, province, { from: nonOwner }), "only owner should be able to use this function");
    })
})