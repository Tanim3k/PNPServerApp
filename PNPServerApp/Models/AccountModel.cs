namespace PNPServerApp.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IBAN { get; set; }
        public CurrencyCype CurrencyType { get; set; }

        public List<TransactionModel> Transactions { get; set; }
    }

    public enum CurrencyCype
    {
        GBP,
        EUR,
        CHF,
    }
}
