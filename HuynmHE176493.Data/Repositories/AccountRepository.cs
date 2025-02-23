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
            if (!_context.SystemAccounts.Any(a => a.AccountEmail == account.AccountEmail)) // Tránh trùng email
            {
                var newAccount = new SystemAccount
                {
                    AccountName = account.AccountName,
                    AccountEmail = account.AccountEmail,
                    AccountRole = account.AccountRole,
                    AccountPassword = account.AccountPassword, // Nếu cần
                    IsActive = account.IsActive
                };
                _context.SystemAccounts.Add(newAccount);
                _context.SaveChanges();
            }
        }

        public void UpdateAccount(SystemAccount account)
        {
            var existingAccount = _context.SystemAccounts.Find(account.AccountId);
            if (existingAccount != null)
            {
                // ✅ Chỉ cập nhật những giá trị được truyền vào
                if (!string.IsNullOrEmpty(account.AccountName))
                    existingAccount.AccountName = account.AccountName;

                if (!string.IsNullOrEmpty(account.AccountEmail))
                    existingAccount.AccountEmail = account.AccountEmail;

                if (account.AccountRole != 0) // Giả sử 0 không phải giá trị hợp lệ
                    existingAccount.AccountRole = account.AccountRole;

                existingAccount.IsActive = account.IsActive; // Checkbox mặc định có giá trị true/false

                _context.SaveChanges();
            }
        }


        public void DeleteAccount(int id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                // Nếu muốn xóa mềm (giữ dữ liệu nhưng ẩn tài khoản)
                // account.IsDeleted = true; 
                // _context.SystemAccounts.Update(account);

                // Nếu muốn xóa hẳn
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
