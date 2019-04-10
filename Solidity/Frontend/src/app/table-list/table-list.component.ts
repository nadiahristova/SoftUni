import { Component, OnInit } from '@angular/core';
import { MatSnackBar  } from '@angular/material';
import { Router } from "@angular/router"

import { Web3Service } from '../service-proxies/web3.service';
import { LocalStorageService, MembershipInfo } from '../service-proxies/local-storage.service';

const abi = require('ethereumjs-abi')

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
          let transactionLogs = transaction.logs 
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
}
