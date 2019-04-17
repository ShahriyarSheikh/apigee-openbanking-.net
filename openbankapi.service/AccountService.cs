using openbankapi.core.Models;
using openbankapi.repository.IRepository;
using openbankapi.service.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public bool CreateAccount(AccountDetails accountDetails)
        {
            return _accountRepository.CreateAccount(accountDetails);
        }

        public AccountDetails GetAccount(string accountNumber)
        {
            return _accountRepository.GetAccount(accountNumber);
        }

        public bool DoesAccountExist(string accountNumber)
        {
            return _accountRepository.DoesAccountExist(accountNumber);
        }
    }
}
