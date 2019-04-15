using NUnit.Framework;
using openbankapi.service;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.nunit.tests
{
    [TestFixture]
    public class Transactions
    {
        TransactionService txService;
        string account1;
        string account2;
        [SetUp]
        public void Setup()
        {
            txService = new TransactionService();
            account1 = "100";
            account2 = "101";
        }

        [Test]
        public void Provided_Account_Details_Should_Send_Transaction() {
            //var txs = txService.GetTransactions(DateTime.UtcNow.Ticks, DateTime.UtcNow.Ticks);

            var sendTxResponse = txService.SendTransaction(account2, account1, 50);
            Assert.AreEqual("Success", sendTxResponse);

        }

        [Test]
        public void Provided_Account_Details_Should_Get_Balance() {
            var expectedBalance = 1000;

            var actualBalance = txService.GetBalance(account1);

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Test]
        public void Provided_Account_Should_Send_And_Check_Remaining_Balance() {
            var expectedBalace = (txService.GetBalance(account1) - 50);
            var sendTxResponse = txService.SendTransaction(account2, account1, 50);
            Assert.AreEqual("Success", sendTxResponse);
            var actualBalance = txService.GetBalance(account1);
            Assert.AreEqual(expectedBalace, actualBalance);

        }

    }
}
