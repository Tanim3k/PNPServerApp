using PNPServerApp.Models;

namespace PNPServerApp.Interfaces
{
    public interface IUsersService: IBaseService
    {
        public UserModel CreateUser(UserCreateModel request);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
