using RealEstateApp.Views;

namespace RealEstateApp;

public partial class AppShell : Shell
{
    private bool _isLoggedIn;

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            if (_isLoggedIn == value)
                return;

            _isLoggedIn = value;

            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(IsLoggedOut));
        }
    }

    public bool IsLoggedOut => !IsLoggedIn;
    public AppShell()
	{
		InitializeComponent();

        BindingContext = this;

        Routing.RegisterRoute(nameof(PropertyDetailPage), typeof(PropertyDetailPage));
        Routing.RegisterRoute(nameof(AddEditPropertyPage), typeof(AddEditPropertyPage));
        Routing.RegisterRoute(nameof(CompassPage), typeof(CompassPage));
        Routing.RegisterRoute(nameof(HeightCalculatorPage), typeof(HeightCalculatorPage));

        Routing.RegisterRoute("loginpage", typeof(LoginPage));

        CheckLoginState();
    }
    private async void CheckLoginState()
    {
        var accessToken = await SecureStorage.GetAsync("access_token");

        IsLoggedIn = !string.IsNullOrEmpty(accessToken);
    }

    public void UpdateLoginState(bool isLoggedIn)
    {
        IsLoggedIn = isLoggedIn;
    }


    protected override async void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        if (args.Target.Location.OriginalString == "//logout")
        {
            args.Cancel();

            await GoToAsync("loginpage?logout=true");
        }
    }
}
