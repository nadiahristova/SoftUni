import { Injectable, Inject } from '@angular/core';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';

const STORAGE_KEY = 'user_memberships'
const STORAGE_KEY_ENTITY = 'logged_entity'


export interface MemberBaseInfo {
  [id:string] : { } 
}

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
      id: string, _is_member: boolean, 
      _is_admin: boolean, _is_owner: boolean,
      _is_pending: boolean ): void {
        
      const user = this.storage.get(STORAGE_KEY) || {};

      let membershipInfo : MembershipInfo = { 
        is_member: _is_member || user[id].is_member, 
        is_admin: _is_admin || user[id].is_admin, 
        is_owner: _is_owner || user[id].is_owner,
        is_pending: _is_pending ||  user[id].is_pending}

      user[id] = membershipInfo
      
      this.storage.set(STORAGE_KEY, user);

      console.log(this.storage.get(STORAGE_KEY) || 'LocaL storage is empty');
    }

    public getCurrentUserMembershipInfo(): MemberBaseInfo {

      return this.storage.get(STORAGE_KEY) || { } ;
    }

    public getCurrentUserMembershipInfoForMEmberBase(id: string) {

      if(!this.storage.get(STORAGE_KEY)) return undefined

      let info : MemberBaseInfo = this.storage.get(STORAGE_KEY)

      return info[id] || { } ;
    }

    public getLoggedInEntityForCurrentUser() {

      return this.storage.get(STORAGE_KEY_ENTITY)
    }

    public setLoggedInEntityForCurrentUser(address: string) {
      
      this.storage.set(STORAGE_KEY_ENTITY, address)
    }
}
