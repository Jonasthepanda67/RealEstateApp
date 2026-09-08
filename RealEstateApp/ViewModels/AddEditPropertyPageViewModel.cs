using RealEstateApp.Models;
using RealEstateApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RealEstateApp.ViewModels;

[QueryProperty(nameof(Mode), "mode")]
[QueryProperty(nameof(Property), "MyProperty")]
public class AddEditPropertyPageViewModel : BaseViewModel
{
    private readonly IPropertyService service;

    public AddEditPropertyPageViewModel(IPropertyService service)
    {
        this.service = service;
        Agents = new ObservableCollection<Agent>(service.GetAgents());


        Battery.BatteryInfoChanged += OnBatteryInfoChanged;
        Battery.EnergySaverStatusChanged += OnEnergySaverStatusChanged;
        Connectivity.ConnectivityChanged += OnConnectivityChanged;
        _ = CheckConnection();
        _ = BatteryStatusCheck();
    }

    public string Mode { get; set; }

    #region Properties
    public ObservableCollection<Agent> Agents { get; }

    private Property _property;
    public Property Property
    {
        get => _property;
        set
        {
            SetProperty(ref _property, value);
            Title = Mode == "newproperty" ? "Add Property" : "Edit Property";

            if (_property?.AgentId != null)
            {
                SelectedAgent = Agents.FirstOrDefault(
                    x => x.Id == _property.AgentId);
            }
        }
    }

    private Agent _selectedAgent;
    public Agent SelectedAgent
    {
        get => _selectedAgent;
        set
        {
            if (SetProperty(ref _selectedAgent, value))
            {
                if (Property != null)
                {
                    Property.AgentId = _selectedAgent?.Id;
                }
            }
        }
    }

    string statusMessage;
    public string StatusMessage
    {
        get { return statusMessage; }
        set { SetProperty(ref statusMessage, value); }
    }

    Color statusColor;
    public Color StatusColor
    {
        get { return statusColor; }
        set { SetProperty(ref statusColor, value); }
    }
    #endregion

    #region Saving

    private Command savePropertyCommand;
    public ICommand SavePropertyCommand => savePropertyCommand ??= new Command(async () => await SaveProperty());
    private async Task SaveProperty()
    {
        if (IsValid() == false)
        {
            StatusMessage = "Please fill in all required fields";
            StatusColor = Colors.Red;
            await Vibrate();
        }
        else
        {
            service.SaveProperty(Property);
            PerformHapticFeedback();
            await Shell.Current.GoToAsync("///propertylist");
        }
    }
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Property.Address)
            || Property.Beds == null
            || Property.Price == null
            || Property.AgentId == null)
            return false;
        return true;
    }

    private Command cancelSaveCommand;
    public ICommand CancelSaveCommand => cancelSaveCommand ??= new Command(async () => { await CancelVibration(); await Shell.Current.GoToAsync(".."); });

    #endregion

    #region Location

    public bool IsGeocodeAddressButtonVisible { get; set; }
    private Command getCurrentLocationCommand;
    public ICommand GetCurrentLocationCommand =>
        getCurrentLocationCommand ??= new Command(
            async () => await GetCurrentLocation());

    private async Task GetCurrentLocation()
    {
        try
        {
            var request = new GeolocationRequest(
                GeolocationAccuracy.Best,
                TimeSpan.FromSeconds(10));

            Location location = await Geolocation.GetLocationAsync(request);

            if (location == null)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Location unavailable",
                    "Unable to determine your current location.",
                    "OK");

                return;
            }

            Property.Latitude = location.Latitude;
            Property.Longitude = location.Longitude;

            IEnumerable<Placemark> placemarks =
            await Geocoding.Default.GetPlacemarksAsync(location);

            Placemark placemark = placemarks?.FirstOrDefault();

            if (placemark != null)
            {
                Property.Address = BuildAddress(placemark);
            }

            OnPropertyChanged(nameof(Property));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Location Error",
                $"Unable to get your current location.\n\n{ex.Message}",
                "OK");
        }
    }
    private Command geocodeAddressCommand;
    public ICommand GeocodeAddressCommand =>
        geocodeAddressCommand ??= new Command(
            async () => await GeocodeAddress());

    private async Task GeocodeAddress()
    {
        try
        {
            if (Property == null || string.IsNullOrWhiteSpace(Property.Address))
            {
                await Shell.Current.DisplayAlertAsync(
                    "Address required",
                    "Please enter an address first.",
                    "OK");

                return;
            }

            IEnumerable<Location> locations =
                await Geocoding.Default.GetLocationsAsync(Property.Address);

            Location location = locations?.FirstOrDefault();

            if (location == null)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Address not found",
                    "Unable to find the location for the entered address.",
                    "OK");

                return;
            }

            Property.Latitude = location.Latitude;
            Property.Longitude = location.Longitude;

            OnPropertyChanged(nameof(Property));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Geocoding Error",
                $"Unable to find the location for this address.\n\n{ex.Message}",
                "OK");
        }
    }

    #endregion

    #region Connection
    private async void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        await CheckConnection();
    }
    private async Task CheckConnection()
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            IsGeocodeAddressButtonVisible = false;

            PerformHapticFeedback();
            await Shell.Current.DisplayAlertAsync(
                "No internet connection",
                "You are not connected to the internet.",
                "OK");
        }
        else
        {
            IsGeocodeAddressButtonVisible = true;

            await Shell.Current.DisplayAlertAsync(
                "Connected to the internet",
                "You are connected to the internet.",
                "OK");
        }
        OnPropertyChanged(nameof(IsGeocodeAddressButtonVisible));
    }

    #endregion

    #region Vibration

    private async Task Vibrate()
    {
        try
        {
            Vibration.Default.Vibrate(TimeSpan.FromSeconds(5));
        }
        catch (FeatureNotSupportedException)
        {
            await Shell.Current.DisplayAlertAsync(
                "Vibration not supported",
                "Vibration is not supported on this device.",
                "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Vibration error",
                $"An error occurred while trying to vibrate the device.\n\n{ex.Message}",
                "OK");
        }
    }

    private async Task CancelVibration()
    {
        try
        {
            Vibration.Default.Cancel();
        }
        catch (FeatureNotSupportedException)
        {
            await Shell.Current.DisplayAlertAsync(
                "Vibration not supported",
                "Vibration is not supported on this device.",
                "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Vibration error",
                $"An error occurred while trying to cancel vibration.\n\n{ex.Message}",
                "OK");
        }
    }

    private void PerformHapticFeedback()
    {
        try
        {
            if (HapticFeedback.Default.IsSupported)
            {
                HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
            }
            else
            {
                Shell.Current.DisplayAlert(
                    "Haptic feedback not supported",
                    "Haptic feedback is not supported on this device.",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert(
                "Haptic feedback error",
                $"An error occurred while trying to perform haptic feedback.\n\n{ex.Message}",
                "OK");
        }
    }


    #endregion

    #region Battery

    private async void OnBatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e) => await BatteryStatusCheck();
    private async void OnEnergySaverStatusChanged(object sender, EnergySaverStatusChangedEventArgs e) => await BatteryStatusCheck();
    private async Task BatteryStatusCheck()
    {
        if (Battery.ChargeLevel < 0.2)
        {
            StatusColor = Colors.Red;
            StatusMessage = "Battery is low. Please charge your device.";
        }
        else if (Battery.State == BatteryState.Charging)
        {
            StatusColor = Colors.Orange;
            StatusMessage = "Battery is charging.";
        }
        else
        {
            StatusColor = Colors.Green;
            StatusMessage = "";
        }

            if (Battery.EnergySaverStatus == EnergySaverStatus.On)
        {
            StatusColor = Colors.Green;
            StatusMessage = "Energy saver is on. Some features may be limited.";
        }
        else
            StatusMessage = "";
    }

    #endregion

    #region Flashlight

    public bool IsFlashlightOn { get; set; }
    private Command flashlightCommand;
    public ICommand FlashlightCommand =>
        flashlightCommand ??= new Command(
            async () => await FlashlightAsync());
    public async Task FlashlightAsync()
    {
        try
        {
            if (IsFlashlightOn)
            {
                await Flashlight.Default.TurnOffAsync();
                IsFlashlightOn = false;
            }
            else
            {
                await Flashlight.Default.TurnOnAsync();
                IsFlashlightOn = true;
            }
        }
        catch (FeatureNotSupportedException)
        {
            await Shell.Current.DisplayAlertAsync(
                "Flashlight not supported",
                "Flashlight is not supported on this device.",
                "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Flashlight error",
                $"An error occurred while trying to turn on the flashlight.\n\n{ex.Message}",
                "OK");
        }
    }

    #endregion

    #region HelperMethods

    private string BuildAddress(Placemark placemark)
    {
        var addressParts = new List<string>();

        if (!string.IsNullOrWhiteSpace(placemark.Thoroughfare))
            addressParts.Add(placemark.Thoroughfare);

        if (!string.IsNullOrWhiteSpace(placemark.SubThoroughfare))
            addressParts.Add(placemark.SubThoroughfare);

        if (!string.IsNullOrWhiteSpace(placemark.PostalCode))
            addressParts.Add(placemark.PostalCode);

        if (!string.IsNullOrWhiteSpace(placemark.Locality))
            addressParts.Add(placemark.Locality);

        if (!string.IsNullOrWhiteSpace(placemark.CountryName))
            addressParts.Add(placemark.CountryName);

        return string.Join(", ", addressParts);
    }
    #endregion
}
