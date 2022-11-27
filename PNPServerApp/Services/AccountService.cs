using PNPServerApp.Interfaces;
using PNPServerApp.Models;

namespace PNPServerApp.Services
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
            : base(httpContextAccessor, httpClientFactory)
        {

        }

        public AccountModel CreateAccount(AccountCreateModel accountCreateModel)
        {
            var user = GetCurrentUser();


            if (user == null) return null;

            var accountModel = new AccountModel
            {
                Id = user.Accounts.Count + 1,
                UserId = user.Id,
                IBAN = accountCreateModel.IBAN,
                CurrencyType = accountCreateModel.CurrencyType,
                Transactions = new List<TransactionModel>()
            };

            user.Accounts.Add(accountModel);

            return accountModel;
        }

        public bool DeleteAccount(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }

        public AccountModel GetAccount(string accountId)
        {
            throw new NotImplementedException();
        }

        public AccountModel GetAccountByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public AccountModel UpdateAccount(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }
    }
}
