using PNPServerApp.Models;

namespace PNPServerApp.Interfaces
{
    public interface IBaseService
    {
        public UserModel? GetUserByUserName(string userName);
        public UserModel? GetCurrentUser();
    }
}
