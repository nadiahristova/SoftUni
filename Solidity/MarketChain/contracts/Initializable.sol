pragma solidity >=0.5.6 <0.6.0;

import "./Ownable.sol";

contract Initializable {

    bool internal _isInitialized;

    event Initialized();

    modifier onlyWhenInitialized() {
        require(_isInitialized, '0');
        _;
    }

    function _initialize () internal {
        _isInitialized = true;

        emit Initialized();
    }
}