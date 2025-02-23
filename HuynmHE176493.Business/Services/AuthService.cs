using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;

namespace HuynmHE176493.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public SystemAccount Authenticate(string email, string password)
        {
            var account = _accountRepository.GetByEmail(email);
            if (account == null || account.AccountPassword != password)
            {
                return null;
            }
            return account;
        }
    }
}
