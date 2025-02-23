using HuynmHE176493.Data.IRepository;
using HuynmHE176493.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HuynmHE176493.Data.Repositories
{
    public class AccountRepository : Repository<SystemAccount>, IAccountRepository
    {
        private readonly FunewsManagementContext _context;

        public AccountRepository(FunewsManagementContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<SystemAccount> GetAll()
        {
            return _context.SystemAccounts.ToList();
        }
        public SystemAccount GetByEmail(string email)
        {
            return _context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == email);
        }

        public IEnumerable<SystemAccount> GetAllAccounts()
        {
            return _context.SystemAccounts.ToList();
        }

        public SystemAccount GetById(int id)
        {
            return _context.SystemAccounts.Find(id);
        }

        public void AddAccount(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
        }

        public void UpdateAccount(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            _context.SaveChanges();
        }

        public void DeleteAccount(int id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                _context.SaveChanges();
            }
        }
        public void ToggleAccountStatus(int id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                account.IsActive = !account.IsActive; // Chuyển trạng thái giữa Active và Inactive
                _context.SaveChanges();
            }
        }

    }
}
