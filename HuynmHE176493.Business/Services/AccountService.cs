using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using HuynmHE176493.Business.IService;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace HuynmHE176493.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration; // ✅ Đọc từ appsettings.json

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public IEnumerable<SystemAccount> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }

        public SystemAccount GetAccountById(int id)
        {
            return _accountRepository.GetById(id);
        }

        public void CreateAccount(SystemAccount account)
        {
            _accountRepository.AddAccount(account);
        }

        public void UpdateAccount(SystemAccount account)
        {
            _accountRepository.UpdateAccount(account);
        }

        public void DeleteAccount(int id)
        {
            _accountRepository.DeleteAccount(id);
        }
        public void ToggleAccountStatus(int id)
        {
            _accountRepository.ToggleAccountStatus(id);
        }
        // ✅ Xác thực người dùng
        public SystemAccount? ValidateUser(string email, string password)
        {
            return _accountRepository.GetAll()
                .FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);
        }
        public bool IsDefaultAdmin(string email, string password)
        {
            string adminEmail = _configuration["AdminAccount:Email"];
            string adminPassword = _configuration["AdminAccount:Password"];

            return email == adminEmail && password == adminPassword;
        }
    }
}
