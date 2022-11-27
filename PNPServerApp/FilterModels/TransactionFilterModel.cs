namespace PNPServerApp.FilterModels
{
    public class TransactionFilterModel
    {
        public Guid? TransactionId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? ReturnTotalCount { get; set; }

        public int? Page { get; set; }
        public int? Index { get; set; }
    }
}
