using AccountManagementAPI.Interfaces;
using AccountManagementAPI.Models;

namespace AccountManagementAPI.Services
{
    private readonly List<Account> accounts = new();
    public class AccountService : IAccountService
    {


        public Account Login(string username, string password)
        {
            return accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
        }

        public void Register(Account account)
        {
            accounts.Add(account);
        }

        public List<Account> GetAllAccounts()
        {
            return accounts;
        }
    }
}
