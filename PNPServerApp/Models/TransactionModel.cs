using Microsoft.IdentityModel.Tokens;

namespace PNPServerApp.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public decimal Credit { get; set; } = 0;
        public decimal Debit { get; set; } = 0;
        public DateTime TransactionDate { get; set; }
    }
}
