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
        require(msg.sender == owner, 'Only owner');
        _;
    }

    modifier onlyNotOwner (address adr) {
        require(adr != owner);
        _;
    }
    
    function transferOwnership(address newOwner) public onlyOwner onlyValidAddress(newOwner) {
        emit OwnershipTransferred(owner, newOwner);
        
        owner = newOwner;
    }
}