using Assignment1.Models;
using HuynmHE176493.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuynmHE176493.Data.DataAccess
{
    public interface ISystemAccountRepository : IRepository<SystemAccount>
    {
        SystemAccount GetByEmail(string email);
    }
}
