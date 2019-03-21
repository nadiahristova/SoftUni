const MarketChain = artifacts.require("MarketChain");

module.exports = function(deployer) {
  deployer.deploy(MarketChain);
};
