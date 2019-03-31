pragma solidity >=0.5.6 <0.6.0;


import "./Ownable.sol";

contract Initializable is Ownable {
    bool internal _isInitialized;

    modifier onlyWhenInitialized() {
        require(_isInitialized);
        _;
    }

    function initialize () onlyOwner public returns(bool) {
        require(!_isInitialized);

        _isInitialized = true;
    }
}