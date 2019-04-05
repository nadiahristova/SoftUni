var ProducerBase = artifacts.require("ProducerBase");
var Market = artifacts.require("RegionalMarket");

const { shouldFail, expectEvent, BN, time } = require('openzeppelin-test-helpers')
const { shared_func, shared_const, MSG } = require('./_utils.js')

contract('BaseMarket', function ([owner, producer, client, not_a_member]) {

    //shared parameters
    let market; 
    let producer_base;
    let producer_base_adr;

    const default_store_name = web3.utils.fromAscii('Really cool store', 32);
    const location = shared_const.default_location

    class Invoice {
        constructor(seller, buyer, producerBase, storeFrontId, productId, amount, pricePerUnit, validUntil) {
          this.seller = seller;
          this.buyer = buyer;
          this.producerBase = producerBase;
          this.storeFrontId = storeFrontId;
          this.productId = productId;
          this.amount = amount;
          this.pricePerUnit = pricePerUnit;
          this.validUntil = validUntil;
        }

        toJSON() {
            let { seller, buyer, producerBase, storeFrontId, productId, amount, pricePerUnit, validUntil } = this;
            return { seller, buyer, producerBase, storeFrontId, productId, amount, pricePerUnit, validUntil };
          }
    }

    let getDateInUnixTimestamp = (date) => Math.floor((new Date(date)).getTime()/ 1000);

    before(() => {
        web3.eth.defaultAccount = owner;
    });

    beforeEach(async () => {
        market = await Market.new()
        producer_base = await ProducerBase.deployed()
        producer_base_adr = producer_base.address;

        await shared_func.initializeRegionalMarket(market);
        await shared_func.initializeProducerBase(producer_base) // initialize producer base

        await shared_func.registerMarketMember(owner, producer, location, market) // register producer address in market
        
        await shared_func.registerMember(owner, producer, producer_base)// add member to producer base
        
        await shared_func.registerMarket(owner, market, producer_base)// register market in the producer base

        await producer_base.requestMarketMembership(market.address, { from: producer })
        
        await producer_base.addStoreFront({ from: producer }); 

        await shared_func.openMarketStore(owner, producer, default_store_name, producer_base.address, market)
    });
/**
    it("should allow a member with a store to publish it to given market", async () => {

        const expectedEventResult = {
            storeFrontId : new BN(1),
            storeOwner: producer,
            marketAddress: market.address
        };

        const result_publish = await producer_base.publishStoreFrontToMarket(market.address, 1, { from: producer }); // verify creation of store front

        const result_has_SF = await market.hasStoreFront(producer, producer_base_adr, 1); // verify publication of store front


        expectEvent.inLogs(result_publish.logs, 'LogStoreFrontShared', { storeOwner: expectedEventResult.storeOwner, storeFrontId: expectedEventResult.storeFrontId, 
            market: expectedEventResult.marketAddress }, 'LogStoreFrontShared event account property not emitted or correct, check publishStoreFrontToMarket method');

        assert.equal(result_publish.receipt.status, true, 'was not able to pushe the store front')
        assert.equal(result_has_SF, true, 'store front was not registered to market');
    })
 */
    it("should allow a client with a store to publish it to given market", async () => {

        let default_invoice = new Invoice(producer, client, producer_base.address, 1, 1, 1000, 200, getDateInUnixTimestamp('08/08/2019'))

        const message = web3.utils.sha3(
          default_invoice.seller,
          default_invoice.buyer,
          default_invoice.producerBase,
          default_invoice.storeFrontId,
          default_invoice.productId,
          default_invoice.amount,
          default_invoice.pricePerUnit,
          default_invoice.validUntil
        )

        const signature = await web3.eth.sign(message, producer, function (err, result) { console.log(err, result); });

        console.log(default_invoice.toJSON())
    })
})