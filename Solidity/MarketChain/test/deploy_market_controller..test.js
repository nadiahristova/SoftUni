var ClientBase = artifacts.require("./ClientBase.sol");
var ProducerBase = artifacts.require("./ProducerBase.sol");
var MarketController = artifacts.require("./MarketController.sol");

const catchRevert = require('../utils/exceptions').catchRevert;

contract('MarketController', function ([owner]) {

    let marketController;
    let clientBase;
    let producerBase;

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        marketController = await MarketController.deployed();
        clientBase = await ClientBase.deployed();
        producerBase = await ProducerBase.deployed();
    });

    it("should already be initialized", async () => {

        await catchRevert(marketController.initialize.call(clientBase.address, producerBase.address));
    })
})