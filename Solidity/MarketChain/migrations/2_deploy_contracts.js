const MarketChain = artifacts.require("MarketChain");
const RepositoryLib = artifacts.require("AddressRepositoryLib");

module.exports = function(deployer) {
  deployer.deploy(RepositoryLib);
  deployer.link(RepositoryLib, MarketChain);
  deployer.deploy(MarketChain);
};
