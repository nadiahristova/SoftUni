const SafeMathLib = artifacts.require("SafeMath");
const VotesKeeperLib = artifacts.require("VotesKeeperLib");
const MemberBaseLib = artifacts.require("MemberBaseLib");
const VotingMemberBaseLib = artifacts.require("VotingMemberBaseLib");
const MarketPartnerLib = artifacts.require("PartnerRelationsKeeperLib");
const InventoryLib = artifacts.require("InventoryLib");
const ECDSA = artifacts.require("ECDSA");

const ProducerBase = artifacts.require("ProducerBase");

const AddressRepositoryLib = artifacts.require("AddressRepositoryLib");

const RegionalMarket = artifacts.require("RegionalMarket");


module.exports = function(deployer, network) {

  function deployLibraries() {
      deployer.deploy(SafeMathLib);
      deployer.link(SafeMathLib, VotesKeeperLib);
      deployer.link(SafeMathLib, InventoryLib);

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

      deployer.deploy(AddressRepositoryLib);

      deployer.link(AddressRepositoryLib, RegionalMarket);
      deployer.link(VotingMemberBaseLib, RegionalMarket);
      deployer.link(SafeMathLib, RegionalMarket);
      deployer.link(ECDSA, RegionalMarket);
  }

  if(network == "development" || network == "develop"){
      deployLibraries();

      deployer.deploy(ProducerBase)
        .then(() => ProducerBase.deployed())
            .then((instance) => instance.initialize([2592000, 1728000], 2, 3, 50));

      deployer.deploy(RegionalMarket)
        .then(() => RegionalMarket.deployed())
          .then((instance) => instance.initialize([2592000, 1728000], 2, 2, 3, 23328000, 50));
    } else if (network == 'ropsten') {
        deployLibraries();

        deployer.deploy(ProducerBase)
          .then(() => ProducerBase.deployed())
          .then((instance) => instance.initialize([2592000, 1728000], 2, 3, 50));

        deployer.deploy(RegionalMarket);
      }
};
