const ClientBase = artifacts.require("ClientBase");
const ProducerBase = artifacts.require("ProducerBase");
const MarketController = artifacts.require("MarketController");

const { shouldFail } = require('openzeppelin-test-helpers');
const { expect } = require('chai');

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

        await shouldFail.reverting(marketController.initialize.call(clientBase.address, producerBase.address));
    })
})