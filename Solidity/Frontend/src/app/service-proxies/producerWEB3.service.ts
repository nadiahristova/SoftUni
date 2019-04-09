import { Injectable, OnInit } from '@angular/core';
// import { Observable } from 'rxjs/Rx';
// import { from, of } from 'rxjs'
// import { map, filter, scan, tap, take, catchError } from 'rxjs/operators';

// import * as web3 from 'web3'
// import * as TruffleContract from 'truffle-contract';

declare let require: any;
declare let window: any;

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')

@Injectable({
  providedIn: 'root'
})
export class ProducersWEB3Service {

  // private web3Provider: web3.providers.HttpProvider;

  // private producerBase: TruffleContract;

  // private default_account: string = null; 

  // constructor() { 
  //   if (window.ethereum) {
  //     this.web3Provider = window.ethereum;

  //     try {
  //       // Request account access
  //       new Promise((resolve, reject) => {
  //         window.ethereum.enable();
  //       })
  //     } catch (error) {
  //       // User denied account access...
  //       console.error("User denied account access")
  //     }
  //   } else if (window.web3) {
  //     this.web3Provider = window.web3.currentProvider;
  //   } else {
  //     this.web3Provider = new web3.providers.HttpProvider('http://localhost:7545');
  //   }
    
  //   window.web3 = new web3(this.web3Provider);

  //   this.initContract(this.web3Provider) 
  // }

  // initContract(provider) {

  //   let that = this;

  //   let paymentContract = TruffleContract(producerBaseABI);
  //       paymentContract.setProvider(provider);

  //       paymentContract.deployed().then((instance) => { that.producerBase = instance })

  //       paymentContract.deployed().then(function(instance) {
  //         var events = instance.allEvents(function(error, log){
  //           if (!error)
  //             $("#eventsList").prepend('<li>' + log.event + '</li>'); // Using JQuery, we will add new events to a list in our index.html
  //         });
  //         }).catch(function(err) {
  //           console.log(err.message);
  //         });
  //   // return new Promise((resolve, reject) => {
  //   //     let paymentContract = TruffleContract(producerBaseABI);
  //   //     paymentContract.setProvider(that.web3Provider);

  //   //     that.contracts.ProducerBase = paymentContract;
  //   // });
  // }

  // getAccountInfo() {

  //   let that = this;

  //   return new Promise((resolve, reject) => {
  //     window.web3.eth.getCoinbase(function(err, account) {
  //       if(err === null) {
  //         window.web3.eth.getBalance(account, function(err, balance) {

  //           if(err === null) {
  //             return resolve({fromAccount: account, balance: web3.utils.fromWei(balance, 'ether')});
  //           } else {
  //             return reject('error!');
  //           }
  //         });
  //       }
  //     });
  //   });
  // }

  // transferEther(
  //   ) {
  //   let that = this;

  //   return new Promise((resolve, reject) => {
  //     let paymentContract = TruffleContract(producerBaseABI);
  //     paymentContract.setProvider(that.web3Provider);

  //     paymentContract.deployed().then(function(instance) {
  //       console.log(instance.getMarketPartners.call())
  //     return instance.getMarketPartners.call();
  //   }).catch(function(error){
  //   console.log(error);
  //     return reject('Error in transferEther service call');
  //   });
  //   });
  //   }

  //   // transferEther(
  //   //   _transferFrom,
  //   //   _transferTo,
  //   //   _amount,
  //   //   _remarks
  //   //   ) {
  //   //   let that = this;
  //   //   return new Promise((resolve, reject) => {
  //   //   this.contracts.ProducerBase.deployed().then(function(instance) {
  //   //   return instance.transferFund(
  //   //   _transferTo,
  //   //   {
  //   //   from:_transferFrom,
  //   //   value:web3.toWei(_amount, 'ether')
  //   //   });
  //   //   }).then(function(status) {
  //   //   if(status) {
  //   //   return resolve({status:true});
  //   //   }
  //   //   }).catch(function(error){
  //   //   console.log(error);
  //   //   return reject('Error in transferEther service call');
  //   //   });
  //   //   });
  //   //   }
  //   //   }
}