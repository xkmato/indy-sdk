﻿using Hyperledger.Indy.AnonCredsApi;
using Hyperledger.Indy.Test.Util;
using Hyperledger.Indy.Test.WalletTests;
using Hyperledger.Indy.WalletApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Hyperledger.Indy.Test.AnonCredsTests
{
    [TestClass]
    public class ProverGetClaimOfferTest : AnonCredsIntegrationTestBase
    {
        private const string WALLET_NAME = "wallet";
        private const string WALLET_KEY = "walletKey";

        [TestMethod]
        public async Task TestsProverGetClaimOffersWorksForEmptyFilter()
        {
            await InitCommonWallet();

            var claimOffers = ""; //TODO await AnonCreds.ProverGetClaimOffersAsync(commonWallet, "{}");
            var claimOffersArray = JArray.Parse(claimOffers);

            Assert.AreEqual(3, claimOffersArray.Count);
        }

        [TestMethod]
        public async Task TestsProverGetClaimOffersWorksForFilterByIssuer()
        {
            await InitCommonWallet();

            var filter = string.Format("{{\"issuer_did\":\"{0}\"}}", issuerDid);

            var claimOffers = ""; // TODO await AnonCreds.ProverGetClaimOffersAsync(commonWallet, filter);
            var claimOffersArray = JArray.Parse(claimOffers);

            Assert.AreEqual(2, claimOffersArray.Count);

            Assert.IsTrue(claimOffers.Contains(string.Format(claimOfferTemplate, issuerDid, 1)));
            Assert.IsTrue(claimOffers.Contains(string.Format(claimOfferTemplate, issuerDid, 2)));
        }

        [TestMethod] 
        public async Task TestsProverGetClaimOffersWorksForFilterBySchema()
        {
            await InitCommonWallet();

            var filter = string.Format("{{\"schema_seq_no\":{0}}}", 2);

            var claimOffers = ""; // TODO await AnonCreds.ProverGetClaimOffersAsync(commonWallet, filter);
            var claimOffersArray = JArray.Parse(claimOffers);

            Assert.AreEqual(2, claimOffersArray.Count);

            Assert.IsTrue(claimOffers.Contains(string.Format(claimOfferTemplate, issuerDid, 2)));
            Assert.IsTrue(claimOffers.Contains(string.Format(claimOfferTemplate, issuerDid2, 2)));
        }

        [TestMethod] 
        public async Task TestsProverGetClaimOffersWorksForFilterByIssuerAndSchema()
        {
            await InitCommonWallet();

            var filter = string.Format("{{\"issuer_did\":\"{0}\",\"schema_seq_no\":{1}}}", issuerDid, 1);

            var claimOffers = ""; // TODO await AnonCreds.ProverGetClaimOffersAsync(commonWallet, filter);
            var claimOffersArray = JArray.Parse(claimOffers);

            Assert.AreEqual(1, claimOffersArray.Count);

            Assert.IsTrue(claimOffers.Contains(string.Format(claimOfferTemplate, issuerDid, 1)));
        }

        [TestMethod]
        public async Task TestsProverGetClaimOffersWorksForNoResult()
        {
            await InitCommonWallet();

            var filter = string.Format("{{\"schema_seq_no\":{0}}}", 3);

            var claimOffers = ""; // TODO await AnonCreds.ProverGetClaimOffersAsync(commonWallet, filter);
            var claimOffersArray = JArray.Parse(claimOffers);

            Assert.AreEqual(0, claimOffersArray.Count);
        }

        [TestMethod]
        public async Task TestProverGetClaimOffersWorksForInvalidFilterJson()
        {
            await InitCommonWallet();

            var filter = string.Format("{{\"schema_seq_no\":\"{0}\"}}", 1);

            //TODO
            //var ex = await Assert.ThrowsExceptionAsync<InvalidStructureException>(() =>
            //    AnonCreds.ProverGetClaimOffersAsync(commonWallet, filter)

            //);
        }

        [TestMethod]
        public async Task TestGetClaimOffersForPlugged()
        {
            var type = "proverInmem";
            var poolName = "default";
            var walletName = "proverCustomWallet";

            await WalletUtils.CreateWallet(WALLET_NAME, WALLET_KEY);


            string claimOffers = string.Empty;
            Wallet wallet = null;

            var claimOffer = string.Format(claimOfferTemplate, issuerDid, 1);
            var claimOffer2 = string.Format(claimOfferTemplate, issuerDid, 2);
            var claimOffer3 = string.Format(claimOfferTemplate, issuerDid2, 2);

            try
            {
                wallet = await WalletUtils.OpenWallet(WALLET_NAME, WALLET_KEY);

                // TODO await AnonCreds.ProverStoreCredentialOfferAsync(wallet, claimOffer);
                // TODO await AnonCreds.ProverStoreCredentialOfferAsync(wallet, claimOffer2);
                // TODO await AnonCreds.ProverStoreCredentialOfferAsync(wallet, claimOffer3);

                var filter = string.Format("{{\"issuer_did\":\"{0}\"}}", issuerDid);

                // TODO claimOffers = await AnonCreds.ProverGetClaimOffersAsync(wallet, filter);
            }
            finally
            {
                if (wallet != null)
                    await wallet.CloseAsync();

                await WalletUtils.DeleteWallet(WALLET_NAME, WALLET_KEY);
            }

            var claimOffersArray = JArray.Parse(claimOffers);

            Assert.AreEqual(2, claimOffersArray.Count);
            Assert.IsTrue(claimOffers.Contains(claimOffer));
            Assert.IsTrue(claimOffers.Contains(claimOffer2));

        }

    }
}
