const MarketController = artifacts.require("MarketController");
const ClientBase = artifacts.require("ClientBase");
const ProducerBase = artifacts.require("ProducerBase");
const RepositoryLib = artifacts.require("AddressRepositoryLib");

module.exports = function(deployer, network) {
  if(network == "development"){
    let market;

    deployer.deploy(RepositoryLib);
    deployer.link(RepositoryLib, MarketController);
    deployer.deploy(MarketController)
      .then(() => MarketController.deployed())
      .then((instance) => market = instance)
        .then(() => deployer.deploy(ProducerBase))
        .then(() => ProducerBase.deployed()).then((instance) => instance.upgradeMarketBase(MarketController.address))
        .then(() => deployer.deploy(ClientBase))
        .then(() => ClientBase.deployed())
        .then((instance) => instance.upgradeMarketBase(MarketController.address))
          .then(() => market.initialize(ClientBase.address, ProducerBase.address));
  }
};
