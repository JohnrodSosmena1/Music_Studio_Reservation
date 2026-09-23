using System.Threading.Tasks;

namespace CRM.winforms.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Attempt login with credentials. Returns JWT token string on success.
        /// </summary>
        Task<string?> LoginAsync(string email, string password, bool rememberMe = false);
    }
}
