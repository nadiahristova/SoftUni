var ClientBase = artifacts.require("./ClientBase.sol");
var ProducerBase = artifacts.require("./ProducerBase.sol");
var MarketController = artifacts.require("./MarketController.sol");

const catchRevert = require('../utils/exceptions').catchRevert;

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