using RealEstateApp.Models;

namespace RealEstateApp.Services
{
    public class MockAuthService : IAuthService
    {
        private const string ValidUsername = "admin";
        private const string ValidPassword = "admin123";

        public async Task<LoginResult> LoginAsync(
            string username,
            string password)
        {
            await Task.Delay(200);

            if (username != ValidUsername || password != ValidPassword)
            {
                return new LoginResult
                {
                    Succeeded = false
                };
            }

            return new LoginResult
            {
                Succeeded = true,
                AccessToken = GenerateToken(),
                RefreshToken = GenerateToken()
            };
        }

        public Task LogoutAsync()
        {
            SecureStorage.Remove("access_token");
            SecureStorage.Remove("refresh_token");

            return Task.CompletedTask;
        }

        private string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }
    }
}
