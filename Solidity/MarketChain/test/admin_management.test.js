var Administrable = artifacts.require("./MarketChain.sol");

const catchRevert = require('../utils/exceptions').catchRevert;

contract('Administrable', function ([owner, admin_candidate, admin, producer, client]) {

    //shared parameters
    let administrable; 

    const default_IsoCode = web3.utils.asciiToHex('BG', 2);
    const default_province = web3.utils.fromAscii('Veliko Tarnovo', 30);

    const emptyAddress = '0x0000000000000000000000000000000000000000'

    // compares two hex values by removing trailing zeroes
    function are_trimmed_HEX_equal(hex1, hex2) {
        return web3.utils.hexToUtf8(hex1) == web3.utils.hexToUtf8(hex2)
    }

    // Initial Setup
    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        administrable = await Administrable.new();
    });

    it("should assign admin to province", async () => {
        const expectedEventResult = {
            account : admin_candidate,
            isoCode : default_IsoCode,
            province : default_province
        };

        const result = await administrable.assignAdminToProvince(admin_candidate, default_IsoCode, default_province, { from: owner });

        const logAdminAssignment = result.logs[0].args;
        const loggedAccount = logAdminAssignment.account;
        const loggedIsoCode = logAdminAssignment.isoCode; 
        const loggedProvince = logAdminAssignment.province;

        assert.equal(result.receipt.status, true, 'was not able');
        assert.equal(loggedAccount, expectedEventResult.account, 'LogAdminAssigned event account property not emitted or correct, check assignAdminToProvince method');
        assert.equal(are_trimmed_HEX_equal(loggedIsoCode, expectedEventResult.isoCode), true, 'LogAdminAssigned event isoCode property not emitted or correct, check assignAdminToProvince method');
        assert.equal(are_trimmed_HEX_equal(loggedProvince, expectedEventResult.province), true, 'LogAdminAssigned event province property not emitted or correct, check assignAdminToProvince method');
    })

    it("should NOT allow admin to be assigned twice to a region", async () => {
        await administrable.assignAdminToProvince(admin_candidate, default_IsoCode, default_province, { from: owner });

        await catchRevert(administrable.assignAdminToProvince(admin_candidate, default_IsoCode, default_province, { from: owner }), 'an admin cannot be assigned to a region more than once');
    })

    it("should NOT allow owner to be assigned as an admin", async () => {
        await catchRevert(administrable.assignAdminToProvince(owner, default_IsoCode, default_province, { from: owner }));
    })

    it("should confirm account being admin", async () => {
        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })
        const result = await administrable.isAdmin.call(admin, { from: client })

        assert.equal(result, true, 'was not able');
    })

    it("should NOT confirm account being admin", async () => {
        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })
        const result = await administrable.isAdmin.call(client, { from: client })
        
        assert.equal(result, false, 'was not able');
    })
  
    it("should NOT allow assignment of more than 5 regions per account", async () => {

        let randomIsoCode
        let randomProvince

        for (let i = 0; i < 5; i++){
            randomIsoCode = web3.utils.randomHex(2);
            randomProvince = web3.utils.randomHex(30);

            await administrable.assignAdminToProvince(admin, randomIsoCode, randomProvince, { from: owner })
        }

        randomIsoCode = web3.utils.randomHex(2);
        randomProvince = web3.utils.randomHex(30);

        await catchRevert(administrable.assignAdminToProvince(admin, randomIsoCode, randomProvince, { from: owner }), 'max number of regions managed by single admin should be 5');
    })

    it("should NOT allow more than 55 admins per region", async () => {
        let randomAccAddress;

        for (let i = 0; i < 55; i++){
            randomAccAddress = web3.utils.randomHex(20);
            await administrable.assignAdminToProvince(web3.utils.randomHex(20), default_IsoCode, default_province, { from: owner })
        }

        randomAccAddress = web3.utils.randomHex(20);

        await catchRevert(administrable.assignAdminToProvince(randomAccAddress, default_IsoCode, default_province, { from: owner }), 'max number of admins should be 55');
    })

    it("should NOT allow the removal of non admin", async () => {
        await catchRevert(administrable.removeAdminFromProvince(admin_candidate, default_IsoCode, default_province, { from: owner }));
    })

    it("should NOT allow admin not assigned to a province to be removed form there", async () => {
        const randomIsoCode = web3.utils.randomHex(2);
        const randomProvince = web3.utils.randomHex(30);

        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })

        await catchRevert(administrable.removeAdminFromProvince(admin, randomIsoCode, default_province, { from: owner }));
        await catchRevert(administrable.removeAdminFromProvince(admin, default_IsoCode, randomProvince, { from: owner }));
    })

    it("should be able to remove admin", async () => {
        const expectedEventResult = {
            account : admin,
            isoCode : default_IsoCode,
            province : default_province
        };

        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })

        let result = await administrable.removeAdminFromProvince(admin, default_IsoCode, default_province, { from: owner })

        const logAdminRemoval = result.logs[0].args;
        const loggedAccount = logAdminRemoval.account;
        const loggedIsoCode = logAdminRemoval.isoCode; 
        const loggedProvince = logAdminRemoval.province;
        
        assert.equal(loggedAccount, expectedEventResult.account, 'LogAdminRetired event account property not emitted or correct, check removeAdminFromProvince method');
        assert.equal(are_trimmed_HEX_equal(loggedIsoCode, expectedEventResult.isoCode), true, 'LogAdminRetired event isoCode property not emitted or correct, check removeAdminFromProvince method');
        assert.equal(are_trimmed_HEX_equal(loggedProvince, expectedEventResult.province), true, 'LogAdminRetired event province property not emitted or correct, check removeAdminFromProvince method');

        result = await administrable.isAdmin(admin);

        assert.equal(result, false);
    })

    it("should be able to delete admin", async () => {

        const randomIsoCode = web3.utils.randomHex(2);
        const randomProvince = web3.utils.randomHex(30);

        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(admin, randomIsoCode, randomProvince, { from: owner })

        await administrable.removeAdminFromProvince(admin, default_IsoCode, default_province, { from: owner })

        const result = await administrable.isAdmin(admin);

        assert.equal(result, true, 'account should be admin');
    })

    it("should return assigned to province admins", async () => {
        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(producer, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(client, default_IsoCode, default_province, { from: owner })

        const result = await administrable.returnAdminsPerProvince.call(default_IsoCode, default_province);

        assert.equal(result.length, 3, 'number of records does not match');
        assert.equal(admin, result[0], 'first account record does not match');
        assert.equal(producer, result[1], 'second account record does not match');
        assert.equal(client, result[2], 'third account record does not match');
    })

    it("should return assigned to province admins with delete", async () => {
        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(producer, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(client, default_IsoCode, default_province, { from: owner })

        await administrable.removeAdminFromProvince(producer, default_IsoCode, default_province, { from: owner })

        const result = await administrable.returnAdminsPerProvince.call(default_IsoCode, default_province);

        assert.equal(result.length, 2, 'number of records does not match');
        assert.equal(admin, result[0], 'first account record does not match');
        assert.equal(client, result[1], 'second account record does not match');
    })
 
    it("should be able to keep correct track of admin records", async () => {
        await administrable.assignAdminToProvince(admin, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(admin_candidate, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(producer, default_IsoCode, default_province, { from: owner })
        await administrable.assignAdminToProvince(client, default_IsoCode, default_province, { from: owner })

        await administrable.removeAdminFromProvince(producer, default_IsoCode, default_province, { from: owner })

        await administrable.assignAdminToProvince(producer, default_IsoCode, default_province, { from: owner })

        await administrable.removeAdminFromProvince(admin_candidate, default_IsoCode, default_province, { from: owner })

        await administrable.assignAdminToProvince(admin_candidate, default_IsoCode, default_province, { from: owner })

        await administrable.removeAdminFromProvince(admin, default_IsoCode, default_province, { from: owner })

        const result = await administrable.returnAdminsPerProvince.call(default_IsoCode, default_province);

        assert.equal(result.length, 3, 'number of records does not match');
        assert.equal(admin_candidate, result[0], 'first account record does not match');
        assert.equal(producer, result[1], 'second account record does not match');
        assert.equal(client, result[2], 'third account record should be empty');
    })
})