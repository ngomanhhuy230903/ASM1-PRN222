using Assignment1.Models;
using System.Collections.Generic;
using System.Linq;

public interface IUserService
{
    SystemAccount AuthenticateUser(string email, string password);
}

public class UserService : IUserService
{
    private readonly List<SystemAccount> _users;

    public UserService(string adminEmail, string adminPassword)
    {
        _users = new List<SystemAccount>
        {
            new SystemAccount { AccountId = 1, AccountName = "Admin", AccountEmail = adminEmail, AccountRole = 1, AccountPassword = adminPassword }
        };
    }

    public SystemAccount AuthenticateUser(string email, string password)
    {
        return _users.FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);
    }
}
