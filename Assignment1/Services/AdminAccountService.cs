using Microsoft.Extensions.Configuration;
using Assignment1.Models;
using Assignment1.Services;

public class AdminAccountService : IAdminAccountService
{
    private readonly IConfiguration _configuration;
   
    public AdminAccountService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SystemAccount GetAdminAccount()
    {
        return new SystemAccount
        {
            AccountId = 1,
            AccountName = "Admin",
            AccountEmail = _configuration["AdminAccount:Email"],
            AccountPassword = _configuration["AdminAccount:Password"],
            AccountRole = 1
        };
    }
}
