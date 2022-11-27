using PNPServerApp.FilterModels;
using PNPServerApp.Interfaces;
using PNPServerApp.Models;
using System.Linq;

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

        public (List<TransactionModel>, int) GetAllTransactions(TransactionFilterModel transactionFilterModel)
        {
            var user = GetCurrentUser();

            if (user == null) return (null, 0);

            var transactions = new List<TransactionModel>();

            foreach (var account in user.Accounts)
            {
                transactions.AddRange(account.Transactions);
            }

            if (transactionFilterModel.AccountId != null && transactionFilterModel.AccountId != 0)
            {
                transactions = transactions.Where(m => m.AccountId == transactionFilterModel.AccountId).ToList();
            }

            if (transactionFilterModel.TransactionDate != null)
            {
                transactions = transactions.Where(m => m.TransactionDate == transactionFilterModel.TransactionDate).ToList();
            }

            if (transactionFilterModel.Page != null && transactionFilterModel.Index != null)
            {
                transactions = transactions.Skip(transactionFilterModel.Index ?? 0).Take(transactionFilterModel.Page ?? 0).ToList();
            }

            return (transactions, transactions.Count);
        }
    }
}
