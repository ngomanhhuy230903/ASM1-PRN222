using HuynmHE176493.Data.Models;

namespace HuynmHE176493.Business.IService
{
    public interface IAuthService
    {
        SystemAccount Authenticate(string email, string password);
    }
}
