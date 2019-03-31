// const MarketController = artifacts.require("MarketController");
// const ClientBase = artifacts.require("ClientBase");
// const ProducerBase = artifacts.require("ProducerBase");
// const RepositoryLib = artifacts.require("AddressRepositoryLib");

const SafeMathLib = artifacts.require("SafeMath");
const VotesKeeperLib = artifacts.require("VotesKeeperLib");
const MemberBaseLib = artifacts.require("MemberBaseLib");
const VotingMemberBaseLib = artifacts.require("VotingMemberBaseLib");
const MarketPartnerLib = artifacts.require("MarketPartnerLib");
const InventoryLib = artifacts.require("InventoryLib");
const ECDSA = artifacts.require("ECDSA");

const ProducerBase = artifacts.require("ProducerBase");

const BaseMarket = artifacts.require("BaseMarket");

const AddressRepositoryLib = artifacts.require("AddressRepositoryLib");

module.exports = function(deployer, network) {
  // if(network == "development"){
  //   let market;

  //   deployer.deploy(RepositoryLib);
  //   deployer.link(RepositoryLib, MarketController);
  //   deployer.deploy(MarketController)
  //     .then(() => MarketController.deployed())
  //     .then((instance) => market = instance)
  //       .then(() => deployer.deploy(ProducerBase))
  //       .then(() => ProducerBase.deployed()).then((instance) => instance.upgradeMarketBase(MarketController.address))
  //       .then(() => deployer.deploy(ClientBase))
  //       .then(() => ClientBase.deployed())
  //       .then((instance) => instance.upgradeMarketBase(MarketController.address))
  //         .then(() => market.initialize(ClientBase.address, ProducerBase.address));
  // }

  if(network == "development"){
      let market;

      deployer.deploy(SafeMathLib);
      deployer.link(SafeMathLib, VotesKeeperLib);

      deployer.deploy(VotesKeeperLib);
      deployer.link(VotesKeeperLib, VotingMemberBaseLib);
      deployer.deploy(MemberBaseLib);
      deployer.link(MemberBaseLib, VotingMemberBaseLib);
      deployer.deploy(VotingMemberBaseLib);

      deployer.deploy(MarketPartnerLib);
      deployer.deploy(InventoryLib);
      deployer.deploy(ECDSA);
      deployer.link(VotingMemberBaseLib, ProducerBase);
      deployer.link(MarketPartnerLib, ProducerBase);
      deployer.link(ECDSA, ProducerBase);
      deployer.link(InventoryLib, ProducerBase);

      deployer.deploy(ProducerBase);

      deployer.deploy(AddressRepositoryLib);
      deployer.link(SafeMathLib, BaseMarket);
      deployer.link(ECDSA, BaseMarket);
      deployer.link(AddressRepositoryLib, BaseMarket);

      deployer.deploy(BaseMarket);
    }
};
