pragma solidity >=0.5.6 <0.6.0;

interface IBaseMarket {
    function affiliateClientBase (address memberBaseAddress) external;

    function affiliateProducerBase (address memberBaseAddress) external;

    function getAccumolatedProfit () external view returns(uint);

    function retrieveProfit () external payable;
}