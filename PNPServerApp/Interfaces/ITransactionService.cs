using PNPServerApp.FilterModels;
using PNPServerApp.Models;

namespace PNPServerApp.Interfaces
{
    public interface ITransactionService: IBaseService
    {
        TransactionModel CreateTransaction(TransactionCreateModel transactionCreateModel);
        (List<TransactionModel>, int) GetAllTransactions(TransactionFilterModel transactionFilterModel);
    }
}
