using RealEstateApp.Services;

namespace RealEstateApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IAuthService authService;

        public LoginPageViewModel(IAuthService authService)
        {
            this.authService = authService;
        }

        #region Properties

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        #endregion

        #region Login

        private Command loginCommand;
        public Command LoginCommand => loginCommand ??= new Command(async () => await OnLoginAsync());

        private async Task OnLoginAsync()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter a username and password.";
                return;
            }

            var result = await authService.LoginAsync(
                Username,
                Password);

            if (!result.Succeeded)
            {
                ErrorMessage = "Invalid username or password.";
                return;
            }

            await SecureStorage.SetAsync(
                "access_token",
                result.AccessToken);

            await SecureStorage.SetAsync(
                "refresh_token",
                result.RefreshToken);

            if (Shell.Current is AppShell appShell)
            {
                appShell.UpdateLoginState(true);
            }

            await Shell.Current.GoToAsync("//propertylist");
        }

        #endregion

        #region Logout

        private async Task Logout()
        {
            await authService.LogoutAsync();

            if (Shell.Current is AppShell appShell)
            {
                appShell.UpdateLoginState(false);
            }

            await Shell.Current.GoToAsync("//propertylist");
        }

        #endregion

        #region Query Parameters

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("logout", out var logoutValue))
            {
                if (bool.TryParse(
                    logoutValue?.ToString(),
                    out bool logout) && logout)
                {
                    await Logout();
                }
            }
        }

        #endregion
    }
}
