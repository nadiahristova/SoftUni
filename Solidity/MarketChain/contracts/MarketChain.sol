pragma solidity >=0.4.22 <0.6.0;

import "./Administrable.sol";

contract MarketChain is Administrable {

    enum MemberType { Producer, Client, ThirdParty }

    struct Event {
        uint256 start;
        uint256 end; 
        Location location;
    }

    struct Meeting {
        uint256 timeStamp;
        Location location;
    }

    //mapping (address => )

    struct Member {
        address account;
        // infromation relevant to user's profile - username, name, email, details etc.
        bytes32 about;
        MemberType memberType;
        Location location;
        // followers - another repository
        // subscribers - another repository
    }

    struct Location { //3 x 32 bytes
        bytes16 postalCode;
        int128 altitude; //meters
        int256 longitude; //seconds
        int240 latitude; //seconds
        bytes2 isoCode;
    }
    
    mapping(address => bool) clientMap; // mapping if 0 then it is not if > 0 returns it's id
    mapping(address => bool) producerMap; // mapping if 0 then it is not if > 0 returns it's id

    /** 
        struct Producers {
            bytes32 shopName;
            bytes32 manager;
            bytes32 email; //?
            Location location;
        }
    */

    function tets() onlyOwner public {

    }
}