using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using openbankapi.core.Models;
using openbankapi.repository.IRepository;
using openbankapi.service;
using openbankapi.service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace openbankapi.nunit.tests
{
    [TestFixture]
    public class Transactions
    {
        Mock<ITransactionRepository> transactionRepository;
        Mock<IAccountService> accountService;
        string account1;
        string account2;
        TransactionService txService;

        [SetUp]
        public void Setup()
        {
            account1 = "100";
            account2 = "101";
            transactionRepository = new Mock<ITransactionRepository>();
            accountService = new Mock<IAccountService>();
            txService = new TransactionService(accountService.Object, transactionRepository.Object);
        }

        [Ignore("Will be implemented after dbcontext")]
        [Test]
        public void Provided_Account_Details_Should_Send_Transaction()
        {
            //int amountToSend = 50;
            //int expectedBalance = (txService.GetBalance(account1) - amountToSend);
            //string sendTxResponse = txService.SendTransaction(account2, account1, amountToSend);
            //Assert.AreEqual("Success", sendTxResponse);
            //int actualBalance = (txService.GetBalance(account1));
            //Assert.AreEqual(expectedBalance, actualBalance);
            Assert.Pass();
        }


        [Test]
        public void Provided_Date_Should_Retrieve_Related_Transaction() {

            long date1 = 636897336590000000;
            long date2 = 636905115370000000;

            Transaction transaction1 = Builder<Transaction>.CreateNew()
                .With(x => x.AccountId, account1)
                .With(x => x.BookingDateTime, date1)
                .Build();
            Transaction transaction2 = Builder<Transaction>.CreateNew()
                .With(x => x.AccountId, account1)
                .With(x => x.BookingDateTime, date2)
                .Build();

            transactionRepository.Setup(x => x.GetTransactions(account1, date1, date2)).Returns(new List<Transaction> { transaction1, transaction2 });

            var txs = (txService.GetTransactions(account1, date1, date2)).ToList();

            Assert.True(txs.Count > 1,"Date time is incorrect");


        }

        [Test]
        public void Provided_Account_Details_Should_Get_Balance()
        {
            int expectedBalance = 2;
            Amount amount = Builder<Amount>.CreateNew().Build();
            Transaction transactions1 = Builder<Transaction>.CreateNew()
                .With(x => x.Amount, amount)
                .With(x=>x.AccountId,account1)
                .With(x=>x.CreditDebitIndicator,"Credit")
                .Build();
            Transaction transactions2 = Builder<Transaction>.CreateNew()
                .With(x => x.Amount, amount)
                .With(x => x.AccountId, account1)
                .With(x => x.CreditDebitIndicator, "Credit")
                .Build();

            accountService.Setup(x => x.DoesAccountExist(account1)).Returns(true);
            transactionRepository.Setup(x => x.GetTransactions(account1, 0, 0)).Returns(new List<Transaction> { transactions1,transactions2});

            int actualBalance = txService.GetBalance(account1);

            //Assert.Pass();
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        [Ignore("Will be implemented after dbcontext")]
        [Test]
        public void Provided_Account_Should_Send_And_Check_Remaining_Balance()
        {
            //int expectedBalace = (txService.GetBalance(account1) - 50);
            //string sendTxResponse = txService.SendTransaction(account2, account1, 50);
            //Assert.AreEqual("Success", sendTxResponse);
            //int actualBalance = txService.GetBalance(account1);
            //Assert.AreEqual(expectedBalace, actualBalance);
            Assert.Pass();

        }


        private void GenerateAccounts()
        {

            for (int i = 0; i < 2; i++)
            {
                AccountDetails accDetails = new AccountDetails()
                {
                    Amount = new Amount
                    {
                        Currency = "USD",
                        Value = 12312
                    },
                    Currency = "USD",
                    Account = new Account
                    {
                        Identification = "10" + i,
                        SecondaryIdentification = "100bankaccount",
                        Name = "rairupanaccount1",
                        SchemeName = "IBAN"
                    },
                    AccountId = "10" + i,
                    CreditDebitIndicator = "Debit",
                    CreditLine = new CreditLine
                    {
                        Amount = new Amount
                        {
                            Value = 1000,
                            Currency = "USD"
                        },
                        Included = true,
                        Type = "Pre-Aggreed"
                    },
                    Type = "ClosingAvailable",
                    CustomerId = "10" + i,
                    DateTime = 15012000000,
                    name = "123412",
                    Nickname = "nicky10" + i,
                    Servicer = new Servicer
                    {
                        Identification = "OBBANK123",
                        SchemeName = "UKSortCode"
                    }
                };
            }
        }

        private void ProcessTransaction(string accountNoTo, string accountNoFrom, int amount, long dateTimeOffset)
        {
            Transaction transactionDetailsReceiver = new Transaction
            {
                AccountId = accountNoTo,
                Amount = new Amount
                {
                    Currency = "USD",
                    Value = amount
                },
                Balance = new Balance
                {
                    Amount = new Amount
                    {
                        Currency = "USD",
                        Value = amount
                    },
                    CreditDebitIndicator = "Debit",
                    Type = "InterimBooked"
                },
                BankTransactionCode = new BankTransactionCode
                {
                    Code = "ReceivedCreditTransfer",
                    SubCode = "DomesticCreditTransfer"
                },
                BookingDateTime = dateTimeOffset,
                CreditDebitIndicator = "Credit",
                ProprietaryBankTransactionCode = new ProprietaryBankTransactionCode
                {
                    Code = "Transfer",
                    Issuer = "AlphaBank"
                },
                Status = "Booked",
                TransactionInformation = "Cash from aburey",
                TransactionReference = accountNoFrom,
                ValueDateTime = 21342142123
            };
            Transaction transactionDetailsSender = new Transaction
            {
                AccountId = accountNoFrom,
                Amount = new Amount
                {
                    Currency = "USD",
                    Value = amount
                },
                Balance = new Balance
                {
                    Amount = new Amount
                    {
                        Currency = "USD",
                        Value = amount
                    },
                    CreditDebitIndicator = "Debit",
                    Type = "InterimBooked"
                },
                BankTransactionCode = new BankTransactionCode
                {
                    Code = "SentCreditTransfer",
                    SubCode = "DomesticCreditTransfer"
                },
                BookingDateTime = dateTimeOffset,
                CreditDebitIndicator = "Debit",
                ProprietaryBankTransactionCode = new ProprietaryBankTransactionCode
                {
                    Code = "Transfer",
                    Issuer = "AlphaBank"
                },
                Status = "Booked",
                TransactionInformation = "Cash from aburey",
                TransactionReference = accountNoFrom,
                ValueDateTime = 21342142123
            };
        }

        public void GenerateAmountFromTestFaucet(string accountNo)
        {
            Transaction transactionDetails = new Transaction
            {
                AccountId = accountNo,
                Amount = new Amount
                {
                    Currency = "USD",
                    Value = 1000
                },
                Balance = new Balance
                {
                    Amount = new Amount
                    {
                        Currency = "USD",
                        Value = 1000
                    },
                    CreditDebitIndicator = "Credit",
                    Type = "InterimBooked"
                },
                BankTransactionCode = new BankTransactionCode
                {
                    Code = "ReceivedCreditTransfer",
                    SubCode = "DomesticCreditTransfer"
                },
                BookingDateTime = DateTime.UtcNow.AddDays(-1).Ticks,
                CreditDebitIndicator = "Credit",
                ProprietaryBankTransactionCode = new ProprietaryBankTransactionCode
                {
                    Code = "Transfer",
                    Issuer = "AlphaBank"
                },
                Status = "Booked",
                TransactionInformation = "Cash from aburey",
                TransactionReference = "FaucetTransaction",
                ValueDateTime = 21342142123
            };
        }

    }
}
