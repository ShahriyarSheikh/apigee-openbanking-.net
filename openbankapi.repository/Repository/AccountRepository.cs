using openbankapi.core.Models;
using openbankapi.repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public bool CreateAccount(AccountDetails accountDetails)
        {
            return true;
        }

        public bool DoesAccountExist(string accountNumber)
        {
            return true;
        }

        public AccountDetails GetAccount(string accountNumber)
        {
            return null;
        }
    }
}
