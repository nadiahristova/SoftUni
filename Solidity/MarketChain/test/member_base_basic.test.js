var ClientBase = artifacts.require("ClientBase");
var ProducerBase = artifacts.require("ProducerBase");
var MarketController = artifacts.require("MarketController");

contract('ClientBase', function ([owner, bob, elly, eve]) {

    let market;
    let clientBase;
    let producerBase;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await MarketController.deployed();
        clientBase = await ClientBase.deployed();
        producerBase = await ProducerBase.deployed();
    });

    it("should be initialized", async () => {
    })
})