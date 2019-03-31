pragma solidity >=0.4.22 <0.6.0;

contract BaseContract {
    modifier onlyValidAddress(address adr) {
        require(adr != address(0));
        _;
    }

    modifier onlyNaturalNumber(uint256 num) {
        require(num >= 0);
        _;
    }
}