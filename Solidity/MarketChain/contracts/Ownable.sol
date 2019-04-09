pragma solidity >=0.4.22 <0.6.0;

import "./BaseContract.sol";

contract Ownable {
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

    modifier onlyNotOwner (address adr) {
        require(adr != owner);
        _;
    }
    
    function transferOwnership(address newOwner) public onlyOwner {
        owner = newOwner;

        emit OwnershipTransferred(owner, newOwner);
    }
}