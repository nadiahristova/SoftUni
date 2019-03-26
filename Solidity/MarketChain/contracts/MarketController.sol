pragma solidity >=0.4.22 <0.6.0;

import "./Administrable.sol";
import "./BaseContract.sol";
import "../interfaces/MarketControllerInterface.sol";

// TODO: add mortal
contract MarketController is BaseContract, MarketControllerInterface, Administrable {

    // default ThirdParty means that the address is not a member
    enum MemberKind { ThirdParty, Producer, Client }

    ///@dev keep track of participants in the market chain and their vote weight
    mapping(address => Member) memberInfo;

    struct Member {
        MemberKind kind;
        uint votingWeight;
    }

    bool private _isInitialized;

    address private _clientBaseContractAddress;
    address private _producerBaseContractAddress;

    event LogSuccessfulInitialization();
    event LogClientBaseAssigned(address indexed clientBaseAddress);
    event LogProducerBaseAssigned(address indexed producerBaseAddress);
    event LogMemberRevokedMembership(address indexed exMember);
    event LogNewClientRegistered(address indexed newClientAddress);
    event LogNewProducerRegistered(address indexed newProducerAddress);

/** 
    modifier onlyClientBaseCall() {
        require(msg.sender == _clientBaseContractAddress);
        _;
    }

    modifier onlyProducerBaseCall() {
        require(msg.sender == _producerBaseContractAddress);
        _;
    }
*/

    modifier onlyWhenInitialized() {
        require(_isInitialized);
        _;
    }

    modifier onlyMemberBaseCall() {
        require(msg.sender == _clientBaseContractAddress || msg.sender == _producerBaseContractAddress);
        _;
    }

    modifier onlyMember(address accAddress) {
        require(isMember(accAddress));
        _;
    }

    function isMember(address accountAddress) public view returns (bool) {
        return memberInfo[accountAddress].kind == MemberKind.ThirdParty;
    }


    //TODO: try to pass interfaces
    ///@dev Initializes constants needed for some ground rules and limitations as well as adds Member Base dependencies 
    ///@param clientBase Address of the Client Base Contract
    ///@param producerBase Address of the Producer Base Contract
    ///@notice Member Base dependencies cannot be changed, market cannot be updated it can only be swapt
    // Downside: all of the member relevant information (vote weight, etc.) WILL be lost
    function initialize(address clientBase, address producerBase) external onlyOwner returns (bool) { 
        require(!_isInitialized);

        _clientBaseContractAddress = clientBase;
        _producerBaseContractAddress = producerBase;

        _isInitialized = true;
        
        emit LogClientBaseAssigned(clientBase);
        emit LogProducerBaseAssigned(producerBase);
        emit LogSuccessfulInitialization();

        return true;
    }

    ///@dev Registers a member from a member base to the current market chain
    ///@param memberCandidate Account address of the member candidate
    ///@notice Function only accepts calls form registered MemberBase Contracts
    function registerMarketMember(address memberCandidate) 
        public 
        onlyWhenInitialized 
        onlyValidAddress(memberCandidate) 
        onlyMemberBaseCall 
    {
        require(!isMember(memberCandidate));

        if(msg.sender == _clientBaseContractAddress){
            memberInfo[memberCandidate].kind = MemberKind.Client;

            emit LogNewClientRegistered(memberCandidate);
        } else {
            memberInfo[memberCandidate].kind = MemberKind.Producer;

            emit LogNewProducerRegistered(memberCandidate);
        }
    }

    ///@dev Revokes account membership
    ///@param member Account address of the member 
    ///@notice Function only accepts calls form registered MemberBase Contracts
    function revokeMembership(address member) external onlyWhenInitialized onlyMemberBaseCall {
       delete memberInfo[member];

       emit LogMemberRevokedMembership(member);
    }

    function supportProducer(address producer) external onlyWhenInitialized returns (bool) {
        return true;
    }

    function voteForMarketMemberBlock(address producer) external onlyWhenInitialized returns (bool) {
        return true;
    }
}