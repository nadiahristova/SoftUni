pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./Ownable.sol";
import "./BaseContract.sol";
import "./Initializable.sol";

import "../libraries/AddressRepositoryLib.sol";


contract AdministrableByRegion is BaseContract, Ownable, Initializable {
    using AddressRepositoryLib for AddressRepositoryLib.Repository;

    AddressRepositoryLib.Repository internal _adminRepository;

    modifier onlyAdmin(){
        require(isAdmin(msg.sender));
        _;
    }

    modifier onlyValidLocation(Location memory location){
        require(location.iSOCode != 0x0 && location.province != 0x0, 'Inv param');
        _;
    }

    struct Location {
        bytes2 iSOCode; 
        bytes30 province;
    }

    event LogAdminAssigned(address indexed account, bytes2 indexed isoCode, bytes30 indexed province);
    event LogAdminRetired(address indexed account, bytes2 indexed isoCode, bytes30 indexed province);

    ///@dev Assignes admin to a given region of a country
    ///@param accAddress Account address of admin candidate 
    ///@param isoCode Two letter hex ISO Code for a given country.
    ///@param province Name of a province in hex. 
    ///@return Success when admin can be assigned to a given region.
    ///@notice Only owner can use this function. Max number of admins per region is restricted to 55. Max number of managed
    /// regions by admin is restricted to 5. ISO Code length is 2 letters upper case. Province name max length is 30 lower case text.
    function assignAdminToProvince(address accAddress, Location memory location) 
        public 
        onlyValidAddress(accAddress) 
        onlyValidLocation(location)
        onlyWhenInitialized
        onlyOwner 
    returns (bool) {
        require(accAddress != owner);

        _adminRepository.add(accAddress, _returnLocationKey(location));

        emit LogAdminAssigned(accAddress, location.iSOCode, location.province);

        return true;
    }

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return True on success
    function removeAdminFromProvince(address accAddress, Location memory location) 
        public 
        onlyValidAddress(accAddress) 
        onlyValidLocation(location)
        onlyWhenInitialized 
        onlyOwner 
    returns (bool) {
        require(isAdmin(accAddress));

        _adminRepository.remove(accAddress, _returnLocationKey(location));

        emit LogAdminRetired(accAddress, location.iSOCode, location.province);

        return true;
    }

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return Is given account an admin 
    function isAdmin(address accAddress) public view returns (bool) {
        return _adminRepository.contains(accAddress);
    }

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return Is given account an admin 
    function returnAdminsPerProvince(Location memory location) public view returns (address[] memory) {
        return _adminRepository.values(_returnLocationKey(location));
    }

    ///@dev Function that returns combined hash of the country's ISO Code and province name 
    /// i. e. the information we want to associate the admin with
    function _returnLocationKey(Location memory location) internal pure returns (bytes32) {
        return keccak256(abi.encodePacked(location.iSOCode, location.province));
    }
}