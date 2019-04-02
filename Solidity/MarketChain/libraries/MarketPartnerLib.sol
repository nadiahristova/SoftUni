pragma solidity >=0.5.6 <0.6.0;

library MarketPartnerLib {

    struct PartnerMarkets {
        mapping(address => mapping(address => bool)) _memberToMarketMap;
        address[25] _marketPartners;
    }
    
    event LogMarketPartnerAdded(address indexed partnerAddress);
    event LogMarketPartnerRemoved(address indexed exMemberAddress);
    event LogMarketMembershipConfirmed(address indexed accAddress, address indexed marketAddress);
    event LogMarketMembershipRevoked(address indexed accAddress, address indexed marketAddress);
    
    modifier onlyContractAddress(address adr) {
        require(_isContract(adr));
        _;
    }

    function _addMarketPartner (PartnerMarkets storage self, address market) 
        internal 
        onlyContractAddress(market)
    returns(bool) {
        address[25] memory markets = self._marketPartners;

        for(uint i = 0; i < markets.length; i++) {
            if(markets[i] == address(0)) {

                self._marketPartners[i] = market;

                emit LogMarketPartnerAdded(markets[i]);

                return true;
            } 
        }

        return false;
    }

    function _removeMarketPartner (PartnerMarkets storage self, address market) 
        internal 
    returns(bool) {
        address[25] memory markets = self._marketPartners;

        int indexOfRemovedMarket = -1;
        for(uint i = 0; i < markets.length - 1; i++) {
            if(indexOfRemovedMarket == -1 && markets[i] == market) {
                indexOfRemovedMarket = int(i);
            } else if(indexOfRemovedMarket != -1 && markets[i + 1] == address(0)){
                self._marketPartners[uint(indexOfRemovedMarket)] = markets[i];
                delete self._marketPartners[i];

                emit LogMarketPartnerRemoved(market);

                return true;
            }
        }

        return false;
    }

    function _hasMarketMembership (PartnerMarkets storage self, address market, address accAddress) 
        view
        internal 
    returns (bool) {

        return self._memberToMarketMap[market][accAddress];
    }

    function _addMarketMembership (PartnerMarkets storage self, address market, address accAddress) 
        internal
        onlyContractAddress(market) 
    returns (bool) {
        
        if(self._memberToMarketMap[market][accAddress]) return false;

        self._memberToMarketMap[market][accAddress] = true;

        emit LogMarketMembershipConfirmed(accAddress, address(market));

        return true;
    }

    function _revokeMarketMembership (PartnerMarkets storage self, address market, address accAddress) 
        internal 
    returns (bool) {

        if(!self._memberToMarketMap[market][accAddress]) return false;

        delete self._memberToMarketMap[market][accAddress];
        
        emit LogMarketMembershipRevoked(accAddress, market);

        return true;
    }

    function _removeAllMarketMemberships(PartnerMarkets storage self, address accAddress) 
        internal {

        address[25] memory markets = self._marketPartners;

        for(uint i; markets[i] != address(0) && (i <  markets.length); i++){
            _revokeMarketMembership(self, markets[i], accAddress);
        }
    }

    function _isMarketRegisteredPartner (PartnerMarkets storage self, address market) 
        view
        internal 
    returns(bool) {
        address[25] memory markets = self._marketPartners;

        for(uint i = 0; i < markets.length; i++) {
            if(markets[i] == market) {

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