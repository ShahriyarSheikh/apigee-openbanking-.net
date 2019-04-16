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
            txService.GenerateAmountFromTestFaucet(account1);
            txService.GenerateAmountFromTestFaucet(account2);
        }

        [Test]
        public void Provided_Account_Details_Should_Send_Transaction() {
            int amountToSend = 50;
            int expectedBalance = (txService.GetBalance(account1) - amountToSend);
            string sendTxResponse = txService.SendTransaction(account2, account1, amountToSend);
            Assert.AreEqual("Success", sendTxResponse);
            int actualBalance = (txService.GetBalance(account1));
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Test]
        public void Provided_Account_Details_Should_Get_Balance() {
            var expectedBalance = 1000;

            var actualBalance = txService.GetBalance(account1);

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Test]
        public void Provided_Account_Should_Send_And_Check_Remaining_Balance() {
            int expectedBalace = (txService.GetBalance(account1) - 50);
            string sendTxResponse = txService.SendTransaction(account2, account1, 50);
            Assert.AreEqual("Success", sendTxResponse);
            int actualBalance = txService.GetBalance(account1);
            Assert.AreEqual(expectedBalace, actualBalance);

        }

    }
}
