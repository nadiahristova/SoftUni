// var ProducerBase = artifacts.require("ProducerBase");
// var Market = artifacts.require("RegionalMarket");

// const abi = require('ethereumjs-abi')

// const Web3 = require('web3')
// const provider = new Web3.providers.HttpProvider('http://localhost:7545')
// const web3 = new Web3(provider)

// //import Personal from 'web3-eth-personal';

// //const Personal = require('web3-eth-personal')

// // "Web3.givenProvider" will be set if in an Ethereum supported browser.
// //const personal = new Personal(Web3.givenProvider);

// const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers')
// const { shared_func, shared_const, MSG } = require('./_utils.js')

// contract('BaseMarket', function ([owner, producer, client, not_a_member]) {

//     //shared parameters
//     let market; 
//     let producer_base;
//     let producer_base_adr;

//     //console.log(Personal)

//     class Invoice {
//         constructor(seller, buyer, producerBase, storeFrontId, productId, amount, pricePerUnit, validUntil) {
//           this.seller = seller;
//           this.buyer = buyer;
//           this.producerBase = producerBase;
//           this.storeFrontId = storeFrontId;
//           this.productId = productId;
//           this.amount = amount;
//           this.pricePerUnit = pricePerUnit;
//           this.validUntil = validUntil;
//         }

//         toJSON() {
//             let { seller, buyer, producerBase, storeFrontId, productId, amount, pricePerUnit, validUntil } = this;
//             return { seller, buyer, producerBase, storeFrontId, productId, amount, pricePerUnit, validUntil };
//           }
//     }

//     class Product {
//         constructor(specificationId, pricePerUnit, amount, hasNegotiablePrice) {
//           this.specificationId = specificationId;
//           this.pricePerUnit = pricePerUnit;
//           this.amount = amount;
//           this.hasNegotiablePrice = hasNegotiablePrice;
//         }

//         toJSON() {
//             let { specificationId, pricePerUnit, amount, hasNegotiablePrice } = this;
//             return { specificationId, pricePerUnit, amount, hasNegotiablePrice };
//           }
//     }

//     const default_store_name = web3.utils.fromAscii('Really cool store', 32);
//     const location = shared_const.default_location
//     const default_product = new Product(1, 100, 15, false)

//     // let signOrder = function (amount, nonce, callback) {
//     //     var hash = "0x" + ethereumjs.ABI.soliditySHA3(
//     //       ["address", "uint256", "uint256"],
//     //       [web3.eth.defaultAccount, amount, nonce]
//     //     ).toString("hex");
      
//     //     web3.personal.sign(hash, web3.eth.defaultAccount, callback);
//     //   }

//     let getDateInUnixTimestamp = (date) => Math.floor((new Date(date)).getTime()/ 1000);

//     before(() => {
//         web3.eth.defaultAccount = producer;
//     });
//     beforeEach(async () => {
//         market = await Market.new()
//         producer_base = await ProducerBase.deployed()
//         producer_base_adr = producer_base.address;

//         await shared_func.initializeRegionalMarket(market);
//         await shared_func.initializeProducerBase(producer_base) // initialize producer base

//         await shared_func.registerMarketMember(owner, producer, location, market) // register producer address in market
        
//         await shared_func.registerMember(owner, producer, producer_base)// add member to producer base
        
//         await shared_func.registerMarket(owner, market, producer_base)// register market in the producer base

//         await producer_base.requestMarketMembership(market.address, { from: producer })
        
//         await producer_base.addStoreFront({ from: producer }); 

//         await shared_func.openMarketStore(owner, producer, default_store_name, producer_base.address, market)
//     });
// /**
//     it("should allow a member with a store to publish it to given market", async () => {

//         const expectedEventResult = {
//             storeFrontId : new BN(1),
//             storeOwner: producer,
//             marketAddress: market.address
//         };

//         const result_publish = await producer_base.publishStoreFrontToMarket(market.address, 1, { from: producer }); // verify creation of store front

//         const result_has_SF = await market.hasStoreFront(producer, producer_base_adr, 1); // verify publication of store front


//         expectEvent.inLogs(result_publish.logs, 'LogStoreFrontShared', { storeOwner: expectedEventResult.storeOwner, storeFrontId: expectedEventResult.storeFrontId, 
//             market: expectedEventResult.marketAddress }, 'LogStoreFrontShared event account property not emitted or correct, check publishStoreFrontToMarket method');

//         assert.equal(result_publish.receipt.status, true, 'was not able to pushe the store front')
//         assert.equal(result_has_SF, true, 'store front was not registered to market');
//     })
//  */
//     // it("should allow a client to buy a product", async () => {

//     //     let expected_SFId = 1; 
//     //     let expected_productId = 1; 

//     //     await shared_func.addProduct(expected_SFId, producer, producer_base, default_product);

//     //     let default_invoice = new Invoice(
//     //         producer, // seller
//     //         client, // buyer
//     //         producer_base.address, // producer base Address
//     //         expected_SFId, //storeFrontId
//     //         expected_productId, //productId
//     //         default_product.amount, 
//     //         default_product.pricePerUnit, 
//     //         getDateInUnixTimestamp('08/08/2019'))//validUntil

//     //     const nonce = await web3.eth.getTransactionCount(web3.eth.defaultAccount, 'pending')

//     //     // const message = web3.utils.sha3(
//     //       default_invoice.seller,
//     //       default_invoice.buyer,
//     //       default_invoice.productId,
//     //       default_invoice.amount,
//     //       default_invoice.pricePerUnit,
//     //       default_invoice.validUntil,
//     //       nonce
//     //     // )

//     //     let signOrder = async function (seller, buyer, productId, amount, pricePerUnit, validUntil, nonce) {
//     //         var hash = "0x" + abi.soliditySHA3(
//     //           ["address", "address", "uint256", "uint256", "uint256", "uint256", "uint256"],
//     //           [seller, buyer, productId, amount, pricePerUnit, validUntil, nonce]
//     //         ).toString("hex");
           
//     //         // keccak256(abi.encodePacked("\x19Ethereum Signed Message:\n32", hash));
//     //         // "\x19Ethereum Signed Message:\n32"
          
//     //         //return web3.eth.sign(hash, seller)
//     //         //keccak256(abi.encodePacked("\x19Ethereum Signed Message:\n32", hash));
//     //         //web3.utils.keccak256(web3.eth.abi.encodeParameters(...))
//     //         //return "0x" + abi.soliditySHA3(["string", "bytes32"], ["\x19Ethereum Signed Message:\n32", hash]).toString("hex")
//     //             return await web3.eth.sign(hash, seller)

//     //             //return web3.personal.sign(hash, seller);
//     //     }

//     //     // const signature = web3.eth.sign(producer, web3.utils.sha3(default_invoice.seller,
//     //     //     default_invoice.buyer,
//     //     //     default_invoice.productId,
//     //     //     default_invoice.amount,
//     //     //     default_invoice.pricePerUnit,
//     //     //     default_invoice.validUntil,
//     //     //     nonce));

        
//     //     let signature = await signOrder(         
//     //         default_invoice.seller,
//     //         default_invoice.buyer,
//     //         default_invoice.productId,
//     //         default_invoice.amount,
//     //         default_invoice.pricePerUnit,
//     //         default_invoice.validUntil,
//     //         nonce)

//     //     console.log('Signature: >> ' + signature + ' <<')

//     //     const invoiceDetails = default_invoice.toJSON();
//     //     //let result = await market.buyProduct(invoiceDetails, nonce, signature, { from: client}); // verify publication of store front

//     //     let result = await market._returnHash(invoiceDetails, nonce, signature, { from: client})
//     //     console.log('producer: >> ' + producer + ' <<')
//     //     console.log('Signer: >> ' + result + ' <<')
  
//     // })

//     it("should blat", async () => {
// /** 
//         let expected_SFId = 1; 
//         let expected_productId = 1; 

//         await shared_func.addProduct(expected_SFId, producer, producer_base, default_product);

//         let default_invoice = new Invoice(
//             producer, // seller
//             client, // buyer
//             producer_base.address, // producer base Address
//             expected_SFId, //storeFrontId
//             expected_productId, //productId
//             default_product.amount, 
//             default_product.pricePerUnit, 
//             getDateInUnixTimestamp('08/08/2019'))//validUntil

//         const nonce = await web3.eth.getTransactionCount(web3.eth.defaultAccount, 'pending')

//         const message = web3.utils.sha3(
//           default_invoice.seller,
//           default_invoice.buyer,
//           default_invoice.productId,
//           default_invoice.amount,
//           default_invoice.pricePerUnit,
//           default_invoice.validUntil,
//           nonce
//         )

//         let signature = await web3.eth.sign(message, producer)

//         console.log('Signature: >> ' + signature + ' <<')

//         const invoiceDetails = default_invoice.toJSON();
//         //let result = await market.buyProduct(invoiceDetails, nonce, signature, { from: client}); // verify publication of store front

//         let result = await market._returnHash(invoiceDetails, nonce, signature, { from: client})
//         console.log('producer: >> ' + producer + ' <<')
//         console.log('Signer: >> ' + result + ' <<')
  
//         */
//        let address = producer_base.address;

//        //console.log(producer_base)
//        console.log(address)
//     })
//     // it("should blat", async () => {

//     //     let expected_SFId = 1; 
//     //     let expected_productId = 1; 

//     //     await shared_func.addProduct(expected_SFId, producer, producer_base, default_product);

//     //     let default_invoice = new Invoice(
//     //         producer, // seller
//     //         client, // buyer
//     //         producer_base.address, // producer base Address
//     //         expected_SFId, //storeFrontId
//     //         expected_productId, //productId
//     //         default_product.amount, 
//     //         default_product.pricePerUnit, 
//     //         getDateInUnixTimestamp('08/08/2019'))//validUntil

//     //     const nonce = await web3.eth.getTransactionCount(web3.eth.defaultAccount, 'pending')

//     //     const message = web3.utils.sha3(
//     //       default_invoice.seller,
//     //       default_invoice.buyer,
//     //       default_invoice.productId,
//     //       default_invoice.amount,
//     //       default_invoice.pricePerUnit,
//     //       default_invoice.validUntil,
//     //       nonce
//     //     )
//     //     var params = [default_invoice.seller,
//     //         default_invoice.buyer,
//     //         default_invoice.productId,
//     //         default_invoice.amount,
//     //         default_invoice.pricePerUnit,
//     //         default_invoice.validUntil,
//     //         nonce]
//     //     let method = 'personal_sign'

//     //     let signature = web3.currentProvider.send({
//     //         method,
//     //         message,
//     //         producer
//     //       })

//     //     console.log('Signature: >> ' + signature + ' <<')

//     //     const invoiceDetails = default_invoice.toJSON();
//     //     //let result = await market.buyProduct(invoiceDetails, nonce, signature, { from: client}); // verify publication of store front

//     //     let result = await market._returnHash(invoiceDetails, nonce, signature, { from: client})
//     //     console.log('producer: >> ' + producer + ' <<')
//     //     console.log('Signer: >> ' + result + ' <<')
  
//     // })


    
// })