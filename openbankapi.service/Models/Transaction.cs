namespace openbankapi.service.Models
{
    public class Transaction
    {
        public string AccountId { get; set; }
        public Amount Amount { get; set; }
        public Balance Balance { get; set; }
        public BankTransactionCode BankTransactionCode { get; set; }
        public long BookingDateTime { get; set; }
        public string CreditDebitIndicator { get; set; }
        public ProprietaryBankTransactionCode ProprietaryBankTransactionCode { get; set; }
        public string Status { get; set; }
        public string TransactionInformation { get; set; }
        public string TransactionReference { get; set; }
        public long ValueDateTime { get; set; }
    }


    public class Balance
    {
        public string Type { get; set; }
        public Amount Amount { get; set; }
        public string CreditDebitIndicator { get; set; }
    }

    public class BankTransactionCode
    {
        public string SubCode { get; set; }
        public string Code { get; set; }
    }

    public class ProprietaryBankTransactionCode
    {
        public string Issuer { get; set; }
        public string Code { get; set; }
    }
}

