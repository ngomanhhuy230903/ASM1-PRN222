using HuynmHE176493.Data.Models;
using System.Collections.Generic;

namespace HuynmHE176493.Data.IRepository
{
    public interface IAccountRepository : IRepository<SystemAccount>
    {
        SystemAccount GetByEmail(string email);  // Lấy tài khoản theo Email
        IEnumerable<SystemAccount> GetAllAccounts(); // Lấy tất cả tài khoản
        SystemAccount GetById(int id); // Lấy tài khoản theo ID
        void AddAccount(SystemAccount account); // Thêm tài khoản mới
        void UpdateAccount(SystemAccount account); // Cập nhật tài khoản
        void DeleteAccount(int id); // Xóa tài khoản theo ID
        void ToggleAccountStatus(int id);
    }
}
