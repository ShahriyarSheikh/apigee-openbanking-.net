using openbankapi.core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.repository.IRepository
{
    public interface IAccountRepository
    {
        AccountDetails GetAccount(string accountNumber);
        bool CreateAccount(AccountDetails accountDetails);

        bool DoesAccountExist(string accountNumber);
    }
}
