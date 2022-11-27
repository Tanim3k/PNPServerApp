namespace PNPServerApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Byte[] PasswordHash { get; set; }
        public Byte[] PasswordSalt { get; set; }

        public List<AccountModel> Accounts { get; set; }
    }
}
