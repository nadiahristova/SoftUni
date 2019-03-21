pragma solidity >=0.4.22 <0.6.0;

contract BaseContract {
    modifier onlyValidAddress(address adr) {
        require(adr != address(0));
        _;
    }

    function _length(address[] storage arr) internal view returns(uint) {
        return arr.length;
    }
}