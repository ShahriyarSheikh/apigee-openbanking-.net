namespace openbankapi.service.Models
{
    public class AccountDetails
    {
        public string name { get; set; }
        public Account Account { get; set; }
        public string AccountId { get; set; }
        public Amount Amount { get; set; }
        public string CreditDebitIndicator { get; set; }
        public CreditLine CreditLine { get; set; }
        public string Currency { get; set; }
        public string CustomerId { get; set; }
        public long DateTime { get; set; }
        public string Nickname { get; set; }
        public Servicer Servicer { get; set; }
        public string Type { get; set; }
    }


    public class Account
    {
        public string SecondaryIdentification { get; set; }
        public string Identification { get; set; }
        public string SchemeName { get; set; }
        public string Name { get; set; }
    }

    public class CreditLine
    {
        public string Type { get; set; }
        public Amount Amount { get; set; }
        public bool Included { get; set; }
    }

    public class Servicer
    {
        public string Identification { get; set; }
        public string SchemeName { get; set; }
    }


}
