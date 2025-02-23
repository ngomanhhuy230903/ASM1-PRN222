using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Business.IService
{
    public interface IAccountService
    {
        IEnumerable<SystemAccount> GetAllAccounts();
        SystemAccount GetAccountById(int id);
        void CreateAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(int id);

        void ToggleAccountStatus(int id);
        SystemAccount? ValidateUser(string email, string password);
        bool IsDefaultAdmin(string email, string password);
    }
}
