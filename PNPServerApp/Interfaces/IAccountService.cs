using PNPServerApp.Models;

namespace PNPServerApp.Interfaces
{
    public interface IAccountService : IBaseService
    {
        public AccountModel CreateAccount(AccountCreateModel accountCreateModel);
        public AccountModel UpdateAccount(AccountModel accountModel);
        public bool DeleteAccount(AccountModel accountModel);

        public AccountModel GetAccount(string accountId);
        public AccountModel GetAccountByUserId(string userId);
        //AccountModel GetAccountByName(string accountName);


    }
}
