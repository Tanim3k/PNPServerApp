using PNPServerApp.Models;

namespace PNPServerApp.Interfaces
{
    public interface ITransactionService: IBaseService
    {
        TransactionModel CreateTransaction(TransactionCreateModel transactionCreateModel);
        List<TransactionModel> GetAllTransactions(int? accountId, DateTime? transactionDate);
    }
}
