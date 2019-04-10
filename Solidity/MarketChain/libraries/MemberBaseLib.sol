pragma solidity >=0.5.6 <0.6.0;

library MemberBaseLib {
    struct Members {
        mapping (address => Member) members;
        mapping (address => uint) pendingRequestsForMembershipCancelation;
    }
    
    struct Member {
        uint joinedAt;
        MembershipActivityStatus status;
    }

    uint constant REMOVAL_CONTEMPLATION_PERIOD = 2 days;
    uint constant LEAVE_WINDOW_PERIOD = 3 days;
    
    enum MembershipActivityStatus { NotaMember, IsActiveMember, MembershipCancelationRequestSent, Blocked }
    enum RevokeMembershipStatus { MembershipReinstated, Pending, Removed }

    event LogMemberRegistration(address indexed accAddress);
    event LogMemberLeaving(address indexed exMemberAddress);
    event LogMemberRemoved(address indexed removedBy, address indexed exMemberAddress);
    event LogMemberReinstatement(address indexed accAddress);
    event LogMemberRequestingMembershipCancelation(address indexed accAddress);

    ///@dev Checkes whether an account is a member to a member base
    ///@param accAddress The address of the account
    function _isMember(Members storage self, address accAddress) 
        view
        internal 
    returns (bool) {
        MembershipActivityStatus status = self.members[accAddress].status;

        return status == MembershipActivityStatus.IsActiveMember || status == MembershipActivityStatus.MembershipCancelationRequestSent;
    }

    ///@dev Add new member to a member base
    function _registerMember(Members storage self, address candidateMember) 
        internal 
    returns(bool) {
        if (_isMember(self, candidateMember)) return false;

        self.members[candidateMember].status = MembershipActivityStatus.IsActiveMember;
        self.members[candidateMember].joinedAt = now;

        emit LogMemberRegistration(candidateMember);

        return true;
    }

    ///@dev Used for a request or confirmation of membership revocation 
    ///@return Membership revocation status
    function _revokeMembership(Members storage self, address accAddress) 
        internal 
    returns (RevokeMembershipStatus) {
        require(_isMember(self, accAddress));

        MembershipActivityStatus status = self.members[accAddress].status;

        // set pending request
        if(status == MembershipActivityStatus.IsActiveMember) {
            self.members[accAddress].status = MembershipActivityStatus.MembershipCancelationRequestSent;
            self.pendingRequestsForMembershipCancelation[accAddress] = now;

            emit LogMemberRequestingMembershipCancelation(accAddress);

            return RevokeMembershipStatus.Pending;
        }

        uint endOfContemplationPeriod = self.pendingRequestsForMembershipCancelation[accAddress] + REMOVAL_CONTEMPLATION_PERIOD;

        require(now > endOfContemplationPeriod, '17');

        // remove membership
        if(now <= endOfContemplationPeriod + LEAVE_WINDOW_PERIOD) {

            delete self.pendingRequestsForMembershipCancelation[accAddress];
            delete self.members[accAddress];

            emit LogMemberLeaving(accAddress);

            return RevokeMembershipStatus.Removed;
        }

        // reset membership
        self.members[accAddress].status = MembershipActivityStatus.IsActiveMember;

        emit LogMemberReinstatement(accAddress);

        return RevokeMembershipStatus.MembershipReinstated;
    } 

    ///@dev Used for forced revoking of membership 
    function _revokeMembershipForced(Members storage self, address accAddress) 
        internal 
    returns (bool) {
        require(_isMember(self, accAddress));

        delete self.pendingRequestsForMembershipCancelation[accAddress];
        delete self.members[accAddress];

        emit LogMemberRemoved(msg.sender, accAddress);

        return true;
    } 
}