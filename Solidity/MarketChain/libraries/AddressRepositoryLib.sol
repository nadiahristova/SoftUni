pragma solidity >=0.5.6 <0.6.0;
pragma experimental ABIEncoderV2;

library AddressRepositoryLib {
    // TODO: remove hardcoded restrictions? Move their assignment to an initialize method
    uint128 constant MAX_NUM_OF_VALUE_ASSIGNMENTS = 5;
    uint128 constant MAX_NUM_OF_VALUES_PER_KEY = 55;

    struct Repository {
        /** ///@dev Maps hashed country's ISO Code and country region to set of admin account addresses */

        ///@dev Maps hashed information key relative to the values in the repository
        mapping(bytes32 => address[]) _values;

        /** 
            * ///@dev Maps hashed value of given country ISO Code, admin's address and given's country region to the value of 
            * /// admin's address index + 1 in _values 
        */

        ///@dev Maps hash of the combination between hashed relative information and the value itself, to the value's index + 1 in the _values 
        mapping(bytes32 => uint) _valueIndexMap;

        /** ///@dev Keeps track of how many times a given account is assigned as an admin to a province */

        ///@dev Keeps track of how many times relevant information is assigned to a given value
        mapping(address => uint) _valueAssignmentsCount;
    }

    ///@dev Adds value to the repository
    ///@param value the address of the account 
    ///@param relatedInfo hashed value of information relevant to the kept values
    ///@return True on success
    function add(Repository storage self, address value, bytes32 relatedInfo) 
        internal 
    returns (bool) {
        uint assignCountPerValue = self._valueAssignmentsCount[value]; 
        require(assignCountPerValue < MAX_NUM_OF_VALUE_ASSIGNMENTS); 

        bytes32 combinedKey = _returnCombinedKey(value, relatedInfo);
        require(self._valueIndexMap[combinedKey] == 0);

        uint length = self._values[relatedInfo].length;
        require(length < MAX_NUM_OF_VALUES_PER_KEY);
       
        self._values[relatedInfo].push(value);

        self._valueIndexMap[combinedKey] = ++ length;// basically the new index + 1
        self._valueAssignmentsCount[value] = ++ assignCountPerValue;

        return true;
    }

    ///@dev Removes value form repository
    ///@param value the address of the account 
    ///@param relatedInfo hashed information associated with the values
    ///@return True on success
    function remove(Repository storage self, address value, bytes32 relatedInfo) 
        public   
    returns (bool) {
        bytes32 combinedKey = _returnCombinedKey(value, relatedInfo);
        uint repoValueIndex = self._valueIndexMap[combinedKey];
        require(repoValueIndex != 0);
        
        uint length = self._values[relatedInfo].length;
        assert(repoValueIndex <= length);
        
        if (length > 1) {
            // actualLength is the index of the last valid(assigned) record
            address lastValidRecod = self._values[relatedInfo][--length];

            // we remove last record
            self._values[relatedInfo].pop();

            // overwrite the address we want to delete with the last valid value
            self._values[relatedInfo][repoValueIndex - 1] = lastValidRecod;
            // we are saving the new index for the moved value
            self._valueIndexMap[_returnCombinedKey(lastValidRecod, relatedInfo)] == repoValueIndex;
        } else {
            assert(length == 1);
            delete self._values[relatedInfo];
        }

        // we are marking the old value as removed from the repository
        delete self._valueIndexMap[combinedKey];
        // we are decreasing num of value assignments
        self._valueAssignmentsCount[value] -= 1;

        return true;
    }

    ///@dev Returns whether address is in the repository
    ///@param value the address of the account 
    ///@return Exists 
    function contains(Repository storage self, address value) public view returns (bool) {
        return self._valueAssignmentsCount[value] != 0;
    }

    ///@dev Return all values associated with relevant hased information i. e. key
    ///@param relatedInfo hashed value of information relevant to the kept values
    ///@return Array of values associated with the input information
    function values(Repository storage self, bytes32 relatedInfo) public view returns (address[] memory) {
        return self._values[relatedInfo];
    }

    ///@dev Function that returns combined hash of the value and it's related information
    function _returnCombinedKey(address value, bytes32 relatedInfo) private pure returns (bytes32) {
        return keccak256(abi.encodePacked(value, relatedInfo));
    }
}