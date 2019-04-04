pragma solidity >=0.5.6 <0.6.0;

// make more generic
library PartnerRelationsKeeperLib {

    uint constant MAX_NUM_OF_PARTNERS = 25;

    struct Partners {
        mapping(address => mapping(address => bool)) _memberToPartnerMap;
        address[25] _partners;
    }
    
    event LogPartnerEntityAdded(address indexed partnerAddress);
    event LogPartnerEntityRemoved(address indexed partnerAddress);
    event LogAffiliationWithPartner(address indexed accAddress, address indexed partnerAddress);
    event LogAffiliationWithPartnerRevoked(address indexed accAddress, address indexed partnerAddress);
    
    modifier onlyContractAddress(address adr) {
        require(_isContract(adr));
        _;
    }

    function _addPartner (Partners storage self, address partner) 
        internal 
        onlyContractAddress(partner)
    returns(bool) {
        address[25] memory partners = self._partners;

        for(uint i = 0; i < MAX_NUM_OF_PARTNERS; i++) {
            if(partners[i] == address(0)) {

                self._partners[i] = partner;

                emit LogPartnerEntityAdded(partner);

                return true;
            } 
        }

        return false;
    }

    function _removePartner (Partners storage self, address partner) 
        internal 
    returns(bool) {
        address[25] memory partners = self._partners;

        uint i;
        uint indexOfRemovedMarket = MAX_NUM_OF_PARTNERS;

        for(i = 0; partners[i] != address(0) && i < MAX_NUM_OF_PARTNERS; i++) {
            if(partners[i] == partner) {
                indexOfRemovedMarket = i;
            } 
        }

        if(indexOfRemovedMarket != MAX_NUM_OF_PARTNERS) {
            i--;
            if(i > 0) {
				self._partners[indexOfRemovedMarket] = partners[i];
			}

            delete self._partners[i];

            emit LogPartnerEntityRemoved(partner);
        }

        return false;
    }

    function _hasMembership (Partners storage self, address partner, address accAddress) 
        view
        internal 
    returns (bool) {

        return self._memberToPartnerMap[partner][accAddress];
    }

    function _addMembership (Partners storage self, address partner, address accAddress) 
        internal
        onlyContractAddress(partner) 
    returns (bool) {
        
        if(self._memberToPartnerMap[partner][accAddress]) return false;

        self._memberToPartnerMap[partner][accAddress] = true;

        emit LogAffiliationWithPartner(accAddress, address(partner));

        return true;
    }

    function _revokeMembership (Partners storage self, address partner, address accAddress) 
        internal 
    returns (bool) {

        if(!self._memberToPartnerMap[partner][accAddress]) return false;

        delete self._memberToPartnerMap[partner][accAddress];
        
        emit LogAffiliationWithPartnerRevoked(accAddress, partner);

        return true;
    }

    function _removeAllMemberships(Partners storage self, address accAddress) 
        internal {

        address[25] memory partners = self._partners;

        for(uint i; partners[i] != address(0) && (i < MAX_NUM_OF_PARTNERS); i++){
            _revokeMembership(self, partners[i], accAddress);
        }
    }

    function _isRegisteredPartner (Partners storage self, address partner) 
        view
        internal 
    returns(bool) {
        address[25] memory partners = self._partners;

        for(uint i = 0; partners[i] != address(0) && i < MAX_NUM_OF_PARTNERS; i++) {
            if(partners[i] == partner) {

                return true;
            } 
        }

        return false;
    }
    
    function _isContract(address accAddress) private view returns (bool){
      uint32 size;

      assembly {
        size := extcodesize(accAddress)
      }

      return (size > 0);
    }
}