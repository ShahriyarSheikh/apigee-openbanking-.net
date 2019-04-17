using openbankapi.core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.repository.IRepository
{
    public interface ITransactionRepository
    {
        int GetBalance(string accNo);
        IEnumerable<Transaction> GetTransactions(string accNo, long toDate = 0, long fromDate = 0);
        string SendTransaction(string to, string from, int amount);
    }
}
