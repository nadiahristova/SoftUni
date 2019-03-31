pragma solidity >=0.5.6 <0.6.0;
pragma experimental ABIEncoderV2;

import "./Ownable.sol";
import "./VotingMemberBase.sol";

import "../libraries/AddressRepositoryLib.sol";
import "../libraries/SafeMath.sol";

// TODO: onlyWhenInitialized

contract Administrable is VotingMemberBase {
    using SafeMath for uint;
    using AddressRepositoryLib for AddressRepositoryLib.Repository;

    AddressRepositoryLib.Repository private _adminRepository;

    modifier onlyAdmin(){
        require(isAdmin(msg.sender));
        _;
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
    function assignAdminToProvince(address accAddress, bytes2 countryISOCode, bytes30 province) 
        public onlyValidAddress(accAddress) onlyOwner onlyNotOwner(accAddress)
    returns (bool) {
        _adminRepository.add(accAddress, _returnLocationKey(countryISOCode, province));

        emit LogAdminAssigned(accAddress, countryISOCode, province);

        return true;
    }

    ///@dev Returns whether an account is an admin
    ///@param accAddress the address of the checked account 
    ///@return True on success
    function removeAdminFromProvince(address accAddress, bytes2 countryISOCode, bytes30 province) 
        public onlyOwner  
    returns (bool) {
        require(isAdmin(accAddress));

        _adminRepository.remove(accAddress, _returnLocationKey(countryISOCode, province));

        emit LogAdminRetired(accAddress, countryISOCode, province);

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
    function returnAdminsPerProvince(bytes2 isoCode, bytes30 province) public view returns (address[] memory) {
        return _adminRepository.values(_returnLocationKey(isoCode, province));
    }

    ///@dev Function that returns combined hash of the country's ISO Code and province name 
    /// i. e. the information we want to associate the admin with
    function _returnLocationKey(bytes2 isoCode, bytes30 province) private pure returns (bytes32) {
        return keccak256(abi.encodePacked(isoCode, province));
    }

    

    function supportProducer(address producer) external returns (bool) {
        return true;
    }

    function voteForMarketMemberBlock(address producer) external returns (bool) {
        return true;
    }
}