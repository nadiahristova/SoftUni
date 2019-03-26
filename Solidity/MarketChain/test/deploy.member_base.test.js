var ClientBase = artifacts.require("./ClientBase.sol");
var ProducerBase = artifacts.require("./ProducerBase.sol");
var MarketController = artifacts.require("./MarketController.sol");

contract('MemberBase', function ([owner]) {

    let marketController;
    let clientBase;
    let producerBase;

    // Initial Setup
    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        marketController = await MarketController.deployed();
        clientBase = await ClientBase.deployed();
        producerBase = await ProducerBase.deployed();
    });

    it("should have market controller correctly assigned", async () => {

        const marketController_Client = await clientBase.getMarketControllerAddress.call(); 
        const marketController_Producer = await producerBase.getMarketControllerAddress.call();

        assert.equal(marketController_Client, marketController.address, "client base's controller not assigned correctly");
        assert.equal(marketController_Producer, marketController.address), "producer base's controller not assigned correctly";
    })
})
