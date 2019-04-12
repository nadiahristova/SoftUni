import { Injectable, Inject } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';

const STORAGE_KEY_ENTITY = 'logged_entity'
const STORAGE_KEY_PENDING = 'pending_mem_requests'
const STORAGE_KEY_PENDING_MARKET = 'pending_mem_requests_market'
const STORE_OWNERS = 'store_owners'
const OFFERS = 'offers'


// export interface MemberBaseInfo {
//   [id:string] : MembershipInfo 
// }

export interface MembershipInfo {
  is_member: boolean;
  is_owner: boolean;
  is_admin: boolean;
  is_pending: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
    
    constructor(@Inject(LOCAL_STORAGE) private storage: StorageService) { }


    public addOrUpdateCurrentUserMembershipInfo(
      id: string, _is_member: boolean | undefined, 
      _is_admin: boolean | undefined, _is_owner: boolean | undefined,
      _is_pending: boolean | undefined): void {

      let userInfo = this.getCurrentUserMembershipInfoForMEmberBase(id)

      userInfo.is_member = _is_member || userInfo.is_member;
      userInfo.is_admin = _is_admin || userInfo.is_admin;
      userInfo.is_owner = _is_owner || userInfo.is_owner;
      userInfo.is_pending = _is_pending || userInfo.is_pending;
      
      this.storage.set(id, JSON.stringify(userInfo));
    }

    public getCurrentUserMembershipInfoForMEmberBase(id: string) : MembershipInfo {
      
      let userInfo = this.storage.get(id) 

      return userInfo ? JSON.parse(userInfo) : { is_member: false, is_admin: false, is_owner: false, is_pending: false}
    }

    public getLoggedInEntityForCurrentUser() {

      return this.storage.get(STORAGE_KEY_ENTITY)
    }

    public setLoggedInEntityForCurrentUser(address: string) {
      
      this.storage.set(STORAGE_KEY_ENTITY, address)
    }

    public addStoreOwner(address: string){
      const requests = this.getStoreOwners()

      requests.push(address)
      
      this.storage.set(STORE_OWNERS, JSON.stringify(requests))
    }

    public getStoreOwners(){
      let req = this.storage.get(STORE_OWNERS) 

      return req ? JSON.parse(req) : [];
    }

    public getPendingMembershipRequests() {
      let req = this.storage.get(STORAGE_KEY_PENDING) 

      return req ? JSON.parse(req) : [];
    }

    public addPendingMembershipRequests(address: string) {
      
      const requests = this.getPendingMembershipRequests()

      requests.push(address)
      
      this.storage.set(STORAGE_KEY_PENDING, JSON.stringify(requests))
    }

    public removePendingMembershipRequests(address: string) {
      
      const requests = this.getPendingMembershipRequests()

      const index = requests.indexOf(address, 0);
      if (index > -1) {
        requests.splice(index, 1);
      }
      
      this.storage.set(STORAGE_KEY_PENDING, JSON.stringify(requests))
    }

    public getPendingMembershipRequestsMarket() {
      let req = this.storage.get(STORAGE_KEY_PENDING_MARKET) 

      return req ? JSON.parse(req) : [];
    }

    public addPendingMembershipRequestsMarket(address: string) {
      
      const requests = this.getPendingMembershipRequestsMarket()

      requests.push(address)
      
      this.storage.set(STORAGE_KEY_PENDING_MARKET, JSON.stringify(requests))
    }

    public getOffers() {
      let req = this.storage.get(OFFERS) 

      return req ? JSON.parse(req) : [];
    }

    public addOffer(of: any) {
      
      const requests = this.getOffers()

      requests.push(of)
      
      this.storage.set(OFFERS, JSON.stringify(requests))
    }

    public removePendingMembershipRequestsMArket(address: string) {
      
      const requests = this.getPendingMembershipRequestsMarket()

      const index = requests.indexOf(address, 0);
      if (index > -1) {
        requests.splice(index, 1);
      }
      
      this.storage.set(STORAGE_KEY_PENDING_MARKET, JSON.stringify(requests))
    }

    public clearStorage() {
      this.storage.clear();
    }
}
