
pragma solidity >=0.5.6 <0.6.0;

contract ProductStandartInterface {
    function addRecord (bytes32 category, bytes32 description) public returns(uint);

    function removeRecord (uint recordId) public returns(uint);

    function getRecordId (bytes32 description) public view returns (uint); 

    function getRecordIds (bytes32 category) public view returns (uint[] memory); 
}