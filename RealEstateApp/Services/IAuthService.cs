using RealEstateApp.Models;

namespace RealEstateApp.Services
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(string username, string password);
        Task LogoutAsync();
    }
}
