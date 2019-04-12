import { Component, OnInit } from '@angular/core';
import { MatSnackBar  } from '@angular/material';
import { Router } from "@angular/router"

import * as Chartist from 'chartist';
import TruffleContract from 'truffle-contract';

import { Web3Service } from '../service-proxies/web3.service';
import { LocalStorageService } from '../service-proxies/local-storage.service';

import { MembershipInfo } from '../service-proxies/local-storage.service';

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')

declare var $: any;

declare let require: any;
declare let window: any;

const Web3 = require('web3');


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  status = '';

  marketDeployed: any[] = [];
  clientBaseDeployed: any[] = [];
  producerBaseDeployed: any[] = [];
  
  accounts: string[];

  membershipData: { [key: string] : MembershipInfo } = { }

  model = {
    amount: 5,
    receiver: '',
    balance: 0,
    account: ''
  };

  constructor(
    private web3Service: Web3Service, 
    private matSnackBar: MatSnackBar, 
    private localStoreage: LocalStorageService, 
    private router: Router) { 
  }
  
  ngOnInit(): void {
    console.log('OnInit');

    this.watchAccount();

    this.web3Service.artifactsToContract(producerBaseABI)
      .then((ProducerBaseAbstraction) => {
        ProducerBaseAbstraction.deployed().then((inst) => {
            this.producerBaseDeployed.push(inst)
          });
        });

    this.web3Service.artifactsToContract(regionalMarketABI)
      .then((MarketAbstraction) => {
          MarketAbstraction.deployed().then((inst) => this.marketDeployed.push(inst))
        });

    let that = this

    window.ethereum.on('accountsChanged', async function (accounts) {
      const delay = new Promise(resolve => setTimeout(resolve, 1500));
      await delay;

      await that.SettleMemberships()
    })

    new Promise(resolve => this.SettleMemberships())
  }

  async SettleMemberships() {
    if (this.producerBaseDeployed.length == 0) {
      const delay = new Promise(resolve => setTimeout(resolve, 1000));
      await delay;
      return await this.SettleMemberships();
    }

    let that = this

    this.producerBaseDeployed.forEach(async function (abs) {
      await that.refreshMembership(abs);
    });

    this.marketDeployed.forEach(async function (abs) {
      await that.refreshMembership(abs);
    });
  }

  async refreshMembership(memberBase){
    
    let account = this.web3Service.getDefaultAccount()

    try {
      const transaction = await memberBase.getMembershipInfo.call(account);
      
      let isMember = transaction["isMember"]
      let isAdmin = false;
      let isOwner = transaction["isOwner"]

      this.membershipData[memberBase.address] = { is_member: isMember, is_owner: isOwner, is_admin: isAdmin, is_pending: false }

      // if(isMember){
      //   this.localStoreage.addStoreOwner(account);
      // }

      if (!transaction) {
        this.setStatus('Transaction failed! Cannot validate membership');
      } else if (isMember) {

        this.localStoreage.addOrUpdateCurrentUserMembershipInfo(memberBase.address, isMember, isAdmin, isOwner, false);

      } else {

        this.setStatus('Membership information gathered.');
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error getting membership; see log.');
    }
  }

  async applyForMembershipPB(index) {
        
    this.localStoreage.addPendingMembershipRequests(this.web3Service.getDefaultAccount())

    this.applyForMembership(this.producerBaseDeployed, index)
  }

  async applyForMembershipM(index) {
        
    this.localStoreage.addPendingMembershipRequestsMarket(this.web3Service.getDefaultAccount())

    this.applyForMembership(this.marketDeployed, index)
  }

  private async applyForMembership(memberBases, index) {
    if (index >= 0 && index < memberBases.length) {
      try {
        const default_account = this.web3Service.getDefaultAccount()

        const transaction = await memberBases[index].requestMembership({ from: default_account });

        console.log(transaction)

        if (!transaction) {
          this.setStatus('Transaction failed! Cannot request membership.');
        } else {
          this.setStatus('Transaction succeeded!');

          this.membershipData[memberBases[index].address].is_pending = true;
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error requesting membership; see log. Did you already applied for membership?');
      }
    }
  }

  async revokeMembershipPB(event, index) {
    
    this.revokeMembership(this.producerBaseDeployed, index)
  }

  async revokeMembershipM(event, index) {
    
    this.revokeMembership(this.marketDeployed, index)
  }

  private async revokeMembership(memberBases, index) {
    if (index >= 0 && index < memberBases.length) {
      try {
    
        const default_account = this.web3Service.getDefaultAccount()

        const transaction = await memberBases[index].revokeMembership({ from: default_account });

        if (!transaction) {
          this.setStatus('Transaction failed! Cannot revoke membership.');
        } else {
          this.setStatus('Transaction succeeded!');

          let transactionLogs = transaction.logs

          transactionLogs.forEach(log => {
            if(log.event == "LogMemberRequestingMembershipCancelation"){
              this.showNotification('bottom','center', 'warning', 'Request for Leaving sent.')
            } else if(log.event == "LogMemberLeaving"){
              this.showNotification('bottom','center', 'success', 'Your request has been accepted.')
              this.membershipData[memberBases[index].address].is_member = false;
              this.membershipData[memberBases[index].address].is_admin = false;
              this.membershipData[memberBases[index].address].is_pending = false;
            } else if(log.event == "LogMemberReinstatement") {
              this.showNotification('bottom','center', 'info', 'Time for decision had passed. The request is Reset.')
            } else throw Error
          });
          
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error revoking membership; see log. Are you perhaps the owner of the organisation or maybe the contemplation period did not pass?');
      }
    }
  }

  async transferOwnershipPB(index, formData) {
    this.transferOwnership(this.producerBaseDeployed, index, formData.value.accAddress)

    formData.reset()
  }

  async transferOwnershipM(event, index, newOwner) {
    
    this.transferOwnership(this.marketDeployed, index, newOwner)
  }

  private async transferOwnership(memberBases, index, newOwner) {
    if (index >= 0 && index < memberBases.length) {
      try {
    
        const default_account = this.web3Service.getDefaultAccount()

        const transaction = await memberBases[index].transferOwnership(newOwner, { from: default_account });


        if (!transaction) {
          this.setStatus('Transaction failed! Cannot transfere ownership.');
        } else {
          this.setStatus('Ownership transferred!');

          this.membershipData[memberBases[index].address].is_owner = false;
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error on ownership transfer; see log.');
      }
    }
  }

  producerBaseLoginClickEvent(event, index) { 
    this.login(this.producerBaseDeployed, index)
  }

  marketLoginClickEvent(event, index) { 
    this.login(this.marketDeployed, index)
  }

  login(member_bases, index) { 
    if (index >= 0 && index < member_bases.length) {
      let entityAddress = member_bases[index].address;

      if(this.membershipData[entityAddress].is_member){
        
        this.localStoreage.setLoggedInEntityForCurrentUser(entityAddress)

        this.router.navigate(['/community'])

        this.setStatus('Logging in...');
      }

      return;
    }
  }
  
  async donate(index, formData){

    if (index >= 0 && index < this.marketDeployed.length) {
      let donation = formData.value.donation
      let isoCode = formData.value.isoCode
      let province = formData.value.province

      formData.reset();

      if(isoCode.length != 2 || province.length > 30){
        this.setStatus('Location data incorrect');

        return
      }
      
      let donationInWei = this.web3Service.changeCurrency(donation, 'ether', 'wei');

      let currentUserBalance = this.web3Service.getDefaultAccountBalance()

      if (currentUserBalance < donationInWei) {

        this.setStatus('Donation exceeds user\'s balance');
        return
      }

      try {
        const default_account = this.web3Service.getDefaultAccount()

        isoCode = Web3.utils.fromAscii(isoCode, 2);
        province = Web3.utils.fromAscii(province, 30);

        const transaction = await this.marketDeployed[index].donateToProvince({ iSOCode: isoCode, province: province }, { from: default_account, value: donationInWei });

        if (!transaction) {
          this.setStatus('Transaction failed! Cannot make a donation.');
        } else {
          this.setStatus('Donation successful!');
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error on donation; see log.');
      }
    }
  }

  async register(index, formData){

    if (index >= 0 && index < this.marketDeployed.length) {
      let isoCode = formData.value.isoCode
      let province = formData.value.province

      formData.reset();

      if(isoCode.length != 2 || province.length > 30){
        this.setStatus('Location data incorrect');

        return
      }

      try {
        const default_account = this.web3Service.getDefaultAccount()

        isoCode = Web3.utils.fromAscii(isoCode, 2);
        province = Web3.utils.fromAscii(province, 30);

        const transaction = await this.marketDeployed[index].registerMember({ iSOCode: isoCode, province: province }, { from: default_account });

        if (!transaction) {
          this.setStatus('Transaction failed! Cannot register account.');
        } else {
          this.setStatus('Registration successful!');

          this.membershipData[this.marketDeployed[index].address].is_member = true;
          this.localStoreage.addOrUpdateCurrentUserMembershipInfo(this.marketDeployed[index].address, true, false, false, false)
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error on registration; see log.');
      }
    }
  }

  watchAccount() {
    this.web3Service.accountsObservable.subscribe((accounts) => {

      console.log('in')
      this.accounts = accounts;
      this.model.account = accounts[0];
    });
  }

  setAmount(e) {
    console.log('Setting amount: ' + e.target.value);
    this.model.amount = e.target.value;
  }

  setReceiver(e) {
    console.log('Setting receiver: ' + e.target.value);
    this.model.receiver = e.target.value;
  }
  
  setStatus(status) {
    this.matSnackBar.open(status, null, {duration: 3000});
  }

  showNotification(from, align, color, msg){
    //const type = ['','info','success','warning','danger'];

    $.notify({
        icon: "notifications",
        message: msg

    },{
        type: color,
        timer: 4000,
        placement: {
            from: from,
            align: align
        },
        template: '<div data-notify="container" class="col-xl-4 col-lg-4 col-11 col-sm-4 col-md-4 alert alert-{0} alert-with-icon" role="alert">' +
          '<button mat-button  type="button" aria-hidden="true" class="close mat-button" data-notify="dismiss">  <i class="material-icons">close</i></button>' +
          '<i class="material-icons" data-notify="icon">notifications</i> ' +
          '<span data-notify="title">{1}</span> ' +
          '<span data-notify="message">{2}</span>' +
          '<div class="progress" data-notify="progressbar">' +
            '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
          '</div>' +
          '<a href="{3}" target="{4}" data-notify="url"></a>' +
        '</div>'
    });
  }
}
