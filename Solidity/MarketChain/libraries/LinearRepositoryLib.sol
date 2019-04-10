pragma solidity >=0.5.6 <0.6.0;

pragma experimental ABIEncoderV2;

import "./SafeMath.sol";

library LinearRepositoryLib {
    using SafeMath for uint256;
   
    struct Repo {
        uint256 _itemIds;

        mapping (uint => uint) _itemToInventoryIndexMap;
        mapping (uint => uint[]) _repoNicheInventory; 
    }

    /** Item Logic */
    function _addItemToInventory (
            Repo storage self,
            uint nicheId,
            uint256 maxNumItemsPerRepoNiche) 
        internal
    returns(uint) {
        uint nicheItemCount = self._repoNicheInventory[nicheId].length;

        require(nicheItemCount < maxNumItemsPerRepoNiche, '18');
        
        uint newItemId = self._itemIds + 1;
        self._repoNicheInventory[nicheId].push(newItemId);

        // the index itself + 1 for the flag
        self._itemToInventoryIndexMap[newItemId] = nicheItemCount + 1;
        self._itemIds = newItemId;

        return newItemId;
    }

    function _removeItemFromInventory(
            Repo storage self,
            uint nicheId,
            uint itemId) 
        internal {
            
        uint nicheItemCount = self._repoNicheInventory[nicheId].length;

        uint itemIndex_deleted = self._itemToInventoryIndexMap[itemId] - 1;

        assert(itemIndex_deleted < nicheItemCount);

        if(nicheItemCount > 1) {
            //we take the id of the last item
            uint itemId_last = self._repoNicheInventory[nicheId][nicheItemCount - 1];
            // we set the index of the last item at the place of the deleted item id
            self._repoNicheInventory[nicheId][itemIndex_deleted] = itemId_last;
            // we set a pointer to the new index fo the last element + 1
            self._itemToInventoryIndexMap[itemId_last] = itemIndex_deleted + 1;
        }

        // deletion of the records for the last element in the given inventory
        self._repoNicheInventory[nicheId].pop();
        delete self._itemToInventoryIndexMap[itemId];
    }

    function _getItemIdsByPageNum(Repo storage self, uint nicheId, uint pageNum, uint itemsPerPage) 
        view
        internal 
    returns (uint[] memory) {
        uint[] memory pagedItemIds = new uint[](itemsPerPage);

        uint[] storage itemIds = self._repoNicheInventory[nicheId];

        uint itemsCount = itemIds.length;

        uint pageStartIndex = pageNum.mul(itemsPerPage);

        if(pageStartIndex < itemsCount) {

            uint pageEndIndex = (pageNum.add(1)).mul(itemsPerPage).sub(1);

            for(uint i = pageStartIndex; i < itemsCount && i <= pageEndIndex; i++){
              
                uint localIndex = i.sub(pageStartIndex);
                pagedItemIds[localIndex] = itemIds[i];
            }
        }

        return pagedItemIds;
    }

    function _inStock(Repo storage self, uint productId) 
        view 
        internal
    returns (bool){
        return self._itemToInventoryIndexMap[productId] != 0;
    }
}