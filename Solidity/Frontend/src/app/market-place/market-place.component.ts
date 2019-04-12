import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MatSnackBar  } from '@angular/material';
import { Router } from "@angular/router"

import { Web3Service } from '../service-proxies/web3.service';
import { LocalStorageService, MembershipInfo } from '../service-proxies/local-storage.service';

const abi = require('ethereumjs-abi')
const Web3 = require('web3');

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')

declare var $: any;

declare let require: any;
declare let window: any;

export interface Invoice{
  seller: string; 
  buyer: string;
  producerBase: string;
  storeFrontId: Number
  productId: Number
  amount: Number 
  pricePerUnit: Number 
  validUntil: Number
  nonce: Number;
  signed: string;
}

interface StoreFrontRow{
  id: Number; 
  producerIndex: Number;
  marketIndex: Number;
  owner: string;
  createdAt: string;
}

interface ProductRow {
  id: Number; 
  producerIndex: Number;
  owner: string;
  specificationId: Number;
  //editedAt: string;
  amount: Number;
  pricePerUnit: Number;
  hasNegotiablePrice: boolean;
}

// interface Offer {
//   productId: Number; 
//   storeFrontId: Number;
//   producerBaseIndex: Number;
//   owner: string;
//   amount: Number;
//   pricePerUnit: Number;
//   nonce: Number;
//   signed: string;
// }

@Component({
  selector: 'app-market-place',
  templateUrl: './market-place.component.html',
  styleUrls: ['./market-place.component.scss']
})
export class MarketPlaceComponent implements OnInit {
  status = '';

  marketDeployed: any[] = [];
  clientBaseDeployed: any[] = [];
  producerBaseDeployed: any[] = [];
  
  currentlyLogedIn:string;
  accounts: string[];

  signedOrder: string;
  nonce: Number;

  @Input() pageNum: Number = 0;
  @Input() productPageNum: Number = 0;

  storeFrontTable: StoreFrontRow[]
  productTable: ProductRow[]

  chosenStoreFront: Number;
  chosenOwner: string;
  chosenProducer: string;
  chosenProductId: Number;
  makeOffer: boolean;

  validUntil: string = ''

  myOffers: any[] = [];
  mySuccessfulOffers: any[] = [];


  addProductInfo = {
    storeFrontId: 0,
    producerBaseIndex: 0
  }
  addProductToStoreFrontId: Number = 0;

  filterData = {
    chosenMarketId: '',
    stoFrontPage: 0,
    chosenStoreFrontId: 0,
    itemsPage: 0
  }

  constructor(
    private web3Service: Web3Service, 
    private matSnackBar: MatSnackBar, 
    private localStoreage: LocalStorageService, 
    private router: Router) { }

  ngOnInit() {

    // let that = this

    window.ethereum.on('accountsChanged', async function (accounts) {
      // that.router.navigate(['/home'])
      // that.setStatus('Navigating to Home Page.');
      location.reload()
    })

    this.web3Service.artifactsToContract(producerBaseABI)
      .then((ProducerBaseAbstraction) => {
        ProducerBaseAbstraction.deployed().then((inst) => {
            this.producerBaseDeployed.push(inst)
          });
        });

    this.web3Service.artifactsToContract(regionalMarketABI)
      .then((MarketAbstraction) => {
          MarketAbstraction.deployed().then((inst) => {
            this.marketDeployed.push(inst)
          })
        });

    new Promise(resolve => this.loadInitData())
  }

  setStatus(status) {
    this.matSnackBar.open(status, null, {duration: 3000});
  }

  async loadInitData(){
    if (this.producerBaseDeployed && (this.producerBaseDeployed.length == 0 || this.marketDeployed.length == 0)) {
      const delay = new Promise(resolve => setTimeout(resolve, 1000));
      await delay;
      return await this.loadInitData();
    }

    let that = this

    this.localStoreage.getStoreOwners().forEach(async function(storeOwner) {
      for(let i =0; i < that.producerBaseDeployed.length; i++){
        for(let j =0; j < that.marketDeployed.length; j++){
            await that.retrieveAllStoreFronts(storeOwner, i, j);
        }
      }
    });

    const default_account = this.web3Service.getDefaultAccount()

    this.localStoreage.getOffers().forEach((item) => {
      if(item.seller == default_account && item.signed == ''){
        this.myOffers.push(item);
      } else if(item.buyer == default_account && item.signed != ''){
        this.mySuccessfulOffers.push(item);
      }
    })

    console.log('my offers: ')
    console.log('my offers: ' + this.myOffers.length)
  }

  async retrieveAllStoreFronts(storeOwner, producerBaseIndex, marketIndex) {
    try {
      const default_account = this.web3Service.getDefaultAccount()
      
      const transaction = await this.producerBaseDeployed[producerBaseIndex]
        .getStoreFrontsByPageNum(storeOwner, this.pageNum, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded!');

        console.log(transaction)

        this.storeFrontTable = transaction
          .filter((item) => { return item.id != '0' && item.isDiabled != true })
          .map((item) => { 

            let newEntry : StoreFrontRow = { 
              id: item.id, 
              producerIndex: producerBaseIndex, 
              marketIndex: marketIndex,  
              createdAt: this.unixToDate(parseInt(item.createdAt.toString())), 
              owner: storeOwner 
            }

            return newEntry
          })
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  //====================

  async signOrder(inv, formData){

    let seller = this.web3Service.getDefaultAccount();

    let invoice : Invoice = {
      seller: seller, 
      buyer: inv.buyer,
      producerBase: inv.producerBase,
      storeFrontId: inv.storeFrontId,
      productId: inv.productId, 
      amount: inv.amount, 
      pricePerUnit: inv.pricePerUnit, 
      validUntil: this.getDateInUnixTimestamp(formData.value.validUntil),
      nonce: 0,
      signed: ''
    }

    console.log(formData.value.validUntil)

    let nonce = await this.web3Service.getNonce(seller)
    invoice.nonce = parseInt(nonce)

    let signedOrder = await this.makeSignature(invoice.seller, invoice.buyer, invoice.productId, 
      invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce)

    invoice.signed = signedOrder

    this.localStoreage.addOffer(invoice)

    this.showNotification('bottom','center', 'success', "Success")

    console.log(invoice)
  }

  async buy(offer, formData){

    let buyer = this.web3Service.getDefaultAccount();


    let invoice = {
      seller: offer.seller, 
      buyer: buyer,
      producerBase: offer.producerBase,
      storeFrontId: offer.storeFrontId,
      productId: offer.productId, 
      amount: offer.amount, 
      pricePerUnit: offer.pricePerUnit, 
      validUntil: parseInt(offer.validUntil)
    }

    console.log('nonce ' + offer.nonce)
    console.log('signature ' + offer.signature)

    // let nonce = formData.value.nonce;
    // let signature = formData.value.signature;
    let amountPayed = formData.value.amountPayed;

    console.log(invoice)

      try {
        let  transaction = await this.marketDeployed[0].buyProduct(invoice, offer.nonce, offer.signed, { from: buyer, value: amountPayed });
  
        if (!transaction) {
          this.setStatus('Transaction failed!');
        } else {
          this.setStatus('Transaction successful!');
          
          this.showNotification('bottom','center', 'success', "Product has been bought.")
          // console.log(transactionLogs)
          
          // transactionLogs.forEach(log => {
          //   if(log.event == "LogProductAddedToStoreFront"){
          //     this.showNotification('bottom','center', 'success', "New product with Id: " + log.args.productId.toString() + " has been added to the store successfuly.")
          //   }
          // });
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error; see log');
      }
  }

  async viewProducts(storeFrontId, owner, producerIndex){
    try{

      const default_account = this.web3Service.getDefaultAccount()

      const transaction = await this.producerBaseDeployed[producerIndex].getProductsByPageNum(owner, storeFrontId, this.productPageNum, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded!');

        this.productTable = transaction
          .filter((item) => { return item.id != '0' })
          .map((item) => { 

            let newEntry : ProductRow = { 
              id: parseInt(item.id), 
              producerIndex: producerIndex, 
              owner: owner,
              specificationId: parseInt(item.specificationId), 
              amount:parseInt(item.amount), 
              pricePerUnit: parseInt(item.pricePerUnit), 
              hasNegotiablePrice: item.hasNegotiablePrice == 'true' 
            }

            return newEntry
          })
      
        this.chosenStoreFront = storeFrontId
        this.chosenOwner = owner;
        this.chosenProducer = this.producerBaseDeployed[producerIndex].address;
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  makeOfferEnable(productId){
    this.makeOffer = true;
    this.chosenProductId = productId;
  }

  saveOffer(formData){
    let pricePerUnit = parseInt(formData.value.pricePerUnit);
    let amount = parseInt(formData.value.amount);

    console.log(amount)
    
    const default_account = this.web3Service.getDefaultAccount()

    let newInvoice: Invoice = {   
      seller: this.chosenOwner, 
      buyer: default_account,
      producerBase: this.chosenProducer,
      storeFrontId: this.chosenStoreFront,
      productId: this.chosenProductId,
      amount: amount, 
      pricePerUnit: pricePerUnit, 
      validUntil: 0,
      nonce: 0,
      signed: ''
    }

    this.localStoreage.addOffer(newInvoice);

    let inv = this.localStoreage.getOffers();

    this.showNotification('bottom','center', 'success', "Offer saved.")
  }

  // resetEditForm(){
    
  //   this.editBtnMode = false;
  //   if(this.edittedProduct) {
  //     this.edittedProduct = {  
  //       id: 0,
  //       specificationId: 0,
  //       editedAt: '',
  //       amount: 0,
  //       pricePerUnit: 0,
  //       hasNegotiablePrice: false }
  //   }
  // }

  // showEditForm(prindex){
  //   this.editBtnMode = true
  //   this.chosenProductIndex = prindex
    
  //   let prData = this.productTable[prindex]
  //   this.edittedProduct = {  
  //     id: prData.id,
  //     specificationId: prData.specificationId,
  //     editedAt: '',
  //     amount: prData.amount,
  //     pricePerUnit: prData.pricePerUnit,
  //     hasNegotiablePrice: prData.hasNegotiablePrice }
  // }

  // async addProduct(formData) {

  //   const memberBase = this.producerBaseDeployed[0]
  //   let storeFronId = this.chosenStoreFront
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
  //       let shown = false
        
  //       transactionLogs.forEach(log => {
  //         if(log.event == "LogProductAddedToStoreFront" && !shown){
  //           this.showNotification('bottom','center', 'success', "New product with Id " + log.args.productId.toString() + " has been added to the store successfuly.")
  //           shown = true
  //         }
  //       });

  //       this.addProductInfo.storeFrontId = 0
  //       this.viewProducts(memberBase, storeFronId)
  //     }
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error; see log');
  //   }
  // }

  // async editProduct(formData) {

  //   const memberBase = this.producerBaseDeployed[0]
  //   let storeFronId = this.chosenStoreFront
  //   let productId = this.edittedProduct.id
  //   let specificationId = formData.value.specificationId
  //   let pricePerUnit = parseInt(formData.value.pricePerUnit)
  //   let amount = parseInt(formData.value.amount)
  //   let hasNegotiablePrice = formData.value.hasNegotiablePrice == 'true'

  //   formData.reset();

  //   try {
  //     const default_account = this.web3Service.getDefaultAccount()

  //     let  transaction = await memberBase.updateProduct(storeFronId, productId, pricePerUnit, amount, hasNegotiablePrice, { from: default_account });

  //     if (!transaction) {
  //       this.setStatus('Transaction failed!');
  //     } else {

  //       this.addProductInfo.storeFrontId = 0
  //       this.viewProducts(memberBase, storeFronId)
  //     }
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error; see log');
  //   }
  // }

  // async publishStoreFront(storeFrontId, formData){
  //   let marketAddress = formData.value.marketAddress

  //   formData.reset();

  //   try {
  //     const default_account = this.web3Service.getDefaultAccount()

  //     let  transaction = await this.producerBaseDeployed[0].publishStoreFrontToMarket(marketAddress, storeFrontId, { from: default_account });

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

  private async makeSignature(seller, buyer, productId, amount, pricePerUnit, validUntil, nonce) {
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

  private getDateInUnixTimestamp = (date) => Math.floor((new Date(date)).getTime()/ 1000);

  private onlyUnique(value, index, self) { 
    return self.indexOf(value) === index;
  }

  private unixToDate(unix_timestamp) {
    // Create a new JavaScript Date object based on the timestamp
    // multiplied by 1000 so that the argument is in milliseconds, not seconds.
    const date = new Date(unix_timestamp*1000);
    // Hours part from the timestamp
    const hours = date.getHours();
    // Minutes part from the timestamp
    const minutes = "0" + date.getMinutes();
    // Seconds part from the timestamp
    const seconds = "0" + date.getSeconds();

    // Will display time in 10:30:23 format
    const formattedTime = hours + ':' + minutes.substr(-2) + ':' + seconds.substr(-2);

    const mm = date.getMonth() + 1; // getMonth() is zero-based
    const dd = date.getDate();
  
    return mm + '/' + dd + '/' + date.getFullYear() + '  ' + formattedTime
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
