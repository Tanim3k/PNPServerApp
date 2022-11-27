using PNPServerApp.Interfaces;
using PNPServerApp.Models;

namespace PNPServerApp.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, httpClientFactory)
        {

        }

        public TransactionModel CreateTransaction(TransactionCreateModel transactionCreateModel)
        {
            var user = GetCurrentUser();

            if (user == null) return null;

            var account = user.Accounts.FirstOrDefault(m => m.Id == transactionCreateModel.AccountId);

            if (account == null) return null;

            var transactionModel = new TransactionModel
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Credit = transactionCreateModel.Credit,
                Debit = transactionCreateModel.Debit,
                TransactionDate = transactionCreateModel.TransactionDate
            };

            account.Transactions.Add(transactionModel);

            return transactionModel;
        }

        public List<TransactionModel> GetAllTransactions(int? accountId, DateTime? transactionDate)
        {
            var user = GetCurrentUser();

            if (user == null) return null;

            var transactions = new List<TransactionModel>();

            if (accountId != null && accountId != 0)
            {
                var account = user.Accounts.FirstOrDefault(m => m.Id == accountId);

                if (account != null)
                {
                    transactions = account.Transactions;
                }
                
            } else
            {
                foreach (var account in user.Accounts)
                {
                    transactions.AddRange(account.Transactions);
                }
            }

            if (transactionDate != null)
            {
                transactions = transactions.Where(m => m.TransactionDate == transactionDate).ToList();
            }

            return transactions;
        }
    }
}
