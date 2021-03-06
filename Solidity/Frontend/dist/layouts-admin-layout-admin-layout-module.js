(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["layouts-admin-layout-admin-layout-module"],{

/***/ "./src/app/community/community.component.html":
/*!****************************************************!*\
  !*** ./src/app/community/community.component.html ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\n<div class=\"main-content\">\n  <div class=\"container-fluid\">\n    <span>Currently logged in: <h4 class=\"badge badge-info\">{{currentlyLogedIn}}</h4></span>\n    <hr/>\n    <div class=\"row\">\n      <div class=\"col-lg-6 col-md-12\">\n            <!-- <button (click) = \"test()\">Click me</button>\n            <button (click) = \"signOrderAcc()\">signOrderAcc me</button>\n            <button (click) = \"getProduct()\">get product</button> -->\n          <div class=\"card\">\n              <div class=\"card-header card-header-warning\">\n                  <h4 class=\"card-title\">Guilds</h4>\n                  <p class=\"card-category\">Available guilds:</p>\n              </div>\n              <div *ngIf=\"producerBaseDeployed.length > 0\" class=\"card-body table-responsive\">\n                  <div *ngFor=\"let pb of producerBaseDeployed; let i = index\" class=\"col-lg-12 col-md-12 col-sm-12\">\n\n                    <span class=\"text-warning\">{{pb.address}}</span> \n                    <div class=\"card card-stats\">\n                      <div class=\"card-header card-header-warning card-header-icon\">\n                          <div class=\"card-icon\" matTooltip=\"{{pb.address}}\" [matTooltipPosition]=\"'below'\">\n                              <i class=\"fa fa-university\"></i>\n                          </div>\n                          <div>\n                              <p class=\"card-category\">Membership Applications</p>\n                              <h4 *ngIf=\"pendReqForMembership\" class=\"card-title\" >\n                                <div *ngFor=\"let addr of pendReqForMembership; let i = index\" class=\"container-fluid\">\n                                    <a matTooltip=\"{{addr}}\" (click)=\"launchMembershipGrantingCampaign(pb, addr, false)\" [matTooltipPosition]=\"'below'\" class=\"badge badge-info\">Applicant {{i+1}}</a>\n                                </div>\n                              </h4>\n                          </div>\n                          <div *ngIf=\"approvedmembers && approvedmembers.length > 0\" >\n                              <p class=\"card-category\">Register Members:</p>\n                              <h4 class=\"card-title\">\n                                <div *ngFor=\"let addr of approvedmembers; let i = index\" class=\"container-fluid\">\n                                    <a matTooltip=\"{{addr}}\" (click)=\"approveMembershipRequest(pb, addr)\" [matTooltipPosition]=\"'below'\" class=\"badge badge-info\">Applicant {{i+1}}</a>\n                                </div>\n                              </h4>\n                          </div> \n                          <div>\n                        <!-- <div class=\"form-group\">\n                            <button mat-raised-button type=\"button\" matTooltip=\"Open Store Front\" [matTooltipPosition]=\"'below'\" \n                            class ='btn btn-warning btn-link btn-lg btn-just-icon' (click)=\"openStoreFront(pb)\">\n                                <i class=\"material-icons\">fiber_new</i>\n                            </button>\n                        </div> -->\n                        <div>\n                            <form #addPartnerForm = \"ngForm\" (ngSubmit) = \"addPartner(pb, addPartnerForm)\">\n                                <div class=\"form-group\">\n                                    <label for=\"tr-member-input\">Add Partner Market:</label> \n                                    <input type=\"text\" class=\"form-control\" name=\"marketAddress\" id=\"tr-member-input\" placeholder=\"Market address\" ngModel required length=\"42\">\n                                </div>\n                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Add Partner Market\" [matTooltipPosition]=\"'below'\" \n                                    class=\"btn btn-danger btn-link btn-just-icon\">\n                                    <i class=\"material-icons\">fiber_new</i>\n                                </button>\n                            </form>\n                          </div>\n                        <div>\n                            <form #reqMembershipForm = \"ngForm\" (ngSubmit) = \"requestMembership(pb, reqMembershipForm)\">\n                                <div class=\"form-group\">\n                                    <label for=\"tr-member-input\">Request Market membership:</label> \n                                    <input type=\"text\" class=\"form-control\" name=\"marketAddress\" id=\"tr-member-input\" placeholder=\"Market address\" ngModel required length=\"42\">\n                                </div>\n                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Request Market membership\" [matTooltipPosition]=\"'below'\" \n                                    class=\"btn btn-danger btn-link btn-just-icon\">\n                                    <i class=\"material-icons\">swap_vert</i>\n                                </button>\n                            </form>\n                        </div>\n                        <!-- <form #enDisStoreFrontForm = \"ngForm\" (ngSubmit) = \"enDisStoreFront(pb, enDisStoreFrontForm)\">\n                            <div class=\"form-group\">\n                                <label>Enable/Disable Store Front:</label> \n                                <br/>\n                                <input type=\"radio\" id='e' value=true name=\"enable\" checked=\"checked\"  ngModel><label for=\"e\">Enable</label><br/>\n                                <input type=\"radio\" id='d' value=false name=\"enable\" ngModel> <label for=\"d\">Disable</label>\n                                <input type=\"text\" class=\"form-control\" name=\"storeFrontId\" id=\"tr-member-input\" placeholder=\"Store Front Id\" ngModel required length=\"42\">\n                            </div>\n                            <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Enable/Disable Store Front\" [matTooltipPosition]=\"'below'\" \n                                class=\"btn btn-warning btn-link btn-just-icon\">\n                                <i class=\"material-icons\">done</i>\n                            </button>\n                        </form> -->\n                        <hr/>\n                        <!-- <form #publishStoreFrontForm = \"ngForm\" (ngSubmit) = \"publishStoreFront(pb, publishStoreFrontForm)\">\n                                <div class=\"form-group\">\n                                    <label>Publish Store Front to Market:</label> \n                                    <input type=\"text\" class=\"form-control\" name=\"storeFrontId\" placeholder=\"Store Front Id\" ngModel required>\n                                    <input type=\"text\" class=\"form-control\" name=\"marketAddress\" placeholder=\"Market Address\" ngModel required length=\"42\">\n                                </div>\n                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Publish Store Front\" [matTooltipPosition]=\"'below'\" \n                                    class=\"btn btn-warning btn-link btn-just-icon\">\n                                    <i class=\"material-icons\">publish</i>\n                                </button>\n                            </form> -->\n                          </div>\n                          <!-- <form #addProductForm = \"ngForm\" (ngSubmit) = \"addProduct(pb, addProductForm)\">\n                                <div class=\"form-group\">\n                                    <label>Add Product to Store Front:</label> \n                                    <input type=\"text\" class=\"form-control\" name=\"storeFrontId\" placeholder=\"Store Front Id\" ngModel required>\n                                    <input type=\"text\" class=\"form-control\" name=\"specificationId\" placeholder=\"Specification Id\" ngModel required>\n                                    <input type=\"text\" class=\"form-control\" name=\"pricePerUnit\" placeholder=\"Price Per Unit\" ngModel required>\n                                    <input type=\"text\" class=\"form-control\" name=\"amount\" placeholder=\"Amount\" ngModel required>\n                                    \n                                    <input type=\"checkbox\" class=\"form-control-lg\" id=\"pr_has_NegotiablePrice\" name=\"hasNegotiablePrice\" ngModel required>\n                                    <label for=\"pr_has_NegotiablePrice\">Has negotiable price</label>\n                                </div>\n                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Add Product\" [matTooltipPosition]=\"'below'\" \n                                    class=\"btn btn-warning btn-link btn-just-icon\">\n                                    <i class=\"material-icons\">fiber_new</i>\n                                </button>\n                          </form> -->\n                      </div>\n                      <div class=\"card-footer\">\n                          <div class=\"stats\">\n                              <i class=\"material-icons\">update</i> Just Updated\n                          </div>\n                          <p class=\"card-category\">\n                            <br/>\n                              My reputation: <span class=\"badge badge-info\">{{guildReputation[i]}}</span>\n                          </p>\n                      </div>\n                    </div>\n\n\n                  </div>\n              </div>\n        </div>\n      </div>\n\n\n\n      \n      <div class=\"col-lg-6 col-md-12\">\n          <div class=\"card\">\n              <div class=\"card-header card-header-danger\">\n                  <h4 class=\"card-title\">Markets</h4>\n                  <p class=\"card-category\">Available markets</p>\n              </div>\n              <div *ngIf=\"marketDeployed.length > 0\" class=\"card-body table-responsive\">\n                  <div *ngFor=\"let market of marketDeployed; let i = index\" class=\"col-lg-12 col-md-12 col-sm-12\">\n                    \n                    <span class=\"text-danger\">{{market.address}}</span> \n                    <div class=\"card card-stats\">\n                      <div class=\"card-header card-header-danger card-header-icon\">\n                          <div class=\"card-icon\" matTooltip=\"{{market.address}}\" [matTooltipPosition]=\"'below'\">\n                              <i class=\"fa fa-shopping-cart\"></i>\n                          </div>\n                          <div>\n                              <p class=\"card-category\">Membership Applications</p>\n                              <h4 *ngIf=\"pendReqForMembershipMK\" class=\"card-title\" >\n                                <div *ngFor=\"let addr of pendReqForMembershipMK; let i = index\" class=\"container-fluid\">\n                                    <a matTooltip=\"{{addr}}\" (click)=\"launchMembershipGrantingCampaign(market, addr, true)\" [matTooltipPosition]=\"'below'\" class=\"badge badge-info\">Applicant {{i+1}}</a>\n                                </div>\n                              </h4>\n                          </div>\n\n                          <div class=\"card-body table-responsive\">\n                              <form #affiliateForm = \"ngForm\" (ngSubmit) = \"affiliate(market, affiliateForm)\">\n                                  <div class=\"form-group\">\n                                      <label for=\"tr-member-input\">Affiliate with guild:</label> \n                                      <input type=\"text\" class=\"form-control\" name=\"accAddress\" id=\"tr-member-input\" placeholder=\"Guild address\" ngModel required length=\"42\">\n                                  </div>\n                                  <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Affiliate\" [matTooltipPosition]=\"'below'\" \n                                      class=\"btn btn-danger btn-link btn-just-icon\">\n                                      <i class=\"material-icons\">thumbs_up_down</i>\n                                  </button>\n                              </form>\n                          </div>\n                          <div>\n                            <form #openStoreForm = \"ngForm\" (ngSubmit) = \"openStore(market, openStoreForm)\">\n                                <div class=\"form-group\">\n                                    <label for=\"tr-member-input\">Open store:</label> \n                                    <input type=\"text\" class=\"form-control\" name=\"storeName\" placeholder=\"Store name\" ngModel required >\n                                    <input type=\"text\" class=\"form-control\" name=\"guildAddress\" placeholder=\"Guild address\" ngModel required length=\"42\">\n                                </div>\n                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Open store\" [matTooltipPosition]=\"'below'\" \n                                    class=\"btn btn-danger btn-link btn-just-icon\">\n                                    <i class=\"material-icons\">thumb_up</i>\n                                </button>\n                            </form>\n                          </div>\n                          \n                      </div>\n                      <div class=\"card-footer\">\n                          <div class=\"stats\">\n                              <i class=\"material-icons\">update</i> Just Updated\n                          </div>\n                          <div class=\"card-category\">\n                                <p>\n                                  My reputation: <span class=\"badge badge-info\">{{marketReputation[i]}}</span>\n                                </p>\n                                <p >Sales: {{profits[i]}}\n                                    <button mat-raised-button type=\"button\" matTooltip=\"Retrieve Weis\" [matTooltipPosition]=\"'below'\" \n                                    class ='btn btn-danger btn-link btn-sm btn-just-icon' (click)=\"getProfit(market)\">\n                                        <i class=\"material-icons\">monetization_on</i>\n                                    </button>\n                                </p>\n                          </div>\n                      </div>\n                    </div>\n                  </div>\n              </div>\n          </div>\n      </div>\n    </div>\n  </div>\n</div>"

/***/ }),

/***/ "./src/app/community/community.component.scss":
/*!****************************************************!*\
  !*** ./src/app/community/community.component.scss ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/community/community.component.ts":
/*!**************************************************!*\
  !*** ./src/app/community/community.component.ts ***!
  \**************************************************/
/*! exports provided: CommunityComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CommunityComponent", function() { return CommunityComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _service_proxies_web3_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../service-proxies/web3.service */ "./src/app/service-proxies/web3.service.ts");
/* harmony import */ var _service_proxies_local_storage_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../service-proxies/local-storage.service */ "./src/app/service-proxies/local-storage.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (undefined && undefined.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};





var producerBaseABI = __webpack_require__(/*! ../../../../MarketChain/build/contracts/ProducerBase.json */ "../MarketChain/build/contracts/ProducerBase.json");
var regionalMarketABI = __webpack_require__(/*! ../../../../MarketChain/build/contracts/RegionalMarket.json */ "../MarketChain/build/contracts/RegionalMarket.json");
var abi = __webpack_require__(/*! ethereumjs-abi */ "./node_modules/ethereumjs-abi/index.js");
var Web3 = __webpack_require__(/*! web3 */ "./node_modules/web3/src/index.js");
var CommunityComponent = /** @class */ (function () {
    function CommunityComponent(web3Service, matSnackBar, localStoreage, router) {
        this.web3Service = web3Service;
        this.matSnackBar = matSnackBar;
        this.localStoreage = localStoreage;
        this.router = router;
        this.status = '';
        this.marketDeployed = [];
        this.clientBaseDeployed = [];
        this.producerBaseDeployed = [];
        this.profits = [];
        this.marketReputation = [];
        this.guildReputation = [];
        this.pendReqForMembership = [];
        this.pendReqForMembershipMK = [];
        this.approvedmembers = [];
        this.getDateInUnixTimestamp = function (date) { return Math.floor((new Date(date)).getTime() / 1000); };
    }
    CommunityComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.checkForMembership();
        var that = this;
        window.ethereum.on('accountsChanged', function (accounts) {
            return __awaiter(this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    that.router.navigate(['/home']);
                    that.setStatus('Navigating to Home Page.');
                    return [2 /*return*/];
                });
            });
        });
        this.web3Service.artifactsToContract(producerBaseABI)
            .then(function (ProducerBaseAbstraction) {
            ProducerBaseAbstraction.deployed().then(function (inst) {
                _this.producerBaseDeployed.push(inst);
                var currAccount = _this.web3Service.getDefaultAccount();
                inst.getVoteWeight(currAccount, { from: currAccount }).then(function (voteWeight) { return _this.guildReputation.push(voteWeight); });
            });
        });
        this.web3Service.artifactsToContract(regionalMarketABI)
            .then(function (MarketAbstraction) {
            MarketAbstraction.deployed().then(function (inst) {
                _this.marketDeployed.push(inst);
                var currAccount = _this.web3Service.getDefaultAccount();
                inst.getAccumolatedProfit({ from: currAccount }).then(function (profit) { return _this.profits.push(profit); });
                inst.getVoteWeight(currAccount, { from: currAccount }).then(function (voteWeight) { return _this.marketReputation.push(voteWeight); });
            });
        });
        this.gatherPendingRequests();
    };
    CommunityComponent.prototype.checkForMembership = function () {
        var currentInstitution = this.localStoreage.getLoggedInEntityForCurrentUser();
        var userRights = this.localStoreage.getCurrentUserMembershipInfoForMEmberBase(currentInstitution);
        // if(currentInstitution == undefined || !userRights.is_member) {
        //   this.router.navigate(['/home'])
        //   this.localStoreage.clearStorage()
        //   this.setStatus('Navigating to Home Page.');
        // }
    };
    CommunityComponent.prototype.setStatus = function (status) {
        this.matSnackBar.open(status, null, { duration: 3000 });
    };
    CommunityComponent.prototype.gatherPendingRequests = function () {
        this.pendReqForMembership = this.localStoreage.getPendingMembershipRequests();
        this.pendReqForMembershipMK = this.localStoreage.getPendingMembershipRequestsMarket();
        this.currentlyLogedIn = this.localStoreage.getLoggedInEntityForCurrentUser();
    };
    //====================
    CommunityComponent.prototype.launchMembershipGrantingCampaign = function (memberBase, accAddress, isMarket) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, transactionLogs, e_1;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 5, , 6]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.launchMembershipGrantingCampaign(accAddress, { from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        console.log(transaction);
                        if (!!transaction) return [3 /*break*/, 2];
                        this.setStatus('Transaction failed! Cannot support member.');
                        return [3 /*break*/, 4];
                    case 2:
                        this.setStatus('Transaction succeeded!');
                        return [4 /*yield*/, memberBase.supportMember(accAddress, 1, { from: default_account })];
                    case 3:
                        transaction = _a.sent();
                        console.log(transaction);
                        console.log(transaction.logs);
                        transactionLogs = transaction.logs;
                        transactionLogs.forEach(function (log) {
                            if (log.event == "PropositionAccepted") {
                                if (isMarket) {
                                    _this.removeElementFromArray(_this.pendReqForMembershipMK, accAddress);
                                    _this.localStoreage.removePendingMembershipRequestsMArket(accAddress);
                                }
                                else {
                                    _this.removeElementFromArray(_this.pendReqForMembership, accAddress);
                                    _this.localStoreage.removePendingMembershipRequests(accAddress);
                                    _this.approvedmembers.push(accAddress);
                                }
                            }
                        });
                        _a.label = 4;
                    case 4: return [3 /*break*/, 6];
                    case 5:
                        e_1 = _a.sent();
                        console.log(e_1);
                        this.setStatus('Error support member; see log.');
                        return [3 /*break*/, 6];
                    case 6: return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.approveMembershipRequest = function (memberBase, accAddress) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_2;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.registerMember(accAddress, { from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        console.log(transaction);
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot approve membership.');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                            this.removeElementFromArray(this.approvedmembers, accAddress);
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        e_2 = _a.sent();
                        console.log(e_2);
                        this.setStatus('Error approve membership; see log. Did you already applied for membership?');
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.getProfit = function (memberBase) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_3;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.retrieveProfit({ from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed! Funds are retrieved.');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        e_3 = _a.sent();
                        console.log(e_3);
                        this.setStatus('Error retrieving funds; see log');
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.affiliate = function (market_base, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var accAddreaa, default_account, transaction, e_4;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        accAddreaa = formData.value.accAddress;
                        formData.reset();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        console.log(default_account);
                        return [4 /*yield*/, market_base.affiliateProducerBase(accAddreaa, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot affiliate with guild.');
                        }
                        else {
                            this.setStatus('Success');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_4 = _a.sent();
                        console.log(e_4);
                        this.setStatus('Error; see log.');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.openStore = function (market, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var guildAddress, storeName, default_account, transaction, e_5;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        guildAddress = formData.value.guildAddress;
                        storeName = formData.value.storeName;
                        console.log(storeName);
                        storeName = Web3.utils.fromAscii(storeName, storeName.length);
                        formData.reset();
                        console.log(storeName);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, market.openStore(guildAddress, storeName, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_5 = _a.sent();
                        console.log(e_5);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
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
    CommunityComponent.prototype.addPartner = function (memberBase, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var marketAddress, default_account, transaction, e_6;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        marketAddress = formData.value.marketAddress;
                        formData.reset();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.addMarketPartner(marketAddress, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded! Store Front published');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_6 = _a.sent();
                        console.log(e_6);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.requestMembership = function (memberBase, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var marketAddress, default_account, transaction, e_7;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        marketAddress = formData.value.marketAddress;
                        formData.reset();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.requestMarketMembership(marketAddress, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded! Store Front published');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_7 = _a.sent();
                        console.log(e_7);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.getProduct = function () {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_8;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, this.producerBaseDeployed[0].getProduct(2, { from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded! Store Front published');
                            console.log(transaction);
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        e_8 = _a.sent();
                        console.log(e_8);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        });
    };
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
    CommunityComponent.prototype.signOrder = function (seller, buyer, productId, amount, pricePerUnit, validUntil, nonce) {
        return __awaiter(this, void 0, void 0, function () {
            var hash;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        hash = "0x" + abi.soliditySHA3(["address", "address", "uint256", "uint256", "uint256", "uint256", "uint256"], [seller, buyer, productId, amount, pricePerUnit, validUntil, nonce]).toString("hex");
                        hash = "0x" + abi.soliditySHA3(["string", "bytes32"], ["\x19Ethereum Signed Message:\n32", hash]).toString("hex");
                        return [4 /*yield*/, this.web3Service.signHash(hash, seller)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    CommunityComponent.prototype.signOrderAcc = function () {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, buyer, invoice, nonce, signedOrder;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        default_account = this.web3Service.getDefaultAccount();
                        buyer = '0x4762352F433415B87C4909d971037dDFC6e772E1';
                        invoice = {
                            seller: '0xb9131879580245c560c1b884E91840FB5336D52a',
                            buyer: '0x4762352F433415B87C4909d971037dDFC6e772E1',
                            producerBase: this.producerBaseDeployed[0].address,
                            storeFrontId: 3,
                            productId: 2,
                            amount: 5,
                            pricePerUnit: 100,
                            validUntil: this.getDateInUnixTimestamp('08/08/2019')
                        };
                        return [4 /*yield*/, this.web3Service.getNonce(default_account)];
                    case 1:
                        nonce = _a.sent();
                        console.log('nonce: ' + nonce);
                        return [4 /*yield*/, this.signOrder(invoice.seller, invoice.buyer, invoice.productId, invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce)];
                    case 2:
                        signedOrder = _a.sent();
                        console.log('signedOrder: ' + signedOrder);
                        return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.test = function () {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, buyer, invoice, nonce, signedOrder, transaction, transactionLogs;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        default_account = '0xb9131879580245c560c1b884E91840FB5336D52a';
                        buyer = this.web3Service.getDefaultAccount();
                        invoice = {
                            seller: '0xb9131879580245c560c1b884E91840FB5336D52a',
                            buyer: '0x4762352F433415B87C4909d971037dDFC6e772E1',
                            producerBase: this.producerBaseDeployed[0].address,
                            storeFrontId: 3,
                            productId: 2,
                            amount: 5,
                            pricePerUnit: 100,
                            validUntil: this.getDateInUnixTimestamp('08/08/2019')
                        };
                        nonce = 26;
                        signedOrder = '0xaca56ccfdc25cfda4ae1f5fc4bedd124340681b7ab083f5da1bdc0b9d3d2f0b97bc63ac451dee4d155c2f29d7ec343ef481c97b12a7511bda076af84f7e541741b';
                        return [4 /*yield*/, this.marketDeployed[0].buyProduct(invoice, nonce, signedOrder, { from: buyer, value: 100000 })];
                    case 1:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction successful!');
                            transactionLogs = transaction.logs;
                            // console.log(transactionLogs)
                            // transactionLogs.forEach(log => {
                            //   if(log.event == "LogProductAddedToStoreFront"){
                            //     this.showNotification('bottom','center', 'success', "New product with Id: " + log.args.productId.toString() + " has been added to the store successfuly.")
                            //   }
                            // });
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    CommunityComponent.prototype.isCurrOwner = function (marketAddr) {
        if (this.currentlyLogedIn != marketAddr) {
            return false;
        }
        return this.localStoreage.getCurrentUserMembershipInfoForMEmberBase(marketAddr).is_owner;
    };
    CommunityComponent.prototype.removeElementFromArray = function (arr, el) {
        var index = arr.indexOf(el, 0);
        if (index > -1) {
            arr.splice(index, 1);
        }
    };
    CommunityComponent.prototype.showNotification = function (from, align, color, msg) {
        //const type = ['','info','success','warning','danger'];
        $.notify({
            icon: "notifications",
            message: msg
        }, {
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
    };
    CommunityComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-community',
            template: __webpack_require__(/*! ./community.component.html */ "./src/app/community/community.component.html"),
            styles: [__webpack_require__(/*! ./community.component.scss */ "./src/app/community/community.component.scss")]
        }),
        __metadata("design:paramtypes", [_service_proxies_web3_service__WEBPACK_IMPORTED_MODULE_3__["Web3Service"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatSnackBar"],
            _service_proxies_local_storage_service__WEBPACK_IMPORTED_MODULE_4__["LocalStorageService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])
    ], CommunityComponent);
    return CommunityComponent;
}());



/***/ }),

/***/ "./src/app/home/home.component.html":
/*!******************************************!*\
  !*** ./src/app/home/home.component.html ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main-content\">\n  <div class=\"container-fluid\">\n    <div class=\"row\">\n        <div class=\"col-lg-12 col-md-12\">\n            <div class=\"card\">\n                <div class=\"card-header card-header-tabs card-header-primary\">\n                    <div class=\"nav-tabs-navigation\">\n                        <div class=\"nav-tabs-wrapper\">\n                            <!-- <span class=\"nav-tabs-title\">Member Bases:</span> -->\n                            <ul class=\"nav nav-tabs \" data-tabs=\"tabs\">\n                                <li class=\"nav-item\">\n                                    <a mat-button class=\"nav-link active\" href=\"#guilds\" data-toggle=\"tab\">\n                                        <i class=\"material-icons\">account_balance</i> Guilds\n                                        <div class=\"ripple-container\"></div>\n                                    </a>\n                                </li>\n                                <li class=\"nav-item disabled\">\n                                    <a mat-button class=\"nav-link disabled\" href=\"#clients\"  data-toggle=\"tab\">\n                                        <i class=\"material-icons\">accessibility_new</i> Client Base\n                                        <div class=\"ripple-container\"></div>\n                                    </a>\n                                </li>\n                                <li class=\"nav-item\">\n                                    <a mat-button class=\"nav-link\" href=\"#markets\" data-toggle=\"tab\">\n                                        <i class=\"material-icons\">shopping_cart</i> Available Markets\n                                        <div class=\"ripple-container\"></div>\n                                    </a>\n                                </li>\n                            </ul>\n                        </div>\n                    </div>\n                </div>\n                <div class=\"card-body\">\n                    <div class=\"tab-content\">\n                        <div *ngIf=\"producerBaseDeployed.length > 0\" class=\"tab-pane active\" id=\"guilds\">\n                            <table class=\"table\">\n                                <tbody >\n                                    <tr *ngFor=\"let pb of producerBaseDeployed; let i = index\">\n                                        <td *ngIf=\"membershipData[pb.address]\">\n                                            <a mat-button [className]=\"membershipData[pb.address] ? 'nav-link' : 'nav-link disabled'\" (click)=\"producerBaseLoginClickEvent($event, i)\">\n                                                {{pb.address}}\n                                                <div class=\"ripple-container\"></div>\n                                            </a>\n                                        </td>\n                                        <td>\n\n                                        </td>\n                                        <td class=\"td-actions text-right\" *ngIf=\"membershipData[pb.address]\">\n                                                <button mat-raised-button type=\"button\" matTooltip=\"Appy for Membership\" [matTooltipPosition]=\"'below'\" \n                                                    [className]=\"membershipData[pb.address].is_member ? 'd-none' : 'btn btn-primary btn-link btn-sm btn-just-icon'\"\n                                                    (click)=\"applyForMembershipPB(i)\">\n                                                    <i class=\"material-icons\">{{membershipData[pb.address].is_pending ? 'alarm_on' : 'send'}}</i>\n                                                </button>\n                                                <button mat-raised-button type=\"button\" matTooltip=\"Revoke Membership\" [matTooltipPosition]=\"'below'\" \n                                                    [className]=\"membershipData[pb.address].is_member && !membershipData[pb.address].is_owner? 'btn btn-primary btn-link btn-sm btn-just-icon' : 'd-none'\" \n                                                    (click)=\"revokeMembershipPB($event, i)\">\n                                                    <i class=\"material-icons\">close</i>\n                                                </button>\n                                                <!-- transfer ownership -->\n                                                <form #transfOwnershipForm = \"ngForm\" (ngSubmit) = \"transferOwnershipPB(i, transfOwnershipForm)\" [className]=\"!membershipData[pb.address].is_owner && 'd-none'\">\n                                                    <div class=\"form-group\">\n                                                        <label for=\"tr-member-input\">Transfere Ownership:</label> \n                                                        <input type=\"text\" class=\"form-control\" name=\"accAddress\" id=\"tr-member-input\" placeholder=\"Account address\" ngModel required length=\"42\">\n                                                    </div>\n                                                    <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Transfer Ownership\" [matTooltipPosition]=\"'below'\" \n                                                        class=\"btn btn-primary btn-link btn-just-icon\">\n                                                        <i class=\"material-icons\">compare_arrows</i>\n                                                    </button>\n                                                </form>\n                                        </td>\n                                    </tr>\n                                </tbody>\n                            </table>\n                        </div>\n\n                        <div class=\"tab-pane \" id=\"clients\">\n                        </div>\n\n                        <div *ngIf=\"marketDeployed.length > 0\" class=\"tab-pane\" id=\"markets\">\n                            <table class=\"table\">\n                                <tbody>\n                                    <tr *ngFor=\"let market of marketDeployed; let i = index\">\n                                        <td *ngIf=\"membershipData[market.address]\">\n                                            <a mat-button [className]=\"membershipData[market.address] ? 'nav-link' : 'nav-link disabled'\" (click)=\"marketLoginClickEvent($event, i)\">\n                                                {{market.address}}\n                                                <div class=\"ripple-container\"></div>\n                                            </a>\n                                        </td>\n                                        <td>\n\n                                        </td>\n                                        <td *ngIf=\"membershipData[market.address] && !membershipData[market.address].is_member\">\n                                                <form #registerForm = \"ngForm\" (ngSubmit) = \"register(i, registerForm)\">\n                                                    <div class=\"form-group\">\n                                                        <label for=\"tr-member-input\">Register:</label> \n                                                        <input type=\"text\" class=\"form-control\" name=\"isoCode\" placeholder=\"ISO Code\" ngModel required>\n                                                        <input type=\"text\" class=\"form-control\" name=\"province\" placeholder=\"Province\" ngModel required>\n                                                    </div>\n                                                    <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Register\" [matTooltipPosition]=\"'below'\" \n                                                        class=\"btn btn-primary btn-link btn-just-icon\">\n                                                        <i class=\"material-icons\">play_arrow</i>\n                                                    </button>\n                                                </form>\n                                        </td>\n                                        <td *ngIf=\"membershipData[market.address]\">\n                                            <form #donateForm = \"ngForm\" (ngSubmit) = \"donate(i, donateForm)\">\n                                                <div class=\"form-group\">\n                                                    <label for=\"tr-member-input\">Make a donation:</label> \n                                                    <input type=\"text\" class=\"form-control\" name=\"donation\" id=\"tr-member-input-m\" placeholder=\"Ether\" ngModel required>\n                                                    <input type=\"text\" class=\"form-control\" name=\"isoCode\" placeholder=\"ISO Code\" ngModel required>\n                                                    <input type=\"text\" class=\"form-control\" name=\"province\" placeholder=\"Province\" ngModel required>\n                                                </div>\n                                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Make a donation\" [matTooltipPosition]=\"'below'\" \n                                                    class=\"btn btn-primary btn-link btn-just-icon\">\n                                                    <i class=\"material-icons\">redo</i>\n                                                </button>\n                                            </form>\n                                        </td>\n                                        <td  *ngIf=\"membershipData[market.address]\" class=\"td-actions text-right\">\n                                            <button mat-raised-button type=\"button\" matTooltip=\"Appy for Membership\" [matTooltipPosition]=\"'below'\" \n                                                [className]=\"membershipData[market.address].is_member ? 'd-none' : 'btn btn-primary btn-link btn-sm btn-just-icon'\"\n                                                (click)=\"applyForMembershipM(i)\">\n                                                <i class=\"material-icons\">{{membershipData[market.address].is_pending ? 'alarm_on' : 'send'}}</i>\n                                            </button>\n                                            <button mat-raised-button type=\"button\" matTooltip=\"Revoke Membership\" [matTooltipPosition]=\"'below'\" \n                                                [className]=\"membershipData[market.address].is_member && !membershipData[market.address].is_owner? 'btn btn-primary btn-link btn-sm btn-just-icon' : 'd-none'\" \n                                                (click)=\"revokeMembershipM($event, i)\">\n                                                <i class=\"material-icons\">close</i>\n                                            </button>\n                                            <!-- register\n                                            <form *ngIf=\"!membershipData[market.address].is_member\"  #transfOwnershipMarket = \"ngForm\" (ngSubmit) = \"transferOwnershipPB(i, transfOwnershipMarket)\" [className]=\"!membershipData[market.address].is_owner && 'd-none'\">\n                                                    <div class=\"form-group\">\n                                                        <label for=\"tr-member-input\">Register:</label> \n                                                        <input type=\"text\" class=\"form-control\" name=\"accAddress\" id=\"tr-member-input-m\" placeholder=\"Account address\" ngModel required length=\"42\">\n                                                    </div>\n                                                    <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Transfer Ownership\" [matTooltipPosition]=\"'below'\" \n                                                        class=\"btn btn-primary btn-link btn-just-icon\">\n                                                        <i class=\"material-icons\">compare_arrows</i>\n                                                    </button>\n                                            </form> -->\n                                            <!-- transfer ownership -->\n                                            <form #transfOwnershipMarket = \"ngForm\" (ngSubmit) = \"transferOwnershipPB(i, transfOwnershipMarket)\" [className]=\"!membershipData[market.address].is_owner && 'd-none'\">\n                                                <div class=\"form-group\">\n                                                    <label for=\"tr-member-input\">Transfere Ownership:</label> \n                                                    <input type=\"text\" class=\"form-control\" name=\"accAddress\" id=\"tr-member-input-m\" placeholder=\"Account address\" ngModel required length=\"42\">\n                                                </div>\n                                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Transfer Ownership\" [matTooltipPosition]=\"'below'\" \n                                                    class=\"btn btn-primary btn-link btn-just-icon\">\n                                                    <i class=\"material-icons\">compare_arrows</i>\n                                                </button>\n                                            </form>\n                                        </td>\n                                    </tr>\n                                </tbody>\n                            </table>\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div> \n  </div>\n</div>\n"

/***/ }),

/***/ "./src/app/home/home.component.scss":
/*!******************************************!*\
  !*** ./src/app/home/home.component.scss ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/home/home.component.ts":
/*!****************************************!*\
  !*** ./src/app/home/home.component.ts ***!
  \****************************************/
/*! exports provided: HomeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HomeComponent", function() { return HomeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _service_proxies_web3_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../service-proxies/web3.service */ "./src/app/service-proxies/web3.service.ts");
/* harmony import */ var _service_proxies_local_storage_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../service-proxies/local-storage.service */ "./src/app/service-proxies/local-storage.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (undefined && undefined.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};





var producerBaseABI = __webpack_require__(/*! ../../../../MarketChain/build/contracts/ProducerBase.json */ "../MarketChain/build/contracts/ProducerBase.json");
var regionalMarketABI = __webpack_require__(/*! ../../../../MarketChain/build/contracts/RegionalMarket.json */ "../MarketChain/build/contracts/RegionalMarket.json");
var Web3 = __webpack_require__(/*! web3 */ "./node_modules/web3/src/index.js");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(web3Service, matSnackBar, localStoreage, router) {
        this.web3Service = web3Service;
        this.matSnackBar = matSnackBar;
        this.localStoreage = localStoreage;
        this.router = router;
        this.status = '';
        this.marketDeployed = [];
        this.clientBaseDeployed = [];
        this.producerBaseDeployed = [];
        this.membershipData = {};
        this.model = {
            amount: 5,
            receiver: '',
            balance: 0,
            account: ''
        };
    }
    HomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        console.log('OnInit');
        this.watchAccount();
        this.web3Service.artifactsToContract(producerBaseABI)
            .then(function (ProducerBaseAbstraction) {
            ProducerBaseAbstraction.deployed().then(function (inst) {
                _this.producerBaseDeployed.push(inst);
            });
        });
        this.web3Service.artifactsToContract(regionalMarketABI)
            .then(function (MarketAbstraction) {
            MarketAbstraction.deployed().then(function (inst) { return _this.marketDeployed.push(inst); });
        });
        var that = this;
        window.ethereum.on('accountsChanged', function (accounts) {
            return __awaiter(this, void 0, void 0, function () {
                var delay;
                return __generator(this, function (_a) {
                    switch (_a.label) {
                        case 0:
                            delay = new Promise(function (resolve) { return setTimeout(resolve, 1500); });
                            return [4 /*yield*/, delay];
                        case 1:
                            _a.sent();
                            return [4 /*yield*/, that.SettleMemberships()];
                        case 2:
                            _a.sent();
                            return [2 /*return*/];
                    }
                });
            });
        });
        new Promise(function (resolve) { return _this.SettleMemberships(); });
    };
    HomeComponent.prototype.SettleMemberships = function () {
        return __awaiter(this, void 0, void 0, function () {
            var delay, that;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(this.producerBaseDeployed.length == 0)) return [3 /*break*/, 3];
                        delay = new Promise(function (resolve) { return setTimeout(resolve, 1000); });
                        return [4 /*yield*/, delay];
                    case 1:
                        _a.sent();
                        return [4 /*yield*/, this.SettleMemberships()];
                    case 2: return [2 /*return*/, _a.sent()];
                    case 3:
                        that = this;
                        this.producerBaseDeployed.forEach(function (abs) {
                            return __awaiter(this, void 0, void 0, function () {
                                return __generator(this, function (_a) {
                                    switch (_a.label) {
                                        case 0: return [4 /*yield*/, that.refreshMembership(abs)];
                                        case 1:
                                            _a.sent();
                                            return [2 /*return*/];
                                    }
                                });
                            });
                        });
                        this.marketDeployed.forEach(function (abs) {
                            return __awaiter(this, void 0, void 0, function () {
                                return __generator(this, function (_a) {
                                    switch (_a.label) {
                                        case 0: return [4 /*yield*/, that.refreshMembership(abs)];
                                        case 1:
                                            _a.sent();
                                            return [2 /*return*/];
                                    }
                                });
                            });
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.refreshMembership = function (memberBase) {
        return __awaiter(this, void 0, void 0, function () {
            var account, transaction, isMember, isAdmin, isOwner, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        account = this.web3Service.getDefaultAccount();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        return [4 /*yield*/, memberBase.getMembershipInfo.call(account)];
                    case 2:
                        transaction = _a.sent();
                        isMember = transaction["isMember"];
                        isAdmin = false;
                        isOwner = transaction["isOwner"];
                        this.membershipData[memberBase.address] = { is_member: isMember, is_owner: isOwner, is_admin: isAdmin, is_pending: false };
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot validate membership');
                        }
                        else if (isMember) {
                            this.localStoreage.addOrUpdateCurrentUserMembershipInfo(memberBase.address, isMember, isAdmin, isOwner, false);
                        }
                        else {
                            this.setStatus('Membership information gathered.');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_1 = _a.sent();
                        console.log(e_1);
                        this.setStatus('Error getting membership; see log.');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.applyForMembershipPB = function (index) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.localStoreage.addPendingMembershipRequests(this.web3Service.getDefaultAccount());
                this.applyForMembership(this.producerBaseDeployed, index);
                return [2 /*return*/];
            });
        });
    };
    HomeComponent.prototype.applyForMembershipM = function (index) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.localStoreage.addPendingMembershipRequestsMarket(this.web3Service.getDefaultAccount());
                this.applyForMembership(this.marketDeployed, index);
                return [2 /*return*/];
            });
        });
    };
    HomeComponent.prototype.applyForMembership = function (memberBases, index) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_2;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(index >= 0 && index < memberBases.length)) return [3 /*break*/, 4];
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBases[index].requestMembership({ from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        console.log(transaction);
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot request membership.');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                            this.membershipData[memberBases[index].address].is_pending = true;
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_2 = _a.sent();
                        console.log(e_2);
                        this.setStatus('Error requesting membership; see log. Did you already applied for membership?');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.revokeMembershipPB = function (event, index) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.revokeMembership(this.producerBaseDeployed, index);
                return [2 /*return*/];
            });
        });
    };
    HomeComponent.prototype.revokeMembershipM = function (event, index) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.revokeMembership(this.marketDeployed, index);
                return [2 /*return*/];
            });
        });
    };
    HomeComponent.prototype.revokeMembership = function (memberBases, index) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, transactionLogs, e_3;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(index >= 0 && index < memberBases.length)) return [3 /*break*/, 4];
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBases[index].revokeMembership({ from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot revoke membership.');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                            transactionLogs = transaction.logs;
                            transactionLogs.forEach(function (log) {
                                if (log.event == "LogMemberRequestingMembershipCancelation") {
                                    _this.showNotification('bottom', 'center', 'warning', 'Request for Leaving sent.');
                                }
                                else if (log.event == "LogMemberLeaving") {
                                    _this.showNotification('bottom', 'center', 'success', 'Your request has been accepted.');
                                    _this.membershipData[memberBases[index].address].is_member = false;
                                    _this.membershipData[memberBases[index].address].is_admin = false;
                                    _this.membershipData[memberBases[index].address].is_pending = false;
                                }
                                else if (log.event == "LogMemberReinstatement") {
                                    _this.showNotification('bottom', 'center', 'info', 'Time for decision had passed. The request is Reset.');
                                }
                                else
                                    throw Error;
                            });
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_3 = _a.sent();
                        console.log(e_3);
                        this.setStatus('Error revoking membership; see log. Are you perhaps the owner of the organisation or maybe the contemplation period did not pass?');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.transferOwnershipPB = function (index, formData) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.transferOwnership(this.producerBaseDeployed, index, formData.value.accAddress);
                formData.reset();
                return [2 /*return*/];
            });
        });
    };
    HomeComponent.prototype.transferOwnershipM = function (event, index, newOwner) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                this.transferOwnership(this.marketDeployed, index, newOwner);
                return [2 /*return*/];
            });
        });
    };
    HomeComponent.prototype.transferOwnership = function (memberBases, index, newOwner) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_4;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(index >= 0 && index < memberBases.length)) return [3 /*break*/, 4];
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBases[index].transferOwnership(newOwner, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot transfere ownership.');
                        }
                        else {
                            this.setStatus('Ownership transferred!');
                            this.membershipData[memberBases[index].address].is_owner = false;
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_4 = _a.sent();
                        console.log(e_4);
                        this.setStatus('Error on ownership transfer; see log.');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.producerBaseLoginClickEvent = function (event, index) {
        this.login(this.producerBaseDeployed, index);
    };
    HomeComponent.prototype.marketLoginClickEvent = function (event, index) {
        this.login(this.marketDeployed, index);
    };
    HomeComponent.prototype.login = function (member_bases, index) {
        if (index >= 0 && index < member_bases.length) {
            var entityAddress = member_bases[index].address;
            if (this.membershipData[entityAddress].is_member) {
                this.localStoreage.setLoggedInEntityForCurrentUser(entityAddress);
                this.router.navigate(['/community']);
                this.setStatus('Logging in...');
            }
            return;
        }
    };
    HomeComponent.prototype.donate = function (index, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var donation, isoCode, province, donationInWei, currentUserBalance, default_account, transaction, e_5;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(index >= 0 && index < this.marketDeployed.length)) return [3 /*break*/, 4];
                        donation = formData.value.donation;
                        isoCode = formData.value.isoCode;
                        province = formData.value.province;
                        formData.reset();
                        if (isoCode.length != 2 || province.length > 30) {
                            this.setStatus('Location data incorrect');
                            return [2 /*return*/];
                        }
                        donationInWei = this.web3Service.changeCurrency(donation, 'ether', 'wei');
                        currentUserBalance = this.web3Service.getDefaultAccountBalance();
                        if (currentUserBalance < donationInWei) {
                            this.setStatus('Donation exceeds user\'s balance');
                            return [2 /*return*/];
                        }
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        isoCode = Web3.utils.fromAscii(isoCode, 2);
                        province = Web3.utils.fromAscii(province, 30);
                        return [4 /*yield*/, this.marketDeployed[index].donateToProvince({ iSOCode: isoCode, province: province }, { from: default_account, value: donationInWei })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot make a donation.');
                        }
                        else {
                            this.setStatus('Donation successful!');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_5 = _a.sent();
                        console.log(e_5);
                        this.setStatus('Error on donation; see log.');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.register = function (index, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var isoCode, province, default_account, transaction, e_6;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(index >= 0 && index < this.marketDeployed.length)) return [3 /*break*/, 4];
                        isoCode = formData.value.isoCode;
                        province = formData.value.province;
                        formData.reset();
                        if (isoCode.length != 2 || province.length > 30) {
                            this.setStatus('Location data incorrect');
                            return [2 /*return*/];
                        }
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        isoCode = Web3.utils.fromAscii(isoCode, 2);
                        province = Web3.utils.fromAscii(province, 30);
                        return [4 /*yield*/, this.marketDeployed[index].registerMember({ iSOCode: isoCode, province: province }, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed! Cannot register account.');
                        }
                        else {
                            this.setStatus('Registration successful!');
                            this.membershipData[this.marketDeployed[index].address].is_member = true;
                            this.localStoreage.addOrUpdateCurrentUserMembershipInfo(this.marketDeployed[index].address, true, false, false, false);
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_6 = _a.sent();
                        console.log(e_6);
                        this.setStatus('Error on registration; see log.');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    HomeComponent.prototype.watchAccount = function () {
        var _this = this;
        this.web3Service.accountsObservable.subscribe(function (accounts) {
            console.log('in');
            _this.accounts = accounts;
            _this.model.account = accounts[0];
        });
    };
    HomeComponent.prototype.setAmount = function (e) {
        console.log('Setting amount: ' + e.target.value);
        this.model.amount = e.target.value;
    };
    HomeComponent.prototype.setReceiver = function (e) {
        console.log('Setting receiver: ' + e.target.value);
        this.model.receiver = e.target.value;
    };
    HomeComponent.prototype.setStatus = function (status) {
        this.matSnackBar.open(status, null, { duration: 3000 });
    };
    HomeComponent.prototype.showNotification = function (from, align, color, msg) {
        //const type = ['','info','success','warning','danger'];
        $.notify({
            icon: "notifications",
            message: msg
        }, {
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
    };
    HomeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-home',
            template: __webpack_require__(/*! ./home.component.html */ "./src/app/home/home.component.html"),
            styles: [__webpack_require__(/*! ./home.component.scss */ "./src/app/home/home.component.scss")]
        }),
        __metadata("design:paramtypes", [_service_proxies_web3_service__WEBPACK_IMPORTED_MODULE_3__["Web3Service"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatSnackBar"],
            _service_proxies_local_storage_service__WEBPACK_IMPORTED_MODULE_4__["LocalStorageService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])
    ], HomeComponent);
    return HomeComponent;
}());



/***/ }),

/***/ "./src/app/layouts/admin-layout/admin-layout.module.ts":
/*!*************************************************************!*\
  !*** ./src/app/layouts/admin-layout/admin-layout.module.ts ***!
  \*************************************************************/
/*! exports provided: AdminLayoutModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AdminLayoutModule", function() { return AdminLayoutModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _admin_layout_routing__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./admin-layout.routing */ "./src/app/layouts/admin-layout/admin-layout.routing.ts");
/* harmony import */ var _user_profile_user_profile_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../user-profile/user-profile.component */ "./src/app/user-profile/user-profile.component.ts");
/* harmony import */ var _table_list_table_list_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../table-list/table-list.component */ "./src/app/table-list/table-list.component.ts");
/* harmony import */ var _home_home_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../home/home.component */ "./src/app/home/home.component.ts");
/* harmony import */ var _community_community_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ../../community/community.component */ "./src/app/community/community.component.ts");
/* harmony import */ var _market_place_market_place_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../../market-place/market-place.component */ "./src/app/market-place/market-place.component.ts");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};













var AdminLayoutModule = /** @class */ (function () {
    function AdminLayoutModule() {
    }
    AdminLayoutModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_2__["CommonModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(_admin_layout_routing__WEBPACK_IMPORTED_MODULE_4__["AdminLayoutRoutes"]),
                _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatButtonModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatRippleModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatDialogModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatSnackBarModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatNativeDateModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatDatepickerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatFormFieldModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatSnackBarModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatDatepickerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatInputModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_10__["MatTooltipModule"]
            ],
            declarations: [
                _home_home_component__WEBPACK_IMPORTED_MODULE_7__["HomeComponent"],
                _user_profile_user_profile_component__WEBPACK_IMPORTED_MODULE_5__["UserProfileComponent"],
                _table_list_table_list_component__WEBPACK_IMPORTED_MODULE_6__["TableListComponent"],
                _community_community_component__WEBPACK_IMPORTED_MODULE_8__["CommunityComponent"],
                _market_place_market_place_component__WEBPACK_IMPORTED_MODULE_9__["MarketPlaceComponent"]
            ]
        })
    ], AdminLayoutModule);
    return AdminLayoutModule;
}());



/***/ }),

/***/ "./src/app/layouts/admin-layout/admin-layout.routing.ts":
/*!**************************************************************!*\
  !*** ./src/app/layouts/admin-layout/admin-layout.routing.ts ***!
  \**************************************************************/
/*! exports provided: AdminLayoutRoutes */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AdminLayoutRoutes", function() { return AdminLayoutRoutes; });
/* harmony import */ var _home_home_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../home/home.component */ "./src/app/home/home.component.ts");
/* harmony import */ var _user_profile_user_profile_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../user-profile/user-profile.component */ "./src/app/user-profile/user-profile.component.ts");
/* harmony import */ var _table_list_table_list_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../table-list/table-list.component */ "./src/app/table-list/table-list.component.ts");
/* harmony import */ var app_community_community_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/community/community.component */ "./src/app/community/community.component.ts");




var AdminLayoutRoutes = [
    // {
    //   path: '',
    //   children: [ {
    //     path: 'dashboard',
    //     component: DashboardComponent
    // }]}, {
    // path: '',
    // children: [ {
    //   path: 'userprofile',
    //   component: UserProfileComponent
    // }]
    // }, {
    //   path: '',
    //   children: [ {
    //     path: 'icons',
    //     component: IconsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'notifications',
    //         component: NotificationsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'maps',
    //         component: MapsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'typography',
    //         component: TypographyComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'upgrade',
    //         component: UpgradeComponent
    //     }]
    // }
    { path: 'home', component: _home_home_component__WEBPACK_IMPORTED_MODULE_0__["HomeComponent"] },
    { path: 'user-profile', component: _user_profile_user_profile_component__WEBPACK_IMPORTED_MODULE_1__["UserProfileComponent"] },
    { path: 'table-list', component: _table_list_table_list_component__WEBPACK_IMPORTED_MODULE_2__["TableListComponent"] },
    { path: 'community', component: app_community_community_component__WEBPACK_IMPORTED_MODULE_3__["CommunityComponent"] },
];


/***/ }),

/***/ "./src/app/table-list/table-list.component.css":
/*!*****************************************************!*\
  !*** ./src/app/table-list/table-list.component.css ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".example-full-width {\r\n    width: 30px;\r\n    float: right;\r\n  }"

/***/ }),

/***/ "./src/app/table-list/table-list.component.html":
/*!******************************************************!*\
  !*** ./src/app/table-list/table-list.component.html ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main-content\">\n  <div class=\"container-fluid\">\n      <div class=\"row\">\n          <div class=\"col-md-12\">\n\n                <!-- <form #signOrderForm=\"ngForm\" (ngSubmit)=\"signOrder(signOrderForm)\">\n                    <div class=\"form-group\">\n                        <label>Approve an order by signing:</label> \n                        <input type=\"text\" class=\"form-control\" name=\"buyer\" id=\"tr-member-input1\" placeholder=\"Buyer\" ngModel>\n                        <input type=\"text\" class=\"form-control\" name=\"producerBase\" id=\"tr-member-input2\" placeholder=\"Guild address\" ngModel >\n                        <input type=\"text\" class=\"form-control\" name=\"storeFrontId\" id=\"tr-member-input3\" placeholder=\"Store Front Id\" ngModel  >\n                        <input type=\"text\" class=\"form-control\" name=\"productId\" id=\"tr-member-input4\" placeholder=\"Product Id\" ngModel >\n                        <input type=\"text\" class=\"form-control\" name=\"amount\" id=\"tr-member-input5\" placeholder=\"Amount\" ngModel >\n                        <input type=\"text\" class=\"form-control\" name=\"pricePerUnit\" id=\"tr-member-input6\" placeholder=\"Price per unit\" ngModel >\n                        <input type=\"text\" class=\"form-control\" name=\"validUntil\" id=\"tr-member-input7\" placeholder=\"Valid until\" ngModel > -->\n\n                        <!-- <mat-form-field>\n                                <input matInput [matDatepicker]=\"validUntil\" placeholder=\"Valid until\">\n                                <mat-datepicker-toggle matSuffix [for]=\"validUntil\"></mat-datepicker-toggle>\n                                <mat-datepicker #picker></mat-datepicker>\n                        </mat-form-field> -->\n                    <!-- </div>\n                    <button type=\"submit\" value=\"submit\" class=\"btn btn-danger btn-round\">Sign order</button>\n                </form>\n                <input type=\"text\" ng-readonly=\"checked\" value=\"{{nonce}}\" ng-readonly=\"all\"/>\n                <input type=\"text\" ng-readonly=\"checked\" value=\"{{signedOrder}}\" ng-readonly=\"all\"/>\n                <br/>\n                <form #sendOrderForm=\"ngForm\" (ngSubmit)=\"sendOrder(sendOrderForm)\">\n                        <div class=\"form-group\">\n                            <label>Buy product:</label> \n                            <input type=\"text\" class=\"form-control\" name=\"seller\" id=\"tr-member-input1\" placeholder=\"Seller\" ngModel>\n                            <input type=\"text\" class=\"form-control\" name=\"producerBase\" id=\"tr-member-input2\" placeholder=\"Guild address\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"storeFrontId\" id=\"tr-member-input3\" placeholder=\"Store Front Id\" ngModel  >\n                            <input type=\"text\" class=\"form-control\" name=\"productId\" id=\"tr-member-input4\" placeholder=\"Product Id\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"amount\" id=\"tr-member-input5\" placeholder=\"Amount\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"pricePerUnit\" id=\"tr-member-input6\" placeholder=\"Price per unit\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"validUntil\" id=\"tr-member-input7\" placeholder=\"Valid until\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"amountPayed\" id=\"tr-member-input7\" placeholder=\"Payments in wei\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"nonce\" id=\"tr-member-input7\" placeholder=\"Nonce\" ngModel >\n                            <input type=\"text\" class=\"form-control\" name=\"signature\" id=\"tr-member-input7\" placeholder=\"Signature of Order\" ngModel > -->\n\n                            <!-- <mat-form-field>\n                                    <input matInput [matDatepicker]=\"validUntil\" placeholder=\"Valid until\">\n                                    <mat-datepicker-toggle matSuffix [for]=\"validUntil\"></mat-datepicker-toggle>\n                                    <mat-datepicker #picker></mat-datepicker>\n                            </mat-form-field> -->\n                        <!-- </div>\n                        <button type=\"submit\" value=\"submit\" class=\"btn btn-danger btn-round\">Buy Product</button>\n                    </form>\n                <button class=\"btn btn-danger btn-round\" (click) = \"getProduct()\">get product</button> -->\n\n\n\n              <div *ngIf=\"marketDeployed[0]\" class=\"card\">\n                  <div class=\"card-header card-header-danger\">\n                    <h4 class=\"card-title \">My Store Fronts\n                            <button type=\"button\" matTooltip=\"Open Store Front\" [matTooltipPosition]=\"'below'\" \n                            class ='badge badge-danger btn-link btn-s btn-just-icon' (click)=\"openStoreFront(producerBaseDeployed[0])\">\n                                +\n                            </button>\n                    </h4>\n                    <!-- <p class=\"card-category\"> All Store Fronts published \n                          @<span matTooltip=\"{{marketDeployed[0].address}}\" [matTooltipPosition]=\"'above'\" class=\"badge badge-danger\">{{marketDeployed[0].address}}</span>       \n                    </p> -->\n                  </div>\n                  <div class=\"card-body\">\n                      <div class=\"table-responsive\">\n                          <table class=\"table\">\n                              <thead class=\" text-primary\">\n                                  <th>\n                                      Store Front\n                                  </th>\n                                  <th>\n                                      Publish to\n                                  </th>\n                                  <th>\n                                      Created At\n                                  </th>\n                              </thead>\n                              <tbody *ngIf=\"storeFronts\" >\n\n                                <tr *ngFor=\"let sf of storeFronts\" >\n                                      <td>\n                                          <a matTooltip=\"View Products\" [matTooltipPosition]=\"'below'\"  class=\"btn btn-primary btn-link\" (click)=\"viewProducts(producerBaseDeployed[0], sf.id)\">{{sf.id}}</a>\n                                      </td>\n                                      <td>\n                                        <form #publishStoreFrontForm = \"ngForm\" (ngSubmit) = \"publishStoreFront(sf.id, publishStoreFrontForm)\">\n                                                <!-- <div class=\"form-group\"> -->\n                                                <input type=\"text\" class=\"form-control\" name=\"marketAddress\" placeholder=\"Market Address\" ngModel required length=\"42\">\n                                                <!-- </div> -->\n                                                <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Publish Store Front\" [matTooltipPosition]=\"'below'\" \n                                                    class=\"btn btn-danger btn-link btn-just-icon\">\n                                                    <i class=\"material-icons\">publish</i>\n                                                </button>\n                                        </form>\n                                      </td>\n                                      <td>\n                                          {{sf.createdAt}}\n                                      </td>\n                                      <!-- <td>\n                                            <button type=\"button\" matTooltip=\"Add Product\" [matTooltipPosition]=\"'below'\" \n                                            class ='badge badge-danger btn-link btn-s btn-just-icon' (click)=\"setUpAddProductForm(sf.id, 0)\">\n                                                +\n                                            </button>\n                                    </td> -->\n                                      <td class=\"text-primary\" >\n                                          <button  mat-raised-button class=\"btn btn-danger btn-link\" (click)=\"enDisStoreFront(producerBaseDeployed[0], sf.id, sf.isDisabled)\">\n                                            {{ sf.isDisabled ? 'Enable' : 'Disable' }}\n                                          </button>\n                                      </td>\n                                  </tr>\n\n                              </tbody>\n                          </table>\n                      </div>\n                      <button class ='example-full-width badge badge-danger btn-link' (click)=\"retrieveAllStoreFronts(producerBaseDeployed[0])\" >>></button>\n                      <mat-form-field class=\"example-full-width\">\n                        <input matInput [(ngModel)]=\"pageNum\" placeholder=\"page\">\n                      </mat-form-field>\n                  </div>\n              </div>\n          </div>\n       \n         <div *ngIf=\"chosenStoreFront\"  class=\"col-md-12\">\n              <div class=\"card card-plain\">\n                  <div class=\"card-header card-header-danger\">\n                      <h4 class=\"card-title mt-0\"> Products @ Store Front {{chosenStoreFront}}</h4>\n                      <!-- <p class=\"card-category\"> Here is a subtitle for this table</p> -->\n                  </div>\n                  <div class=\"card-body\">\n                        <div class=\"container-fluid\">\n                            <div class=\"row\">\n                                <div class=\"col-lg-10 col-md-12\">\n                                <div class=\"table-responsive\">\n                                    <table class=\"table table-hover\">\n                                        <thead >\n                                            <th>\n                                                    Specification Id\n                                            </th>\n                                            <th>\n                                                    Edited At\n                                            </th>\n                                            <th>\n                                                    Amount\n                                            </th>\n                                            <th>\n                                                    Price Per Unit\n                                            </th>\n                                            <th>\n                                                    Has Negotiable Price\n                                            </th>\n                                        </thead>\n                                        <tbody>\n                                            <tr *ngFor=\"let pr of productTable; let i = index\">\n                                                <td>\n                                                    {{pr.specificationId}}                                                \n                                                </td>\n                                                <td>\n                                                    {{pr.editedAt}}\n                                                </td>\n                                                <td>\n                                                    {{pr.amount}}\n                                                </td>\n                                                <td>\n                                                    {{pr.pricePerUnit}}\n                                                </td>\n                                                <td>\n                                                    {{pr.hasNegotiablePrice}}\n                                                </td>\n                                                <td>\n                                                    <button  mat-raised-button class=\"btn btn-danger btn-link\" (click)=\"showEditForm(i)\">\n                                                            edit\n                                                    </button>\n                                                </td>\n                                            </tr>\n                                        </tbody>\n                                    </table>\n                                </div>\n                                </div>\n                                <div class=\"col-lg-2 col-md-12\">\n                                        <div class=\"card-footer\">\n                                                <form *ngIf=\"!editBtnMode\" #addProductForm = \"ngForm\" (ngSubmit) = \"addProduct(addProductForm)\">\n                                                        <div class=\"form-group\">\n                                                            <!-- <label>Add Product to Store Front:</label>  -->\n                                                            <input type=\"text\" class=\"form-control\" name=\"specificationId\" placeholder=\"Specification Id\" ngModel required>\n                                                            <input type=\"text\" class=\"form-control\" name=\"pricePerUnit\" placeholder=\"Price Per Unit\" ngModel required>\n                                                            <input type=\"text\" class=\"form-control\" name=\"amount\" placeholder=\"Amount\" ngModel required>\n                                                            \n                                                            <input type=\"checkbox\" class=\"form-control-lg\" id=\"pr_has_NegotiablePrice\" name=\"hasNegotiablePrice\" ngModel required>\n                                                            <label for=\"pr_has_NegotiablePrice\">Has negotiable price</label>\n                                                        </div>\n                                                        <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Add Product\" [matTooltipPosition]=\"'below'\" \n                                                            class=\"btn btn-danger btn-link\">\n                                                            add product\n                                                        </button>\n                                                </form>\n                                                <form  *ngIf=\"editBtnMode\" #editProductForm = \"ngForm\" (ngSubmit) = \"editProduct(editProductForm)\">\n                                                        <div class=\"form-group\">\n                                                            <!-- <label>Add Product to Store Front:</label>  -->\n                                                            <input type=\"text\" class=\"form-control\"  [(ngModel)]=\"edittedProduct.specificationId\" name=\"specificationId\" placeholder=\"Specification Id\" required>\n                                                            <input type=\"text\" class=\"form-control\" [(ngModel)]=\"edittedProduct.pricePerUnit\" name = \"pricePerUnit\" placeholder=\"Price Per Unit\" required>\n                                                            <input type=\"text\" class=\"form-control\" [(ngModel)]=\"edittedProduct.amount\" name = \"amount\" placeholder=\"Amount\" required>\n                                                            \n                                                            <input type=\"checkbox\" class=\"form-control-lg\" [(ngModel)]=\"edittedProduct.hasNegotiablePrice\" id=\"pr_has_NegotiablePrice\" name=\"hasNegotiablePrice\" required>\n                                                            <label for=\"pr_has_NegotiablePrice\">Has negotiable price</label>\n                                                        </div>\n                                                        <button mat-raised-button type=\"submit\" value=\"submit\" matTooltip=\"Add Product\" [matTooltipPosition]=\"'below'\" \n                                                            class=\"btn btn-danger btn-link\">\n                                                            edit product\n                                                        </button>\n                                                </form>\n                                        </div>\n                                </div>\n                            </div>\n                            <button class ='example-full-width badge badge-danger btn-link' (click)=\"viewProducts(producerBaseDeployed[0], chosenStoreFront)\" >>></button>\n                            <mat-form-field class=\"example-full-width\">\n                                    <input matInput [(ngModel)]=\"productPageNum\" placeholder=\"page\">\n                            </mat-form-field>\n                        </div>\n                    </div>\n                </div>\n          </div> \n      </div>\n  </div>\n</div>\n"

/***/ }),

/***/ "./src/app/table-list/table-list.component.ts":
/*!****************************************************!*\
  !*** ./src/app/table-list/table-list.component.ts ***!
  \****************************************************/
/*! exports provided: TableListComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TableListComponent", function() { return TableListComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _service_proxies_web3_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../service-proxies/web3.service */ "./src/app/service-proxies/web3.service.ts");
/* harmony import */ var _service_proxies_local_storage_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../service-proxies/local-storage.service */ "./src/app/service-proxies/local-storage.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __awaiter = (undefined && undefined.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (undefined && undefined.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};





var abi = __webpack_require__(/*! ethereumjs-abi */ "./node_modules/ethereumjs-abi/index.js");
var Web3 = __webpack_require__(/*! web3 */ "./node_modules/web3/src/index.js");
var producerBaseABI = __webpack_require__(/*! ../../../../MarketChain/build/contracts/ProducerBase.json */ "../MarketChain/build/contracts/ProducerBase.json");
var regionalMarketABI = __webpack_require__(/*! ../../../../MarketChain/build/contracts/RegionalMarket.json */ "../MarketChain/build/contracts/RegionalMarket.json");
var TableListComponent = /** @class */ (function () {
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
    function TableListComponent(web3Service, matSnackBar, localStoreage, router) {
        this.web3Service = web3Service;
        this.matSnackBar = matSnackBar;
        this.localStoreage = localStoreage;
        this.router = router;
        this.status = '';
        this.marketDeployed = [];
        this.clientBaseDeployed = [];
        this.producerBaseDeployed = [];
        this.pageNum = 0;
        this.productPageNum = 0;
        this.editBtnMode = false;
        this.addProductInfo = {
            storeFrontId: 0,
            producerBaseIndex: 0
        };
        this.addProductToStoreFrontId = 0;
        this.filterData = {
            chosenMarketId: '',
            stoFrontPage: 0,
            chosenStoreFrontId: 0,
            itemsPage: 0
        };
        this.getDateInUnixTimestamp = function (date) { return Math.floor((new Date(date)).getTime() / 1000); };
    }
    TableListComponent.prototype.ngOnInit = function () {
        var _this = this;
        var that = this;
        window.ethereum.on('accountsChanged', function (accounts) {
            return __awaiter(this, void 0, void 0, function () {
                return __generator(this, function (_a) {
                    that.router.navigate(['/home']);
                    that.setStatus('Navigating to Home Page.');
                    return [2 /*return*/];
                });
            });
        });
        this.web3Service.artifactsToContract(producerBaseABI)
            .then(function (ProducerBaseAbstraction) {
            ProducerBaseAbstraction.deployed().then(function (inst) {
                _this.producerBaseDeployed.push(inst);
            });
        });
        this.web3Service.artifactsToContract(regionalMarketABI)
            .then(function (MarketAbstraction) {
            MarketAbstraction.deployed().then(function (inst) {
                _this.marketDeployed.push(inst);
            });
        });
        new Promise(function (resolve) { return _this.loadInitData(); });
    };
    TableListComponent.prototype.setStatus = function (status) {
        this.matSnackBar.open(status, null, { duration: 3000 });
    };
    TableListComponent.prototype.loadInitData = function () {
        return __awaiter(this, void 0, void 0, function () {
            var delay, that;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!(this.producerBaseDeployed.length == 0)) return [3 /*break*/, 3];
                        delay = new Promise(function (resolve) { return setTimeout(resolve, 1000); });
                        return [4 /*yield*/, delay];
                    case 1:
                        _a.sent();
                        return [4 /*yield*/, this.loadInitData()];
                    case 2: return [2 /*return*/, _a.sent()];
                    case 3:
                        that = this;
                        this.producerBaseDeployed.forEach(function (pb) {
                            return __awaiter(this, void 0, void 0, function () {
                                return __generator(this, function (_a) {
                                    switch (_a.label) {
                                        case 0: return [4 /*yield*/, that.retrieveAllStoreFronts(pb)];
                                        case 1:
                                            _a.sent();
                                            return [2 /*return*/];
                                    }
                                });
                            });
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.ngOnChanges = function (changes) {
        location.reload();
    };
    //====================
    TableListComponent.prototype.signOrder = function (formData) {
        return __awaiter(this, void 0, void 0, function () {
            var seller, invoice, nonce, signedOrder;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        seller = this.web3Service.getDefaultAccount();
                        invoice = {
                            seller: seller,
                            buyer: formData.value.buyer,
                            producerBase: formData.value.producerBase,
                            storeFrontId: formData.value.storeFrontId,
                            productId: formData.value.productId,
                            amount: formData.value.amount,
                            pricePerUnit: formData.value.pricePerUnit,
                            validUntil: this.getDateInUnixTimestamp(formData.value.validUntil)
                        };
                        return [4 /*yield*/, this.web3Service.getNonce(seller)];
                    case 1:
                        nonce = _a.sent();
                        this.nonce = parseInt(nonce);
                        console.log(invoice);
                        return [4 /*yield*/, this.makeSignature(invoice.seller, invoice.buyer, invoice.productId, invoice.amount, invoice.pricePerUnit, invoice.validUntil, nonce)];
                    case 2:
                        signedOrder = _a.sent();
                        this.signedOrder = signedOrder;
                        return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.sendOrder = function (formData) {
        return __awaiter(this, void 0, void 0, function () {
            var buyer, invoice, nonce, signature, amountPayed, transaction, transactionLogs, e_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        buyer = this.web3Service.getDefaultAccount();
                        invoice = {
                            seller: formData.value.seller,
                            buyer: buyer,
                            producerBase: formData.value.producerBase,
                            storeFrontId: formData.value.storeFrontId,
                            productId: formData.value.productId,
                            amount: formData.value.amount,
                            pricePerUnit: formData.value.pricePerUnit,
                            validUntil: this.getDateInUnixTimestamp(formData.value.validUntil)
                        };
                        nonce = formData.value.nonce;
                        signature = formData.value.signature;
                        amountPayed = formData.value.amountPayed;
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        return [4 /*yield*/, this.marketDeployed[0].buyProduct(invoice, nonce, signature, { from: buyer, value: amountPayed })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction successful!');
                            transactionLogs = transaction.logs;
                            // console.log(transactionLogs)
                            // transactionLogs.forEach(log => {
                            //   if(log.event == "LogProductAddedToStoreFront"){
                            //     this.showNotification('bottom','center', 'success', "New product with Id: " + log.args.productId.toString() + " has been added to the store successfuly.")
                            //   }
                            // });
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_1 = _a.sent();
                        console.log(e_1);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.openStoreFront = function (memberBase) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, transactionLogs, e_2;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 5, , 6]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.addStoreFront({ from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        if (!!transaction) return [3 /*break*/, 2];
                        this.setStatus('Transaction failed!');
                        return [3 /*break*/, 4];
                    case 2:
                        this.setStatus('Transaction succeeded!');
                        transactionLogs = transaction.logs;
                        transactionLogs.forEach(function (log) {
                            if (log.event == "LogStoreFrontCreated") {
                                _this.showNotification('bottom', 'center', 'success', "New store Front with Id " + log.args.storeFrontId.toString() + " has been added to the store successfuly.");
                            }
                        });
                        return [4 /*yield*/, this.retrieveAllStoreFronts(memberBase)
                            //location.reload();
                        ];
                    case 3:
                        _a.sent();
                        _a.label = 4;
                    case 4: return [3 /*break*/, 6];
                    case 5:
                        e_2 = _a.sent();
                        console.log(e_2);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 6];
                    case 6: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.retrieveAllStoreFronts = function (memberBase) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_3;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.getStoreFrontsByPageNum(default_account, this.pageNum, { from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                            this.storeFronts = transaction
                                .filter(function (item) { return item.id != '0'; })
                                .map(function (item) {
                                var newEntry = { id: 0, createdAt: '', isDisabled: false };
                                newEntry.id = parseInt(item.id);
                                newEntry.createdAt = _this.unixToDate(parseInt(item.createdAt.toString()));
                                newEntry.isDisabled = item.isDisabled == 'true';
                                return newEntry;
                            });
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        e_3 = _a.sent();
                        console.log(e_3);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        });
    };
    //TODO: Fix this
    TableListComponent.prototype.enDisStoreFront = function (memberBase, storeFrontId, isDiabled) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_4;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 8, , 9]);
                        default_account = this.web3Service.getDefaultAccount();
                        transaction = void 0;
                        if (!(isDiabled == true)) return [3 /*break*/, 2];
                        return [4 /*yield*/, memberBase.enableStoreFront(storeFrontId, { from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        return [3 /*break*/, 4];
                    case 2: return [4 /*yield*/, memberBase.disableStoreFront(storeFrontId, { from: default_account })];
                    case 3:
                        transaction = _a.sent();
                        _a.label = 4;
                    case 4:
                        if (!!transaction) return [3 /*break*/, 5];
                        this.setStatus('Transaction failed!');
                        return [3 /*break*/, 7];
                    case 5:
                        if (isDiabled == true) {
                            this.setStatus('Transaction succeeded! Store Front enabled');
                        }
                        else {
                            this.setStatus('Transaction succeeded! Store Front disabled');
                        }
                        return [4 /*yield*/, this.retrieveAllStoreFronts(memberBase)];
                    case 6:
                        _a.sent();
                        _a.label = 7;
                    case 7: return [3 /*break*/, 9];
                    case 8:
                        e_4 = _a.sent();
                        console.log(e_4);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 9];
                    case 9: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.viewProducts = function (memberBase, storeFrontId) {
        return __awaiter(this, void 0, void 0, function () {
            var default_account, transaction, e_5;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        this.resetEditForm();
                        default_account = this.web3Service.getDefaultAccount();
                        this.chosenStoreFront = storeFrontId;
                        return [4 /*yield*/, memberBase.getProductsByPageNum(default_account, storeFrontId, this.productPageNum, { from: default_account })];
                    case 1:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded!');
                            console.log(transaction);
                            this.productTable = transaction
                                .filter(function (item) { return item.id != '0'; })
                                .map(function (item) {
                                var newEntry = { id: 0, editedAt: '', specificationId: 0, amount: 0, pricePerUnit: 0, hasNegotiablePrice: false };
                                newEntry.id = parseInt(item.id);
                                newEntry.specificationId = parseInt(item.specificationId);
                                newEntry.amount = parseInt(item.amount);
                                newEntry.pricePerUnit = parseInt(item.pricePerUnit);
                                newEntry.editedAt = _this.unixToDate(parseInt(item.editedAt.toString()));
                                newEntry.hasNegotiablePrice = item.hasNegotiablePrice == 'true';
                                return newEntry;
                            });
                            console.log(this.productTable);
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        e_5 = _a.sent();
                        console.log(e_5);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.resetEditForm = function () {
        this.editBtnMode = false;
        if (this.edittedProduct) {
            this.edittedProduct = {
                id: 0,
                specificationId: 0,
                editedAt: '',
                amount: 0,
                pricePerUnit: 0,
                hasNegotiablePrice: false
            };
        }
    };
    TableListComponent.prototype.showEditForm = function (prindex) {
        this.editBtnMode = true;
        this.chosenProductIndex = prindex;
        var prData = this.productTable[prindex];
        this.edittedProduct = {
            id: prData.id,
            specificationId: prData.specificationId,
            editedAt: '',
            amount: prData.amount,
            pricePerUnit: prData.pricePerUnit,
            hasNegotiablePrice: prData.hasNegotiablePrice
        };
    };
    TableListComponent.prototype.addProduct = function (formData) {
        return __awaiter(this, void 0, void 0, function () {
            var memberBase, storeFronId, specificationId, pricePerUnit, amount, hasNegotiablePrice, default_account, transaction, transactionLogs, shown_1, e_6;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        memberBase = this.producerBaseDeployed[0];
                        storeFronId = this.chosenStoreFront;
                        specificationId = formData.value.specificationId;
                        pricePerUnit = parseInt(formData.value.pricePerUnit);
                        amount = parseInt(formData.value.amount);
                        hasNegotiablePrice = formData.value.hasNegotiablePrice == 'true';
                        formData.reset();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.addProductToStoreFront(storeFronId, specificationId, pricePerUnit, amount, hasNegotiablePrice, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            transactionLogs = transaction.logs;
                            console.log(transactionLogs);
                            shown_1 = false;
                            transactionLogs.forEach(function (log) {
                                if (log.event == "LogProductAddedToStoreFront" && !shown_1) {
                                    _this.showNotification('bottom', 'center', 'success', "New product with Id " + log.args.productId.toString() + " has been added to the store successfuly.");
                                    shown_1 = true;
                                }
                            });
                            this.addProductInfo.storeFrontId = 0;
                            this.viewProducts(memberBase, storeFronId);
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_6 = _a.sent();
                        console.log(e_6);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.editProduct = function (formData) {
        return __awaiter(this, void 0, void 0, function () {
            var memberBase, storeFronId, productId, specificationId, pricePerUnit, amount, hasNegotiablePrice, default_account, transaction, e_7;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        memberBase = this.producerBaseDeployed[0];
                        storeFronId = this.chosenStoreFront;
                        productId = this.edittedProduct.id;
                        specificationId = formData.value.specificationId;
                        pricePerUnit = parseInt(formData.value.pricePerUnit);
                        amount = parseInt(formData.value.amount);
                        hasNegotiablePrice = formData.value.hasNegotiablePrice == 'true';
                        formData.reset();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, memberBase.updateProduct(storeFronId, productId, pricePerUnit, amount, hasNegotiablePrice, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.addProductInfo.storeFrontId = 0;
                            this.viewProducts(memberBase, storeFronId);
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_7 = _a.sent();
                        console.log(e_7);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.publishStoreFront = function (storeFrontId, formData) {
        return __awaiter(this, void 0, void 0, function () {
            var marketAddress, default_account, transaction, e_8;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        marketAddress = formData.value.marketAddress;
                        formData.reset();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        default_account = this.web3Service.getDefaultAccount();
                        return [4 /*yield*/, this.producerBaseDeployed[0].publishStoreFrontToMarket(marketAddress, storeFrontId, { from: default_account })];
                    case 2:
                        transaction = _a.sent();
                        if (!transaction) {
                            this.setStatus('Transaction failed!');
                        }
                        else {
                            this.setStatus('Transaction succeeded! Store Front published');
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        e_8 = _a.sent();
                        console.log(e_8);
                        this.setStatus('Error; see log');
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    TableListComponent.prototype.makeSignature = function (seller, buyer, productId, amount, pricePerUnit, validUntil, nonce) {
        return __awaiter(this, void 0, void 0, function () {
            var hash;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        hash = "0x" + abi.soliditySHA3(["address", "address", "uint256", "uint256", "uint256", "uint256", "uint256"], [seller, buyer, productId, amount, pricePerUnit, validUntil, nonce]).toString("hex");
                        hash = "0x" + abi.soliditySHA3(["string", "bytes32"], ["\x19Ethereum Signed Message:\n32", hash]).toString("hex");
                        return [4 /*yield*/, this.web3Service.signHash(hash, seller)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    TableListComponent.prototype.unixToDate = function (unix_timestamp) {
        // Create a new JavaScript Date object based on the timestamp
        // multiplied by 1000 so that the argument is in milliseconds, not seconds.
        var date = new Date(unix_timestamp * 1000);
        // Hours part from the timestamp
        var hours = date.getHours();
        // Minutes part from the timestamp
        var minutes = "0" + date.getMinutes();
        // Seconds part from the timestamp
        var seconds = "0" + date.getSeconds();
        // Will display time in 10:30:23 format
        var formattedTime = hours + ':' + minutes.substr(-2) + ':' + seconds.substr(-2);
        var mm = date.getMonth() + 1; // getMonth() is zero-based
        var dd = date.getDate();
        return mm + '/' + dd + '/' + date.getFullYear() + '  ' + formattedTime;
    };
    TableListComponent.prototype.showNotification = function (from, align, color, msg) {
        //const type = ['','info','success','warning','danger'];
        $.notify({
            icon: "notifications",
            message: msg
        }, {
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
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], TableListComponent.prototype, "pageNum", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], TableListComponent.prototype, "productPageNum", void 0);
    TableListComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-table-list',
            template: __webpack_require__(/*! ./table-list.component.html */ "./src/app/table-list/table-list.component.html"),
            styles: [__webpack_require__(/*! ./table-list.component.css */ "./src/app/table-list/table-list.component.css")]
        }),
        __metadata("design:paramtypes", [_service_proxies_web3_service__WEBPACK_IMPORTED_MODULE_3__["Web3Service"],
            _angular_material__WEBPACK_IMPORTED_MODULE_1__["MatSnackBar"],
            _service_proxies_local_storage_service__WEBPACK_IMPORTED_MODULE_4__["LocalStorageService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])
    ], TableListComponent);
    return TableListComponent;
}());



/***/ }),

/***/ "./src/app/user-profile/user-profile.component.css":
/*!*********************************************************!*\
  !*** ./src/app/user-profile/user-profile.component.css ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/user-profile/user-profile.component.html":
/*!**********************************************************!*\
  !*** ./src/app/user-profile/user-profile.component.html ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"main-content\">\n  <div class=\"container-fluid\">\n      <div class=\"row\">\n          <div class=\"col-md-8\">\n              <div class=\"card\">\n                  <div class=\"card-header card-header-danger\">\n                      <h4 class=\"card-title\">Edit Profile</h4>\n                      <p class=\"card-category\">Complete your profile</p>\n                  </div>\n                  <div class=\"card-body\">\n                      <form>\n                          <div class=\"row\">\n                              <div class=\"col-md-5\">\n                                <mat-form-field class=\"example-full-width\">\n                                    <input matInput placeholder=\"Company (disabled)\" disabled>\n                                  </mat-form-field>\n                              </div>\n                              <div class=\"col-md-3\">\n                                  <mat-form-field class=\"example-full-width\">\n                                    <input matInput placeholder=\"Username\">\n                                  </mat-form-field>\n                              </div>\n                              <div class=\"col-md-4\">\n                                  <mat-form-field class=\"example-full-width\">\n                                    <input matInput placeholder=\"Email address\" type=\"email\">\n                                  </mat-form-field>\n                              </div>\n                          </div>\n                          <div class=\"row\">\n                              <div class=\"col-md-6\">\n                                <mat-form-field class=\"example-full-width\">\n                                  <input matInput placeholder=\"Fist Name\" type=\"text\">\n                                </mat-form-field>\n                              </div>\n                              <div class=\"col-md-6\">\n                                <mat-form-field class=\"example-full-width\">\n                                  <input matInput placeholder=\"Last Name\" type=\"text\">\n                                </mat-form-field>\n                              </div>\n                          </div>\n                          <div class=\"row\">\n                              <div class=\"col-md-12\">\n                                <mat-form-field class=\"example-full-width\">\n                                  <input matInput placeholder=\"Adress\" type=\"text\">\n                                </mat-form-field>\n                              </div>\n                          </div>\n                          <div class=\"row\">\n                              <div class=\"col-md-4\">\n                                <mat-form-field class=\"example-full-width\">\n                                  <input matInput placeholder=\"City\" type=\"text\">\n                                </mat-form-field>\n                              </div>\n                              <div class=\"col-md-4\">\n                                <mat-form-field class=\"example-full-width\">\n                                  <input matInput placeholder=\"Country\" type=\"text\">\n                                </mat-form-field>\n                              </div>\n                              <div class=\"col-md-4\">\n                                <mat-form-field class=\"example-full-width\">\n                                  <input matInput placeholder=\"Postal Code\" type=\"text\">\n                                </mat-form-field>\n                              </div>\n                          </div>\n                          <div class=\"row\">\n                              <div class=\"col-md-12\">\n                                <label>About Me</label>\n                                <mat-form-field class=\"example-full-width\">\n                                   <textarea matInput placeholder=\"Lamborghini Mercy, Your chick she so thirsty, I'm in that two seat Lambo.\"></textarea>\n                                 </mat-form-field>\n                                  <!-- <div class=\"form-group\">\n\n                                      <div class=\"form-group\">\n                                          <label class=\"bmd-label-floating\"> Lamborghini Mercy, Your chick she so thirsty, I'm in that two seat Lambo.</label>\n                                          <textarea class=\"form-control\" rows=\"5\"></textarea>\n                                      </div>\n                                  </div> -->\n                              </div>\n                          </div>\n                          <button mat-raised-button type=\"submit\" class=\"btn btn-danger pull-right\">Update Profile</button>\n                          <div class=\"clearfix\"></div>\n                      </form>\n                  </div>\n              </div>\n          </div>\n          <div class=\"col-md-4\">\n              <div class=\"card card-profile\">\n                  <div class=\"card-avatar\">\n                      <a href=\"#pablo\">\n                          <img class=\"img\" src=\"./assets/img/faces/marc.jpg\" />\n                      </a>\n                  </div>\n                  <div class=\"card-body\">\n                      <h6 class=\"card-category text-gray\">CEO / Co-Founder</h6>\n                      <h4 class=\"card-title\">Alec Thompson</h4>\n                      <p class=\"card-description\">\n                          Don't be scared of the truth because we need to restart the human foundation in truth And I love you like Kanye loves Kanye I love Rick Owens’ bed design but the back is...\n                      </p>\n                      <a href=\"#pablo\" class=\"btn btn-danger btn-round\">Follow</a>\n                  </div>\n              </div>\n          </div>\n      </div>\n  </div>\n</div>\n"

/***/ }),

/***/ "./src/app/user-profile/user-profile.component.ts":
/*!********************************************************!*\
  !*** ./src/app/user-profile/user-profile.component.ts ***!
  \********************************************************/
/*! exports provided: UserProfileComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserProfileComponent", function() { return UserProfileComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var UserProfileComponent = /** @class */ (function () {
    function UserProfileComponent() {
    }
    UserProfileComponent.prototype.ngOnInit = function () {
    };
    UserProfileComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-user-profile',
            template: __webpack_require__(/*! ./user-profile.component.html */ "./src/app/user-profile/user-profile.component.html"),
            styles: [__webpack_require__(/*! ./user-profile.component.css */ "./src/app/user-profile/user-profile.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], UserProfileComponent);
    return UserProfileComponent;
}());



/***/ })

}]);
//# sourceMappingURL=layouts-admin-layout-admin-layout-module.js.map