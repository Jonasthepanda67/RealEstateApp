using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace RealEstateApp.ViewModels;
public class PropertyListPageViewModel : BaseViewModel
{
    public ObservableCollection<PropertyListItem> PropertiesCollection { get; } = new();
    private Location _currentLocation;
    private bool sortFurthestFirst = false;

    private readonly IPropertyService service;

    public PropertyListPageViewModel(IPropertyService service)
    {
        Title = "Property List";
        this.service = service;
    }

    bool isRefreshing;
    public bool IsRefreshing
    {
        get => isRefreshing;
        set => SetProperty(ref isRefreshing, value);
    }

    private Command getPropertiesCommand;
    public ICommand GetPropertiesCommand => getPropertiesCommand ??= new Command(async () => await GetPropertiesAsync());

    async Task GetPropertiesAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            IsRefreshing = true;

            await GetUserLocationAsync();

            List<Property> properties = service.GetProperties();

            PropertiesCollection.Clear();

            foreach (Property property in properties)
            {
                var item = new PropertyListItem(property);

                if (_currentLocation != null &&
                    property.Latitude.HasValue &&
                    property.Longitude.HasValue)
                {
                    Location propertyLocation = new Location(
                        property.Latitude.Value,
                        property.Longitude.Value);

                    item.Distance = _currentLocation.CalculateDistance(
                        propertyLocation,
                        DistanceUnits.Kilometers);
                }

                PropertiesCollection.Add(item);
            }

            SortProperties();

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get properties: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    #region Location

    private async Task GetUserLocationAsync()
    {
        try
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                var request = new GeolocationRequest(
                    GeolocationAccuracy.Best,
                    TimeSpan.FromSeconds(10));

                location = await Geolocation.GetLocationAsync(request);
            }

            _currentLocation = location;

            if (_currentLocation == null)
            {
                await Shell.Current.DisplayAlert(
                    "Location unavailable",
                    "Unable to determine your current location.",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get location: {ex.Message}");

            await Shell.Current.DisplayAlert(
                "Location Error",
                "Unable to determine your current location.",
                "OK");
        }
    }

    #endregion

    #region Sorting
    private Command sortCommand;
    public ICommand SortCommand => sortCommand ??= new Command(async () => await SortAsync());

    async Task SortAsync()
    {
        if (IsBusy)
            return;

        sortFurthestFirst = !sortFurthestFirst;

        SortProperties();
    }

    private void SortProperties()
    {
        var sorted = sortFurthestFirst
            ? PropertiesCollection
                .OrderByDescending(x => x.Distance ?? double.MinValue)
                .ToList()
            : PropertiesCollection
                .OrderBy(x => x.Distance ?? double.MaxValue)
                .ToList();

        PropertiesCollection.Clear();

        foreach (var item in sorted)
        {
            PropertiesCollection.Add(item);
        }
    }

    #endregion


    #region Navigation
    private Command goToDetailsCommand;
    public ICommand GoToDetailsCommand => goToDetailsCommand ??= new Command<PropertyListItem>(async (propertyListItem) => await GoToDetails(propertyListItem));


    async Task GoToDetails(PropertyListItem propertyListItem)
    {
        if (propertyListItem == null)
            return;

        await Shell.Current.GoToAsync(nameof(PropertyDetailPage), true, new Dictionary<string, object>
        {
            {"MyPropertyListItem", propertyListItem }
        });
    }

    private Command goToAddPropertyCommand;
    public ICommand GoToAddPropertyCommand => goToAddPropertyCommand ??= new Command(async () => await GotoAddProperty());
    async Task GotoAddProperty()
    {
        await Shell.Current.GoToAsync($"{nameof(AddEditPropertyPage)}?mode=newproperty", true, new Dictionary<string, object>
        {
            {"MyProperty", new Property() }
        });
    }
    #endregion
}
