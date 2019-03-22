pragma solidity >=0.5.6 <0.6.0;
pragma experimental ABIEncoderV2;

import "./Ownable.sol";

contract Administrable is Ownable {
    struct AdminCommitee {
        uint256 takenSlots;
        address[] admins;
    }

/** 
    struct CountryRegion {
        bytes2 isoCode;
        bytes30 province;
    }

    ///@dev Maps admin account addresses to region basic info
    mapping(address => CountryRegion[5]) _adminsAssignments;
*/
    ///@dev Maps hashed country's ISO Code and country region to set of admin account addresses
    mapping(bytes32 => AdminCommitee) private _adminsPerProvince;

    ///@dev Maps hashed value of given country ISO Code, admin's address and given's country region to the value of 
    /// admin's address index + 1 in _adminsPerCountry 
    ///@notice In case of value being 0 - this serves as a flag that address is not registered as admin in this country 
    mapping(bytes32 => uint) private _countryHasAdminMap;

    ///@dev Keeps track of how many times a given account is assigned as an admin to a province
    mapping(address => uint) private _adminAssignmentsCountMap;

    modifier onlyAdmin(){
        require(isAdmin(msg.sender));
        _;
    }


    event LogAdminAssigned(address indexed account, bytes2 indexed isoCode, bytes30 indexed province);
    event LogAdminRetired(address indexed account, bytes2 indexed isoCode, bytes30 indexed province);

    ///@dev Assignes admin to a given region of a country
    ///@param admin Account address of admin candidate 
    ///@param isoCode Two letter hex ISO Code for a given country.
    ///@param province Name of a province in hex. 
    ///@return Success when admin can be assigned to a given region.
    ///@notice Only owner can use this function. Max number of admins per region is restricted to 55. Max number of managed
    /// regions by admin is restricted to 5. ISO Code length is 2 letters upper case. Province name max length is 30 lower case text.
    function assignAdminToProvince(address admin, bytes2 countryISOCode, bytes30 province) 
        public onlyValidAddress(admin) onlyOwner onlyNotOwner(admin)
    returns (bool) {
        uint numOfAssignments = _adminAssignmentsCountMap[admin]; 
        require(numOfAssignments < 5, '1');

        bytes32 assignedAdminLocationKey = _returnLocationAdminKey(countryISOCode, admin, province);
        require(_countryHasAdminMap[assignedAdminLocationKey] == 0, '3');

        bytes32 assignedLocationKey = _returnLocationKey(countryISOCode, province);
        uint takenSlots = _adminsPerProvince[assignedLocationKey].takenSlots;
        
        require(takenSlots < 55, '2');

        /** 
        CountryRegion[5] memory managedRegions = _adminsAssignments[admin];
 
        uint index = 0;
        while(index < managedRegions.length){
            
            if(managedRegions[index].isoCode == 0x0) {
                // a slot is available
                _adminsAssignments[admin][index] = CountryRegion({ isoCode: countryISOCode, province: province });
                break;
            }

            index++;
        }

        // empty slot was not found
        require(index != managedRegions.length);
        */

        uint collectionLength = _adminsPerProvince[assignedLocationKey].admins.length;

        if(takenSlots < collectionLength) {
             _adminsPerProvince[assignedLocationKey].admins[takenSlots] = admin;
        } else {
            _adminsPerProvince[assignedLocationKey].admins.push(admin);
        }

        takenSlots++;
        numOfAssignments++;

        _adminAssignmentsCountMap[admin] = numOfAssignments;

        _adminsPerProvince[assignedLocationKey].takenSlots = takenSlots;

        _countryHasAdminMap[assignedAdminLocationKey] = takenSlots;// basically the new index + 1

        emit LogAdminAssigned(admin, countryISOCode, province);

        return true;
    }

   /** 
    ///@dev Returns administered regions
    ///@param accAddres Account address
    ///@return Collection of basic region data managed by the account 
    function returnAdministeredRegions(address accAddres) public view returns (CountryRegion[5] memory) {
        CountryRegion[5] memory regions = _adminsAssignments[accAddres];
        return regions;
    }
    */

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return True on success
    function removeAdminFromProvince(address accAddress, bytes2 countryISOCode, bytes30 province) 
        public onlyOwner  
    returns (bool) {
        require(isAdmin(accAddress));

        bytes32 assignedAdminLocationKey_Deleted = _returnLocationAdminKey(countryISOCode, accAddress, province);
        uint index = _countryHasAdminMap[assignedAdminLocationKey_Deleted];
        require(index != 0);

        bytes32 assignedLocationKey = _returnLocationKey(countryISOCode, province);
        
        uint takenSlots = _adminsPerProvince[assignedLocationKey].takenSlots;

        assert(index <= takenSlots);
        
        if (takenSlots == 1) {
            delete _adminsPerProvince[assignedLocationKey];
        }
        else {
            // takenSlots is the index of the last valid(assigned) record
            takenSlots --;
            address lastValidRecod = _adminsPerProvince[assignedLocationKey].admins[takenSlots];
            // we nullify last record
            delete _adminsPerProvince[assignedLocationKey].admins[takenSlots];
            _adminsPerProvince[assignedLocationKey].takenSlots = takenSlots;

            // overwrite the address we want to delete with the last valid value
            _adminsPerProvince[assignedLocationKey].admins[index - 1] = lastValidRecod;
            // we are saving the new index for the moved admin
            _countryHasAdminMap[_returnLocationAdminKey(countryISOCode, lastValidRecod, province)] == index;
        }

        // we are marking the old admin as deleted
        delete _countryHasAdminMap[assignedAdminLocationKey_Deleted];
        // we decreasing num of admin assignments
        _adminAssignmentsCountMap[accAddress] -= 1;

        emit LogAdminRetired(accAddress, countryISOCode, province);

        return true;
    }

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return Is given account an admin 
    function isAdmin(address accAddress) public view returns (bool) {
        return _adminAssignmentsCountMap[accAddress] != 0;
    }

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return Is given account an admin 
    function returnAdminsPerProvince(bytes2 isoCode, bytes30 province) public view returns (address[] memory) {
        return _adminsPerProvince[_returnLocationKey(isoCode, province)].admins;
    }
/** 
    function takenSlots(bytes2 isoCode, bytes30 province) public view returns (uint) {
        return _adminsPerProvince[_returnLocationKey(isoCode, province)].takenSlots;
    }
*/
    function _returnLocationAdminKey(bytes2 isoCode, address adr, bytes30 province) private pure returns (bytes32) {
        return keccak256(abi.encodePacked(isoCode, adr, province));
    }

    function _returnLocationKey(bytes2 isoCode, bytes30 province) private pure returns (bytes32) {
        return keccak256(abi.encodePacked(isoCode, province));
    }
}