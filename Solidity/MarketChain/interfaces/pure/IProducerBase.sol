pragma solidity >=0.5.6 <0.6.0;

interface IProducerBase {

    function revokeMembershipImmediately(address accAddress) external;

    function launchMembershipRevocationCampaign (address accAddress) external returns(bool);

    function registerMember(address accAddress) external;

    function supportMember(address accAddress, uint248 votingCampaignId) external;

    function addStoreFront() external;

    function removeStoreFront(uint storeFrontId) external;

    function disableStoreFront(uint storeFrontId) external;

    function enableStoreFront(uint storeFrontId) external;

    function publishStoreFrontToMarket(address market, uint storeFrontId) external;

    function addProductToStoreFront(
            uint storeFrontId,
            uint specificationId, 
            uint pricePerUnit, 
            uint amount, 
            bool hasNegotiablePrice) external;

    function removeProductFromStoreFront(uint storeFrontId, uint productId) external;

    function updateProduct(
            uint storeFrontId,
            uint productId, 
            uint pricePerUnit, 
            uint amountProduced, 
            bool hasNegotiablePrice) external;
    
    function getMembershipInfo(address accAddress) external view returns (bool isMember, bool isOwner);
}
