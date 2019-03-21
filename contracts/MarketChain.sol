pragma solidity >=0.4.22 <0.6.0;

import "./Administrable.sol";

contract MarketChain is Administrable {

    struct Location { //3 x 32 bytes
        bytes16 postalCode;
        int128 altitude; //meters
        int256 longitude; //seconds
        int240 latitude; //seconds
        bytes2 isoCode;
    }
    
    struct Producers {
        bytes32 shopName;
        bytes32 manager;
        bytes32 email; //?
        Location location;
    }
    
    struct Event {
        uint256 start;
        uint256 end; 
        Location location;
    }
}