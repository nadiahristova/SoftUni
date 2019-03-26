pragma solidity >=0.4.21 <0.6.0;

import "./Ownable.sol";
import "../interfaces/MemberBaseInterface.sol";
import "../interfaces/MarketControllerInterface.sol";

contract MemberBase is Ownable, MemberBaseInterface {

// https://blog.colony.io/writing-upgradeable-contracts-in-solidity-6743f0eecc88/ Upgrade Repository by using library and another contract
// TODO move membership management logic to library

    struct Event {
        uint256 start;
        uint256 end; 
        Location location;
    }

    struct Meeting { 
        uint timeStamp;
        Location location;
    }

    struct Member {
        //address account;
        MembershipActivityStatus status;
        // infromation relevant to user's profile - username, name, email, details etc.
        bytes32 about;
        Location location;
        // followers - another repository
        // subscribers - another repository
    }

    struct Location {
        LocationAccuracy accuracy; 
        int248 altitude; //meters
        int longitude; //seconds
        int latitude; //seconds
    }
    
    mapping (address => Member) members;
    mapping (address => uint) pendingRequestsForMembershipRemoval;

    MarketControllerInterface _marketController;

    uint constant REMOVAL_CONTEMPLATION_PERIOD = 2 days;

    enum LocationAccuracy { 
        Rough, // consider only zip code
        Precise 
    } 

    enum MembershipActivityStatus { NotaMember, IsActiveMember, MembershipCancelationRequestSent, Blocked }
    enum RevokeMembershipStatus { MembershipReinstated, Pending, Removed }

    modifier onlyMember() {
        require(isMember(msg.sender));
        _;
    }

    modifier onlyWhenInitialized() {
        // the only initialization we need for now is a valid market pointer
        require(address(_marketController) != address(0), 'not initialized');
        _;
    }

    event LogMemberRegistrationToMemberBase(address indexed accAddress);
    event LogMemberLeaving(address indexed exMemberAddress);
    event LogMemberReinstatement(address indexed accAddress);
    event LogMemberRequestingMembershipCancelation(address indexed accAddress);
    event LogMarketControllerUpgrade(address oldContractAddress, address newContractAddress);

    function getMarketControllerAddress() public view returns(address) {

        return address(_marketController);
    }

    ///@dev Checkes whether an account is a member to a member base
    ///@param accAddress The address of the account
    function isMember(address accAddress) public view onlyWhenInitialized returns (bool) {

        MembershipActivityStatus status = members[accAddress].status;
        return status == MembershipActivityStatus.IsActiveMember || status == MembershipActivityStatus.MembershipCancelationRequestSent;
    }

    ///@dev Add new member to a member base
    function registerMember() external onlyWhenInitialized {

        address candidateMember = msg.sender;
        require(!isMember(candidateMember));
        
        _marketController.registerMarketMember(candidateMember);

        members[candidateMember].status = MembershipActivityStatus.IsActiveMember;
        emit LogMemberRegistrationToMemberBase(candidateMember);
    }

    ///@dev In case of market swap an existing member can send a join request to the new market 
    /// so he/she can participate in the available activities
    ///@return True on success
    ///@notice Only registered members of the Member Base can use this function
    // The contract system can be updated so that a member base can be associated with several types of market structure
    // and an existing member can send join requests to markets of his/her's interest  
    function sendJoinRequest() external onlyWhenInitialized onlyMember returns (bool) {

        _marketController.registerMarketMember(msg.sender);

        return true;
    }

    ///@dev Upgrades the version of MarketController contract. Used for the first time during deployment of MarketController and MemberBase contracts
    ///@param newMarketController address of the contract with logic and ground rules for a market chain
    function upgradeMarketBase(address newMarketController) external onlyOwner returns (bool) {

        address oldContractAddress = address(_marketController);
       
        _marketController = MarketControllerInterface(newMarketController);
        
        emit LogMarketControllerUpgrade(oldContractAddress, newMarketController);

        return true;
    }

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function revokeMembership() external onlyMember returns (RevokeMembershipStatus) {

        address member = msg.sender;

        MembershipActivityStatus status = members[member].status;

        if(status == MembershipActivityStatus.IsActiveMember) {
            members[member].status = MembershipActivityStatus.MembershipCancelationRequestSent;
            pendingRequestsForMembershipRemoval[member] = now;

            return RevokeMembershipStatus.Pending;
        }

        if(pendingRequestsForMembershipRemoval[member] + REMOVAL_CONTEMPLATION_PERIOD > now) {
            delete pendingRequestsForMembershipRemoval[member];
            delete members[member];

            _marketController.revokeMembership(member);

            emit LogMemberLeaving(member);

            return RevokeMembershipStatus.Removed;
        }

        members[member].status = MembershipActivityStatus.IsActiveMember;

        return RevokeMembershipStatus.MembershipReinstated;
    } 
}