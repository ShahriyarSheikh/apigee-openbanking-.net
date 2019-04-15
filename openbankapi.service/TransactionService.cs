using openbankapi.service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace openbankapi.service
{
    public class TransactionService
    {
        public static List<AccountDetails> accounts = new List<AccountDetails>();
        public static List<Transaction> transactions = new List<Transaction>();

        public TransactionService()
        {
            GenerateMockModelAccountDetails();
            GenerateMockModelTransaction();
        }
        public IEnumerable<Transaction> GetTransactions(long toDate, long fromDate,string accNo)
        {
            return transactions.Where(x => x.BookingDateTime >= toDate && x.BookingDateTime <= fromDate);
        }


        public int GetBalance(string accNo)
        {
            if (!EnsureAccount(accNo))
            {
                return 0;
            }
            var balanceDebit = transactions.Where(x => x.AccountId == accNo && x.CreditDebitIndicator == "Debit").Sum(x => x.Amount.Value);
            var balanceCredit = transactions.Where(x => x.AccountId == accNo && x.CreditDebitIndicator == "Credit").Sum(x => x.Amount.Value);

            return (balanceCredit - balanceDebit);
        }

        public string SendTransaction(string to, string from, int amount)
        {
            //Two Mock Users with account numbers 10000 and 10001
            if (!EnsureAccount(to) && !EnsureAccount(from))
            {
                return "No Account Exists";
            }

            if (GetBalance(from) < amount)
            {
                return "Insufficient Balance";
            }
            GenerateMockModelTransactions(to, from,amount, DateTime.UtcNow.Ticks);

            return "Success";
        }

        private bool EnsureAccount(string accNo)
        {
            return accNo == "100" || accNo == "101" ? true : false;
        }

        private void GenerateMockModelAccountDetails()
        {

            for (int i = 0; i < 2; i++)
            {
                var accDetails = new AccountDetails()
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
                    AccountId = "10"+i,
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
                accounts.Add(accDetails);
            }
        }

        private void GenerateMockModelTransactions(string accountNoTo,string accountNoFrom, int amount, long dateTimeOffset) {
            var transactionDetailsReceiver = new Transaction
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
            var transactionDetailsSender = new Transaction
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
            transactions.Add(transactionDetailsReceiver);
            transactions.Add(transactionDetailsSender);
        }

        private void GenerateMockModelTransaction() {
            for (int i = 0; i < 2; i++) {
                var transactionDetails = new Transaction
                {
                    AccountId = "10"+i,
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
                    TransactionReference = "MockTransaction",
                    ValueDateTime = 21342142123
                };
                transactions.Add(transactionDetails);
            }
        }
    }
}
