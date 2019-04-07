import { Component, OnInit } from '@angular/core';
import { MatSnackBar  } from '@angular/material';

import * as Chartist from 'chartist';

import { Web3Service } from '../service-proxies/web3.service';
import { ProducersWEB3Service } from '../service-proxies/producerWEB3.service';

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')


declare let require: any;


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  status = '';

  ProducerBase: any;
  Market: any;
  
  accounts: string[];

  model = {
    amount: 5,
    receiver: '',
    balance: 0,
    account: ''
  };


  constructor(private web3Service: Web3Service, private matSnackBar: MatSnackBar) { 
    console.log('Constructor: ' + web3Service);
  }
  
  ngOnInit(): void {
    console.log('OnInit: ' + this.web3Service);
    console.log(this);

    this.watchAccount();
    this.web3Service.artifactsToContract(producerBaseABI)
      .then((ProducerBaseAbstraction) => {
        this.ProducerBase = ProducerBaseAbstraction;

        this.ProducerBase.deployed().then(deployed => {
          console.log(deployed);
          
          const transaction = deployed.getMarketPartners.call();

          if (!transaction) {
            this.setStatus('Transaction failed!');
          } else {
            this.setStatus('Transaction complete!');
          }
          // deployed.getMarketPartners.sendTransaction({from: this.model.account}, (err, ev) => {
          //   console.log('Transfer event came in, refreshing balance');
          //   console.log('Accounts' + this.model.account)
          //   //this.refreshBalance();
          // });
        });

      });
  }

  watchAccount() {
    this.web3Service.accountsObservable.subscribe((accounts) => {
      this.accounts = accounts;
      this.model.account = accounts[0];
      //this.refreshBalance();
    });
  }

  setStatus(status) {
    this.matSnackBar.open(status, null, {duration: 3000});
  }

  async sendCoin() {
    if (!this.ProducerBase) {
      this.setStatus('Metacoin is not loaded, unable to send transaction');
      return;
    }

    const amount = this.model.amount;
    const receiver = this.model.receiver;

    console.log('Sending coins' + amount + ' to ' + receiver);

    this.setStatus('Initiating transaction... (please wait)');
    try {
      const deployedProducerBase = await this.ProducerBase.deployed();
      const transaction = await deployedProducerBase.sendCoin.sendTransaction(receiver, amount, {from: this.model.account});

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction complete!');
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error sending coin; see log.');
    }
  }

  async myClickFunction(event) { 
    //alert(await this.producerWEB3.getMarketPartners());
    await this.test()
  }

  async test() {
    if (!this.ProducerBase) {
      this.setStatus('ProducerBase is not loaded, unable to send transaction');
      return;
    }

    const amount = this.model.amount;
    const receiver = this.model.receiver;

    console.log('Sending coins' + amount + ' to ' + receiver);

    this.setStatus('Initiating transaction... (please wait)');
    try {
      const deployedProducerBase = await this.ProducerBase.deployed();
      const transaction = await deployedProducerBase.getMarketPartners.call({from: this.model.account});

      if (!transaction) {
        this.setStatus('Transaction failed!');
      } else {
        this.setStatus('Transaction complete!');
      }
    } catch (e) {
      console.log(e);
      this.setStatus('Error getting markets see log.');
    }
  }

  // async refreshBalance() {
  //   console.log('Refreshing balance');

  //   try {
  //     const deployedProducerBase = await this.ProducerBase.deployed();
  //     console.log(deployedProducerBase);
  //     console.log('Account', this.model.account);
  //     const metaCoinBalance = await deployedProducerBase.getBalance.call(this.model.account);
  //     console.log('Found balance: ' + metaCoinBalance);
  //     this.model.balance = metaCoinBalance;
  //   } catch (e) {
  //     console.log(e);
  //     this.setStatus('Error getting balance; see log.');
  //   }
  // }

  setAmount(e) {
    console.log('Setting amount: ' + e.target.value);
    this.model.amount = e.target.value;
  }

  setReceiver(e) {
    console.log('Setting receiver: ' + e.target.value);
    this.model.receiver = e.target.value;
  }

  
  // startAnimationForLineChart(chart){
  //     let seq: any, delays: any, durations: any;
  //     seq = 0;
  //     delays = 80;
  //     durations = 500;

  //     chart.on('draw', function(data) {
  //       if(data.type === 'line' || data.type === 'area') {
  //         data.element.animate({
  //           d: {
  //             begin: 600,
  //             dur: 700,
  //             from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
  //             to: data.path.clone().stringify(),
  //             easing: Chartist.Svg.Easing.easeOutQuint
  //           }
  //         });
  //       } else if(data.type === 'point') {
  //             seq++;
  //             data.element.animate({
  //               opacity: {
  //                 begin: seq * delays,
  //                 dur: durations,
  //                 from: 0,
  //                 to: 1,
  //                 easing: 'ease'
  //               }
  //             });
  //         }
  //     });

  //     seq = 0;
  // };
  // startAnimationForBarChart(chart){
  //     let seq2: any, delays2: any, durations2: any;

  //     seq2 = 0;
  //     delays2 = 80;
  //     durations2 = 500;
  //     chart.on('draw', function(data) {
  //       if(data.type === 'bar'){
  //           seq2++;
  //           data.element.animate({
  //             opacity: {
  //               begin: seq2 * delays2,
  //               dur: durations2,
  //               from: 0,
  //               to: 1,
  //               easing: 'ease'
  //             }
  //           });
  //       }
  //     });

  //     seq2 = 0;
  // };

  // ngOnInit() {
  //     /* ----------==========     Daily Sales Chart initialization For Documentation    ==========---------- */

  //     const dataDailySalesChart: any = {
  //         labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
  //         series: [
  //             [12, 17, 7, 17, 23, 18, 38]
  //         ]
  //     };

  //    const optionsDailySalesChart: any = {
  //         lineSmooth: Chartist.Interpolation.cardinal({
  //             tension: 0
  //         }),
  //         low: 0,
  //         high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
  //         chartPadding: { top: 0, right: 0, bottom: 0, left: 0},
  //     }

  //     var dailySalesChart = new Chartist.Line('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

  //     this.startAnimationForLineChart(dailySalesChart);


  //     /* ----------==========     Completed Tasks Chart initialization    ==========---------- */

  //     const dataCompletedTasksChart: any = {
  //         labels: ['12p', '3p', '6p', '9p', '12p', '3a', '6a', '9a'],
  //         series: [
  //             [230, 750, 450, 300, 280, 240, 200, 190]
  //         ]
  //     };

  //    const optionsCompletedTasksChart: any = {
  //         lineSmooth: Chartist.Interpolation.cardinal({
  //             tension: 0
  //         }),
  //         low: 0,
  //         high: 1000, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
  //         chartPadding: { top: 0, right: 0, bottom: 0, left: 0}
  //     }

  //     var completedTasksChart = new Chartist.Line('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);

  //     // start animation for the Completed Tasks Chart - Line Chart
  //     this.startAnimationForLineChart(completedTasksChart);



  //     /* ----------==========     Emails Subscription Chart initialization    ==========---------- */

  //     var datawebsiteViewsChart = {
  //       labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
  //       series: [
  //         [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]

  //       ]
  //     };
  //     var optionswebsiteViewsChart = {
  //         axisX: {
  //             showGrid: false
  //         },
  //         low: 0,
  //         high: 1000,
  //         chartPadding: { top: 0, right: 5, bottom: 0, left: 0}
  //     };
  //     var responsiveOptions: any[] = [
  //       ['screen and (max-width: 640px)', {
  //         seriesBarDistance: 5,
  //         axisX: {
  //           labelInterpolationFnc: function (value) {
  //             return value[0];
  //           }
  //         }
  //       }]
  //     ];
  //     var websiteViewsChart = new Chartist.Bar('#websiteViewsChart', datawebsiteViewsChart, optionswebsiteViewsChart, responsiveOptions);

  //     //start animation for the Emails Subscription Chart
  //     this.startAnimationForBarChart(websiteViewsChart);
  // }

}
