var Ownable = artifacts.require("Ownable");

const { shouldFail } = require('openzeppelin-test-helpers');

contract('Ownable', function ([owner, nonOwner]) {

    //shared parameters
    let ownable; 

    const emptyAddress = '0x0000000000000000000000000000000000000000'

    // Initial Setup
    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        ownable = await Ownable.new();
    });


    // Unit tests
    it("should mark deployer address as owner", async () => {
        const deployer = await ownable.owner.call();

        assert.equal(deployer, owner, 'the deployer is not marked as the owner');
    })

    it("should transfer ownership", async () => {
        const result = await ownable.transferOwnership(nonOwner, { from: owner });

        assert.equal(result.receipt.status, true, 'ownership cannot be transferred');
    })

    it("should not allow to transfer ownership", async () => {
        await shouldFail.reverting(ownable.transferOwnership(nonOwner, { from: nonOwner }), "only owner can transfer ownership");
    })

    it("should not allow to transfer ownership", async () => {
        await shouldFail.reverting(ownable.transferOwnership(emptyAddress, { from: owner }), "should specify existing address");
    })
})