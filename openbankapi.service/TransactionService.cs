using openbankapi.service.IService;
using openbankapi.core.Models;
using openbankapi.models.Enums;
using openbankapi.repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace openbankapi.service
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IAccountService accountService, ITransactionRepository transactionRepository)
        {
            _accountService = accountService;
            _transactionRepository = transactionRepository;
        }
        public IEnumerable<Transaction> GetTransactions(string accNo, long toDate = 0, long fromDate = 0)
        {
            return _transactionRepository.GetTransactions(accNo, toDate, fromDate);
        }

        public int GetBalance(string accNo)
        {
            if (!_accountService.DoesAccountExist(accNo))
                return 0;

            IEnumerable<Transaction> transactions = _transactionRepository.GetTransactions(accNo);
            int balanceDebit = transactions.Where(x => x.AccountId == accNo && x.CreditDebitIndicator == TransactionType.Debit.ToString()).Sum(x => x.Amount.Value);
            int balanceCredit = transactions.Where(x => x.AccountId == accNo && x.CreditDebitIndicator == TransactionType.Credit.ToString()).Sum(x => x.Amount.Value);

            return (balanceCredit - balanceDebit);
        }

        public string SendTransaction(string to, string from, int amount)
        {

            if (GetBalance(from) < amount)
            {
                return "Insufficient Balance";
            }

            return _transactionRepository.SendTransaction(to, from, amount);
        }




    }
}
