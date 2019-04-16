using openbankapi.core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.core.IService
{
    public interface ITransactionService
    {
        int GetBalance(string accNo);
        IEnumerable<Transaction> GetTransactions(long toDate, long fromDate, string accNo);
        void GenerateAmountFromTestFaucet(string accountNo);
        string SendTransaction(string to, string from, int amount);
    }
}
