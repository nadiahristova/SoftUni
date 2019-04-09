import { Component, OnInit } from '@angular/core';
import { MatSnackBar  } from '@angular/material';
import { Router } from "@angular/router"

import * as Chartist from 'chartist';
import TruffleContract from 'truffle-contract';

import { Web3Service } from '../service-proxies/web3.service';
import { LocalStorageService } from '../service-proxies/local-storage.service';
import { ProducersWEB3Service } from '../service-proxies/producerWEB3.service';

import { MemberBaseInfo } from '../service-proxies/local-storage.service';

let producerBaseABI = require('../../../../MarketChain/build/contracts/ProducerBase.json')
let regionalMarketABI = require('../../../../MarketChain/build/contracts/RegionalMarket.json')


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

  membershipData: { [key: string] : MemberBaseInfo } = { }

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

    console.log('Constructor: ' + web3Service);
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

    this.localStoreage.setLoggedInEntityForCurrentUser(undefined)

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
    
    this.applyForMembership(this.producerBaseDeployed, index)
  }

  async applyForMembershipM(index) {
    
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
        } else 
        /// options.... states
        {
          this.setStatus('Transaction succeeded!');
        }
      } catch (e) {
        console.log(e);
        this.setStatus('Error revoking membership; see log. Are you perhaps the owner of the organisation?');
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
