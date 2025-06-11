using AccountManagementAPI.Models;

namespace AccountManagementAPI.Interfaces
{
    public interface IAccountService
    {
        Account Login(string username, string password);
        void Register(Account account);
        List<Account> GetAllAccounts();
    }
}
