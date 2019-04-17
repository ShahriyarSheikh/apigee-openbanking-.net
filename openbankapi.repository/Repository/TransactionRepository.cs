using openbankapi.core.Models;
using openbankapi.repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace openbankapi.repository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public int GetBalance(string accNo)
        {
            return 0;
        }

        public IEnumerable<Transaction> GetTransactions(string accNo, long toDate = 0, long fromDate = 0)
        {
            IEnumerable<Transaction> transactions = null;
            return transactions;
        }

        public string SendTransaction(string to, string from, int amount)
        {
            return "Success";
        }
    }
}
