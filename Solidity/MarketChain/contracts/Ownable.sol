pragma solidity >=0.4.22 <0.6.0;

import "./BaseContract.sol";

contract Ownable is BaseContract {
    address public owner;
    
    event OwnershipTransferred(address indexed previousOwner, address indexed newOwner);
    
    constructor () public {
        //require(tx.origin == msg.sender);
        owner = msg.sender;
    }

    modifier onlyOwner () {
        require(msg.sender == owner, '2');
        _;
    }
    
    /// @dev Transfers ownershipto given address
    /// @notice Only owner function
    /// @param newOwner The account address of the potential new owner
    function transferOwnership(address newOwner)
        public 
        onlyValidAddress(newOwner) 
        onlyOwner {

        owner = newOwner;

        emit OwnershipTransferred(owner, newOwner);
    }
}