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
}

interface StoreFront{
  id: Number; 
  createdAt: string;
  isDisabled: boolean;
}

interface ProductRow {
  id: Number; 
  specificationId: Number;
  editedAt: string;
  amount: Number;
  pricePerUnit: Number;
  hasNegotiablePrice: boolean;
}



@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})
export class TableListComponent implements OnInit {
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

  storeFronts: StoreFront[]
  productTable: ProductRow[]
  chosenStoreFront: Number;
  chosenProductIndex: Number;

  editBtnMode: boolean = false;
  edittedProduct: ProductRow

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

  // invoice = {
  //   seller: '', 
  //   buyer: '',
  //   producerBase: '',
  //   storeFrontId: 3,
  //   productId: 2, 
  //   amount: 5, 
  //   pricePerUnit: 100, 
  //   validUntil: this.getDateInUnixTimestamp('08/08/2019')
  // }

  constructor(
    private web3Service: Web3Service, 
    private matSnackBar: MatSnackBar, 
    private localStoreage: LocalStorageService, 
    private router: Router) { }

  ngOnInit() {

    let that = this

    window.ethereum.on('accountsChanged', async function (accounts) {
      that.router.navigate(['/home'])
      that.setStatus('Navigating to Home Page.');
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
    if (this.producerBaseDeployed.length == 0) {
      const delay = new Promise(resolve => setTimeout(resolve, 1000));
      await delay;
      return await this.loadInitData();
    }
    
    let that = this

    this.producerBaseDeployed.forEach(async function (pb) {
      await that.retrieveAllStoreFronts(pb);
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    location.reload()
  }

  //====================

  async signOrder(formData){

    let seller = this.web3Service.getDefaultAccount();

    let invoice : Invoice = {
      seller: seller, 
      buyer: formData.value.buyer,
      producerBase: formData.value.producerBase,
      storeFrontId: formData.value.storeFrontId,
      productId: formData.value.productId, 
      amount: formData.value.amount, 
      pricePerUnit: formData.value.pricePerUnit, 
      validUntil: this.getDateInUnixTimestamp(formData.value.validUntil)
    }

    // signedOrder
    let nonce = await this.web3Service.getNonce(seller)
    this.nonce = parseInt(nonce)

    console.log(invoice)

    let signedOrder = await this.makeSignature(invoice.seller, invoice.buyer, invoice.productId, 
      invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce)

    this.signedOrder = signedOrder
  }

  async sendOrder(formData){

    let buyer = this.web3Service.getDefaultAccount();

    let invoice : Invoice = {
      seller: formData.value.seller, 
      buyer: buyer,
      producerBase: formData.value.producerBase,
      storeFrontId: formData.value.storeFrontId,
      productId: formData.value.productId, 
      amount: formData.value.amount, 
      pricePerUnit: formData.value.pricePerUnit, 
      validUntil: this.getDateInUnixTimestamp(formData.value.validUntil)
    }

    let nonce = formData.value.nonce;
    let signature = formData.value.signature;
    let amountPayed = formData.value.amountPayed;

      try {
        let  transaction = await this.marketDeployed[0].buyProduct(invoice, nonce, signature, { from: buyer, value: amountPayed });
  
        if (!transaction) {
          this.setStatus('Transaction failed!');
        } else {
          this.setStatus('Transaction successful!');
          //let transactionLogs = transaction.logs 
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
 
  async openStoreFront(memberBase) {
    try {
      const default_account = this.web3Service.getDefaultAccount()

      const transaction = await memberBase.addStoreFront({ from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded!');
        let transactionLogs = transaction.logs 

        transactionLogs.forEach(log => {
          if(log.event == "LogStoreFrontCreated"){
            this.showNotification('bottom','center', 'success', "New store Front with Id " + log.args.storeFrontId.toString() + " has been added to the store successfuly.")
          }
        });

        await this.retrieveAllStoreFronts(memberBase)

        //location.reload();
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  async retrieveAllStoreFronts(memberBase) {
    try {
      const default_account = this.web3Service.getDefaultAccount()
      
      const transaction = await memberBase.getStoreFrontsByPageNum(default_account, this.pageNum, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded!');

        this.storeFronts = transaction
          .filter((item) => { return item.id != '0' })
          .map((item) => { 

          let newEntry : StoreFront = { id: 0, createdAt: '', isDisabled: false }

          newEntry.id = parseInt(item.id)

          newEntry.createdAt = this.unixToDate(parseInt(item.createdAt.toString()))
          newEntry.isDisabled = item.isDisabled == 'true'

          return newEntry
        })
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  //TODO: Fix this
  async enDisStoreFront(memberBase, storeFrontId, isDiabled) {
    
    try {
      const default_account = this.web3Service.getDefaultAccount()

      let transaction;

      if(isDiabled == true) {
        transaction = await memberBase.enableStoreFront(storeFrontId, { from: default_account });
      } else {
        transaction = await memberBase.disableStoreFront(storeFrontId, { from: default_account });
      }

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        if(isDiabled == true) {
          this.setStatus('Transaction succeeded! Store Front enabled');
        } else {
          this.setStatus('Transaction succeeded! Store Front disabled');
        }

        await this.retrieveAllStoreFronts(memberBase)
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  async viewProducts(memberBase, storeFrontId){
    try{
      this.resetEditForm()

      const default_account = this.web3Service.getDefaultAccount()

      this.chosenStoreFront = storeFrontId

      const transaction = await memberBase.getProductsByPageNum(default_account, storeFrontId, this.productPageNum, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction succeeded!');

        console.log(transaction)
        this.productTable = transaction
          .filter((item) => { return item.id != '0' })
          .map((item) => { 

          let newEntry : ProductRow = { id: 0, editedAt: '', specificationId: 0, amount:0, pricePerUnit:0, hasNegotiablePrice: false }

          newEntry.id = parseInt(item.id)
          newEntry.specificationId = parseInt(item.specificationId)
          newEntry.amount = parseInt(item.amount)
          newEntry.pricePerUnit = parseInt(item.pricePerUnit)
          newEntry.editedAt = this.unixToDate(parseInt(item.editedAt.toString()))
          newEntry.hasNegotiablePrice = item.hasNegotiablePrice == 'true'

          return newEntry
        })

        console.log(this.productTable)
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  resetEditForm(){
    
    this.editBtnMode = false;
    if(this.edittedProduct) {
      this.edittedProduct = {  
        id: 0,
        specificationId: 0,
        editedAt: '',
        amount: 0,
        pricePerUnit: 0,
        hasNegotiablePrice: false }
    }
  }

  showEditForm(prindex){
    this.editBtnMode = true
    this.chosenProductIndex = prindex
    
    let prData = this.productTable[prindex]
    this.edittedProduct = {  
      id: prData.id,
      specificationId: prData.specificationId,
      editedAt: '',
      amount: prData.amount,
      pricePerUnit: prData.pricePerUnit,
      hasNegotiablePrice: prData.hasNegotiablePrice }
  }

  async addProduct(formData) {

    const memberBase = this.producerBaseDeployed[0]
    let storeFronId = this.chosenStoreFront
    let specificationId = formData.value.specificationId
    let pricePerUnit = parseInt(formData.value.pricePerUnit)
    let amount = parseInt(formData.value.amount)
    let hasNegotiablePrice = formData.value.hasNegotiablePrice == 'true'

    formData.reset();

    try {
      const default_account = this.web3Service.getDefaultAccount()

      let  transaction = await memberBase.addProductToStoreFront(storeFronId, specificationId, pricePerUnit, amount, hasNegotiablePrice, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        let transactionLogs = transaction.logs 
        console.log(transactionLogs)
        let shown = false
        
        transactionLogs.forEach(log => {
          if(log.event == "LogProductAddedToStoreFront" && !shown){
            this.showNotification('bottom','center', 'success', "New product with Id " + log.args.productId.toString() + " has been added to the store successfuly.")
            shown = true
          }
        });

        this.addProductInfo.storeFrontId = 0
        this.viewProducts(memberBase, storeFronId)
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  async editProduct(formData) {

    const memberBase = this.producerBaseDeployed[0]
    let storeFronId = this.chosenStoreFront
    let productId = this.edittedProduct.id
    let specificationId = formData.value.specificationId
    let pricePerUnit = parseInt(formData.value.pricePerUnit)
    let amount = parseInt(formData.value.amount)
    let hasNegotiablePrice = formData.value.hasNegotiablePrice == 'true'

    formData.reset();

    try {
      const default_account = this.web3Service.getDefaultAccount()

      let  transaction = await memberBase.updateProduct(storeFronId, productId, pricePerUnit, amount, hasNegotiablePrice, { from: default_account });

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {

        this.addProductInfo.storeFrontId = 0
        this.viewProducts(memberBase, storeFronId)
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error; see log');
    }
  }

  async publishStoreFront(storeFrontId, formData){
    let marketAddress = formData.value.marketAddress

    formData.reset();

    try {
      const default_account = this.web3Service.getDefaultAccount()

      let  transaction = await this.producerBaseDeployed[0].publishStoreFrontToMarket(marketAddress, storeFrontId, { from: default_account });

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
