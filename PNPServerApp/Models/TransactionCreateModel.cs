using Microsoft.IdentityModel.Tokens;

namespace PNPServerApp.Models
{
    public class TransactionCreateModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal ExchangeRate { get; set; } // USD rate
        public decimal Credit { get; set; } = 0;
        public decimal Debit { get; set; } = 0;
        public DateTime TransactionDate { get; set; }
    }
}
