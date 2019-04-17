using openbankapi.core.Models;
using System.Collections.Generic;

namespace openbankapi.service.IService
{
    public interface ITransactionService
    {
        int GetBalance(string accNo);
        IEnumerable<Transaction> GetTransactions(string accNo, long toDate = 0, long fromDate = 0);
        string SendTransaction(string to, string from, int amount);
    }
}
