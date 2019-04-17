using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using openbankapi.core.Models;
using openbankapi.repository.IRepository;
using openbankapi.service;
using openbankapi.service.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.nunit.tests
{
    [TestFixture]
    public class Accounts
    {
        //Mock<ITransactionService> txService;
        Mock<IAccountRepository> accountRepository;
        string account1;
        string account2;
        private AccountService accountService;

        [SetUp]
        public void Setup()
        {
            accountRepository = new Mock<IAccountRepository>();
            account1 = "100";
            account2 = "101";
            accountService = new AccountService(accountRepository.Object);
        }


        [Test]
        public void Provided_AccountNumber_Should_Return_AccountDetails()
        {
            AccountDetails accountDetails = Builder<AccountDetails>.CreateNew().With(x => x.AccountId, account1).Build();
            accountRepository.Setup(x => x.GetAccount(account1)).Returns(accountDetails);

            AccountDetails actualAccountDetails = accountService.GetAccount(account1);

            Assert.AreEqual(accountDetails, actualAccountDetails);
        }

        [Test]
        public void Provided_AccountNumber_Should_Check_IfExists() {
            accountRepository.Setup(x => x.DoesAccountExist(account1)).Returns(true);
            accountRepository.Setup(x => x.DoesAccountExist(account2)).Returns(false);

            Assert.IsTrue(accountService.DoesAccountExist(account1));
            Assert.IsFalse(accountService.DoesAccountExist(account2));

        }


    }

}
