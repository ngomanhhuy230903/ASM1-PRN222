using Assignment1.Models;
using HuynmHE176493.Data.DataAccess;
using System.Linq;

namespace HuynmHE176493.Data.DataAccess
{
    public class SystemAccountRepository : Repository<SystemAccount>, ISystemAccountRepository
    {
        private readonly FunewsManagementContext _context;

        public SystemAccountRepository(FunewsManagementContext context) : base(context)
        {
            _context = context;
        }

        public SystemAccount GetByEmail(string email)
        {
            return _context.SystemAccounts.FirstOrDefault(a => a.AccountEmail == email);
        }
    }
}
