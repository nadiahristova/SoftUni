import { Component, OnInit } from '@angular/core';
import { MatSnackBar  } from '@angular/material';
import { Router } from "@angular/router"

import { Web3Service } from '../service-proxies/web3.service';
import { LocalStorageService, MembershipInfo } from '../service-proxies/local-storage.service';

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')


const abi = require('ethereumjs-abi')

const Web3 = require('web3');

declare var $: any;

declare let require: any;
declare let window: any;

@Component({
  selector: 'app-community',
  templateUrl: './community.component.html',
  styleUrls: ['./community.component.scss']
})
export class CommunityComponent implements OnInit {
  status = '';

  marketDeployed: any[] = [];
  clientBaseDeployed: any[] = [];
  producerBaseDeployed: any[] = [];

  profits: any[] = []
  marketReputation: any[] = []
  guildReputation: any[] = []
  
  accounts: string[];

  pendReqForMembership: string[] = [];
  pendReqForMembershipMK: string[] = [];
  approvedmembers: string[] = [];

  currentlyLogedIn:string;

  constructor(
    private web3Service: Web3Service, 
    private matSnackBar: MatSnackBar, 
    private localStoreage: LocalStorageService, 
    private router: Router) { 

    }

  ngOnInit() {

    this.checkForMembership()

    let that = this

    window.ethereum.on('accountsChanged', async function (accounts) {
      that.router.navigate(['/home'])
      that.setStatus('Navigating to Home Page.');
    })

    this.web3Service.artifactsToContract(producerBaseABI)
      .then((ProducerBaseAbstraction) => {
        ProducerBaseAbstraction.deployed().then((inst) => {
            this.producerBaseDeployed.push(inst)

            let currAccount = this.web3Service.getDefaultAccount()

            inst.getVoteWeight(currAccount, {from: currAccount}).then((voteWeight) => this.guildReputation.push(voteWeight))
          });
        });

    this.web3Service.artifactsToContract(regionalMarketABI)
      .then((MarketAbstraction) => {
          MarketAbstraction.deployed().then((inst) => {
            this.marketDeployed.push(inst)

            let currAccount = this.web3Service.getDefaultAccount()

            inst.getAccumolatedProfit({from: currAccount}).then((profit) => this.profits.push(profit))
            inst.getVoteWeight(currAccount, {from: currAccount}).then((voteWeight) => this.marketReputation.push(voteWeight))
          })
        });

    this.gatherPendingRequests();
  }

  checkForMembership() {
    let currentInstitution = this.localStoreage.getLoggedInEntityForCurrentUser();
    let userRights = this.localStoreage.getCurrentUserMembershipInfoForMEmberBase(currentInstitution);

    // if(currentInstitution == undefined || !userRights.is_member) {
    //   this.router.navigate(['/home'])
    //   this.localStoreage.clearStorage()
      
    //   this.setStatus('Navigating to Home Page.');
    // }
  }

  setStatus(status) {
    this.matSnackBar.open(status, null, {duration: 3000});
  }

  gatherPendingRequests() {
    this.pendReqForMembership = this.localStoreage.getPendingMembershipRequests();
    this.pendReqForMembershipMK = this.localStoreage.getPendingMembershipRequestsMarket();
    this.currentlyLogedIn = this.localStoreage.getLoggedInEntityForCurrentUser()
  }

  //====================

  async launchMembershipGrantingCampaign(memberBase, accAddress, isMarket) {
    try {
      const default_account = this.web3Service.getDefaultAccount()

      let transaction = await memberBase.launchMembershipGrantingCampaign(accAddress, { from: default_account });

      console.log(transaction)

      if (!transaction) {
        this.setStatus('Transaction failed! Cannot support member.');
      } else {
        this.setStatus('Transaction succeeded!');
        
        transaction = await memberBase.supportMember(accAddress, 1, { from: default_account });

        console.log(transaction)
        console.log(transaction.logs)

        let transactionLogs = transaction.logs

        transactionLogs.forEach(log => {
          if(log.event == "PropositionAccepted"){
            if(isMarket) {
              this.removeElementFromArray(this.pendReqForMembershipMK, accAddress)
              this.localStoreage.removePendingMembershipRequestsMArket(accAddress)
            } else {
              this.removeElementFromArray(this.pendReqForMembership, accAddress)
              this.localStoreage.removePendingMembershipRequests(accAddress)
  
              this.approvedmembers.push(accAddress)
            }
          }
        });
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error support member; see log.');
    }
  }

  async approveMembershipRequest(memberBase, accAddress) {
      try {
        const default_account = this.web3Service.getDefaultAccount()

        const transaction = await memberBase.registerMember(accAddress, { from: default_account });

        console.log(transaction)

        if (!transaction) {
          this.setStatus('Transaction failed! Cannot approve membership.');
        } else {
          this.setStatus('Transaction succeeded!');

          this.removeElementFromArray(this.approvedmembers, accAddress)
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error approve membership; see log. Did you already applied for membership?');
      }
  }

  async getProfit(memberBase) {
    try {
      const default_account = this.web3Service.getDefaultAccount()

      const transaction = await memberBase.retrieveProfit({ from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed! Funds are retrieved.');
      } else {
        this.setStatus('Transaction succeeded!');
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error retrieving funds; see log');
    }
  }

  async affiliate(market_base, formData){
      let accAddreaa = formData.value.accAddress

      formData.reset();

      try {
        const default_account = this.web3Service.getDefaultAccount()
console.log(default_account)
        const transaction = await market_base.affiliateProducerBase(accAddreaa, { from: default_account });

        if (!transaction) {
          this.setStatus('Transaction failed! Cannot affiliate with guild.');
        } else {
          this.setStatus('Success');
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error; see log.');
      }
  }

  async openStore(market, formData) {
    let guildAddress = formData.value.guildAddress
    let storeName = formData.value.storeName
    console.log(storeName)
    
    storeName = Web3.utils.fromAscii(storeName, storeName.length);

    formData.reset();
    console.log(storeName)
    try {
      const default_account = this.web3Service.getDefaultAccount()

      const transaction = await market.openStore(guildAddress, storeName, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded!');

        this.localStoreage.addStoreOwner(default_account)
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  // async openStoreFront(memberBase) {
  //   try {
  //     const default_account = this.web3Service.getDefaultAccount()

  //     const transaction = await memberBase.addStoreFront({ from: default_account });

  //     if (!transaction) {
  //       this.setStatus('Transaction failed!');
  //     } else {
  //       this.setStatus('Transaction succeeded!');
  //       let transactionLogs = transaction.logs 

  //       transactionLogs.forEach(log => {
  //         if(log.event == "LogStoreFrontCreated"){
  //           this.showNotification('bottom','center', 'success', "New store Front with Id: " + log.args.storeFrontId.toString() + " has been added to the store successfuly.")
  //         }
  //       });
  //     }
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error; see log');
  //   }
  // }

  // async enDisStoreFront(market, formData) {
  //   let storeFrontId = formData.value.storeFrontId
  //   let enDis = formData.value.enable

  //   console.log(enDis)

  //   formData.reset();
    
  //   try {
  //     const default_account = this.web3Service.getDefaultAccount()

  //     let transaction;

  //     if(enDis == true) {
  //       transaction = await market.enableStoreFront(storeFrontId, { from: default_account });
  //     } else {
  //       transaction = await market.disableStoreFront(storeFrontId, { from: default_account });
  //     }

  //     if (!transaction) {
  //       this.setStatus('Transaction failed!');
  //     } else {
  //       if(enDis == true) {
  //         this.setStatus('Transaction succeeded! Store Front enabled');
  //       } else {
  //         this.setStatus('Transaction succeeded! Store Front disabled');
  //       }
  //     }
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error; see log');
  //   }
  // }

  // async publishStoreFront(memberBase, formData){
  //   let storeFrontId = formData.value.storeFrontId
  //   let marketAddress = formData.value.marketAddress

  //   formData.reset();

  //   try {
  //     const default_account = this.web3Service.getDefaultAccount()

  //     let  transaction = await memberBase.publishStoreFrontToMarket(marketAddress, storeFrontId, { from: default_account });

  //     if (!transaction) {
  //       this.setStatus('Transaction failed!');
  //     } else {
  //       this.setStatus('Transaction succeeded! Store Front published');
  //     }
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error; see log');
  //   }
  // }

  async addPartner(memberBase, formData) {
    let marketAddress = formData.value.marketAddress

    formData.reset();

    try {
      const default_account = this.web3Service.getDefaultAccount()

      let  transaction = await memberBase.addMarketPartner(marketAddress, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded! Store Front published');
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  async requestMembership(memberBase, formData) {
    let marketAddress = formData.value.marketAddress

    formData.reset();

    try {
      const default_account = this.web3Service.getDefaultAccount()

      let  transaction = await memberBase.requestMarketMembership(marketAddress, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded! Store Front published');
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

    async getProduct() {
  
  
      try {
        const default_account = this.web3Service.getDefaultAccount()
  
        let  transaction = await this.producerBaseDeployed[0].getProduct(2, { from: default_account });
  
        if (!transaction) {
          this.setStatus('Transaction failed!');
        } else {
          this.setStatus('Transaction succeeded! Store Front published');
          console.log(transaction)
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error; see log');
      }
    }

  // async addProduct(memberBase, formData) {

  //   let storeFronId = formData.value.storeFrontId
  //   let specificationId = formData.value.specificationId
  //   let pricePerUnit = parseInt(formData.value.pricePerUnit)
  //   let amount = parseInt(formData.value.amount)
  //   let hasNegotiablePrice = formData.value.hasNegotiablePrice == 'true'

  //   formData.reset();

  //   try {
  //     const default_account = this.web3Service.getDefaultAccount()

  //     let  transaction = await memberBase.addProductToStoreFront(storeFronId, specificationId, pricePerUnit, amount, hasNegotiablePrice, { from: default_account });

  //     if (!transaction) {
  //       this.setStatus('Transaction failed!');
  //     } else {
  //       let transactionLogs = transaction.logs 
  //       console.log(transactionLogs)
        
  //       transactionLogs.forEach(log => {
  //         if(log.event == "LogProductAddedToStoreFront"){
  //           this.showNotification('bottom','center', 'success', "New product with Id: " + log.args.productId.toString() + " has been added to the store successfuly.")
  //         }
  //       });
  //     }
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error; see log');
  //   }
  // }

  private async signOrder(seller, buyer, productId, amount, pricePerUnit, validUntil, nonce) {
    var hash = "0x" + abi.soliditySHA3(
      ["address", "address", "uint256", "uint256", "uint256", "uint256", "uint256"],
      [seller, buyer, productId, amount, pricePerUnit, validUntil, nonce]
    ).toString("hex");

    hash = "0x" + abi.soliditySHA3(
      ["string", "bytes32"],
      ["\x19Ethereum Signed Message:\n32", hash]
    ).toString("hex");

    return await this.web3Service.signHash(hash, seller)
  }

  async signOrderAcc() {
    const default_account = this.web3Service.getDefaultAccount()
    const buyer = '0x4762352F433415B87C4909d971037dDFC6e772E1'

    let invoice = {
      seller: '0xb9131879580245c560c1b884E91840FB5336D52a', 
      buyer: '0x4762352F433415B87C4909d971037dDFC6e772E1',
      producerBase: this.producerBaseDeployed[0].address,
      storeFrontId: 3,
      productId: 2, 
      amount: 5, 
      pricePerUnit: 100, 
      validUntil: this.getDateInUnixTimestamp('08/08/2019')
    }

    let nonce = await this.web3Service.getNonce(default_account)

    console.log('nonce: ' + nonce)

    let signedOrder = await this.signOrder(invoice.seller, invoice.buyer, invoice.productId, 
      invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce)

    console.log('signedOrder: ' + signedOrder)
  }

  async test() {

    const default_account = '0xb9131879580245c560c1b884E91840FB5336D52a'
    const buyer = this.web3Service.getDefaultAccount()

    let invoice = {
      seller: '0xb9131879580245c560c1b884E91840FB5336D52a', 
      buyer: '0x4762352F433415B87C4909d971037dDFC6e772E1',
      producerBase: this.producerBaseDeployed[0].address,
      storeFrontId: 3,
      productId: 2, 
      amount: 5, 
      pricePerUnit: 100, 
      validUntil: this.getDateInUnixTimestamp('08/08/2019')
    }

    let nonce = 26
    // try {

      let signedOrder = '0xaca56ccfdc25cfda4ae1f5fc4bedd124340681b7ab083f5da1bdc0b9d3d2f0b97bc63ac451dee4d155c2f29d7ec343ef481c97b12a7511bda076af84f7e541741b'

      let  transaction = await this.marketDeployed[0].buyProduct(invoice, nonce, signedOrder, { from: buyer, value: 100000 });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction successful!');
        let transactionLogs = transaction.logs 
        // console.log(transactionLogs)
        
        // transactionLogs.forEach(log => {
        //   if(log.event == "LogProductAddedToStoreFront"){
        //     this.showNotification('bottom','center', 'success', "New product with Id: " + log.args.productId.toString() + " has been added to the store successfuly.")
        //   }
        // });
      }
    // } catch (e) {
    //   console.log(e);
    //   this.setStatus('Error; see log');
    // }
  }

  private getDateInUnixTimestamp = (date) => Math.floor((new Date(date)).getTime()/ 1000);

  private isCurrOwner(marketAddr){
    if(this.currentlyLogedIn != marketAddr) {
      return false;
    }

    return this.localStoreage.getCurrentUserMembershipInfoForMEmberBase(marketAddr).is_owner
  }

  private removeElementFromArray(arr, el) {
    const index = arr.indexOf(el, 0);

      if (index > -1) {
        arr.splice(index, 1);
      }
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
