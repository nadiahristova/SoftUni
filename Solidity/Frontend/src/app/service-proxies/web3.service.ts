import { Injectable } from '@angular/core';
import TruffleContract from 'truffle-contract';
import {Subject} from 'rxjs';

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')

declare let require: any;
declare let window: any;

const Web3 = require('web3');

@Injectable({
    providedIn: 'root'
  })
export class Web3Service {

  private web3: any;

  private accounts: string[];
  private currBalance: any;

  public ready = false;

  // public MarketAbstractions: any[] = [];
  // public ClientBaseAbstraction: any[] = [];
  // public ProducerBaseAbstractions: any[] = []; 

  public accountsObservable = new Subject<string[]>();

  constructor() {
    window.addEventListener('load', async (event) =>  {
      this.bootstrapWeb3();
    });
  }

  public bootstrapWeb3() {
    if (window.ethereum) {
      this.web3 = new Web3(window.ethereum);

      try {
        // Request account access
        new Promise((resolve, reject) => {
          window.ethereum.enable();
        })
        
      } catch (error) {
        // User denied account access...
        console.error("User denied account access")
      }
    // Checking if Web3 has been injected by the browser (Mist/MetaMask)
    } else  if (typeof window.web3 !== 'undefined') {
      // Use Mist/MetaMask's provider
      console.log('Use Mist/MetaMask\'s provider')
      this.web3 = new Web3(window.web3.currentProvider);
    } else {
      console.log('No web3? You should consider trying MetaMask!');

      // Hack to provide backwards compatibility for Truffle, which uses web3js 0.20.x
      Web3.providers.HttpProvider.prototype.sendAsync = Web3.providers.HttpProvider.prototype.send;
      // fallback - use your fallback strategy (local node / hosted node + in-dapp id mgmt / fail)
      this.web3 = new Web3(new Web3.providers.HttpProvider('http://localhost:7545'));
    }

    setInterval(() => this.refreshAccounts(), 500);
  }

  // async LoadArtifacts() {
  //   const market_ini = await this.artifactsToContract(regionalMarketABI);
  //   const market = await market_ini.deployed()
  //   this.MarketAbstractions.push(market)

  //   const producerBase_ini = await this.artifactsToContract(producerBaseABI);
  //   const producerBase = await producerBase_ini.deployed()
  //   this.ProducerBaseAbstractions.push(producerBase)

  //   console.log('Artefacts loaded')
  // }

  public getAccounts() {
    return this.accounts;
  }

  public getDefaultAccount() {
    return this.accounts[0];
  }

  public getDefaultAccountBalance() {

    return this.currBalance;
  }

  public async artifactsToContract(artifacts) {
    if (!this.web3) {
      const delay = new Promise(resolve => setTimeout(resolve, 500));
      await delay;
      return await this.artifactsToContract(artifacts);
    }

    const contractAbstraction = TruffleContract(artifacts);
    contractAbstraction.setProvider(this.web3.currentProvider);
    return contractAbstraction;
  }

  private refreshAccounts() {
    this.web3.eth.getAccounts((err, accs) => {
      console.log('Refreshing accounts');
      if (err != null) {
        console.warn('There was an error fetching your accounts.');
        return;
      }

      // Get the initial account balance so it can be displayed.
      if (accs.length === 0) {
        console.warn('Couldn\'t get any accounts! Make sure your Ethereum client is configured correctly.');
        return;
      }

      this.web3.eth.getBalance(accs[0], (err, balance) => {
          this.currBalance = balance;
      });

      if (!this.accounts || this.accounts.length !== accs.length || this.accounts[0] !== accs[0]) {
        console.log('Observed new accounts');

        this.accountsObservable.next(accs);
        this.accounts = accs;

        this.web3.eth.defaultAccount = accs[0]
      }

      this.ready = true;
    });
  }

  changeCurrency(balance, current, desired) {
    if(current == 'wei') {
      return this.web3.utils.fromWei(balance, desired)
    } else if(current == 'ether') {
      return this.web3.utils.toWei(balance)
    }
  }

}
