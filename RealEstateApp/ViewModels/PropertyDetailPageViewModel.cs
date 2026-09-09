using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace RealEstateApp.ViewModels;

[QueryProperty(nameof(PropertyListItem), "MyPropertyListItem")]
public class PropertyDetailPageViewModel : BaseViewModel
{
    private readonly IPropertyService service;
    public PropertyDetailPageViewModel(IPropertyService service)
    {
        this.service = service;
    }
    
    #region Properties

    Property property;
    public Property Property { get => property; set { SetProperty(ref property, value); } }


    Agent agent;
    public Agent Agent { get => agent; set { SetProperty(ref agent, value); } }

    public ObservableCollection<PropertyImage> PropertyImages { get; } = new();

    PropertyListItem propertyListItem;
    public PropertyListItem PropertyListItem
    {
        get => propertyListItem;

        set
        {
            SetProperty(ref propertyListItem, value);

            Property = propertyListItem.Property;

            Agent = service.GetAgents()
                .FirstOrDefault(x => x.Id == Property.AgentId);

            // Populate carousel images
            PropertyImages.Clear();

            if (Property.ImageUrls != null)
            {
                foreach (var image in Property.ImageUrls)
                {
                    PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = image
                    });
                }
            }
        }
    }

    #endregion

    #region Navigation

    private Command editPropertyCommand;
    public ICommand EditPropertyCommand => editPropertyCommand ??= new Command(async () => await GotoEditProperty());
    async Task GotoEditProperty()
    {
        await Shell.Current.GoToAsync($"{nameof(AddEditPropertyPage)}?mode=editproperty", true, new Dictionary<string, object>
        {
            {"MyProperty", Property }
        });
    }
    #endregion

    #region Phone

    private Command openPhoneOptionsCommand;
    public ICommand OpenPhoneOptionsCommand => openPhoneOptionsCommand ??= new Command(async () => await OpenPhoneOptionsExecute());

    public async Task OpenPhoneOptionsExecute()
    {
        if (Agent == null || string.IsNullOrWhiteSpace(Agent.Phone))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Agent phone number is not available.", "OK");
            return;
        }
        string action = await Shell.Current.DisplayActionSheetAsync("Contact Agent", "Cancel", null, "Call", "SMS");
        switch (action)
        {
            case "Call":
                try
                {
                    PhoneDialer.Open(Agent.Phone);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlertAsync("Error", $"Unable to make a call: {ex.Message}", "OK");
                }
                break;
            case "SMS":
                try
                {
                    await Sms.ComposeAsync(new SmsMessage($"Hej, {Property.Vendor.FirstName},\nangående {Property.Address} ", Agent.Phone));
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlertAsync("Error", $"Unable to compose Sms message: {ex.Message}", "OK");
                }
                break;
            default:
                break;
        }
    }


    #endregion

    #region Email

    private Command openEmailClientCommand;
    public ICommand OpenEmailClientCommand => openEmailClientCommand ??= new Command(async () => await OpenEmailClientExecute());

    private async Task OpenEmailClientExecute()
    {
        if (Agent == null || string.IsNullOrWhiteSpace(Agent.Email))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Agent email is not available.", "OK");
            return;
        }

        var attachmentFilePath = Path.Combine(FileSystem.CacheDirectory, "property.txt");
        await File.WriteAllTextAsync(attachmentFilePath, $"{Property.Address}");

        EmailMessage message = new EmailMessage($"{Property.Name}", $"Hej, {Property.Vendor.FirstName},\nangående {Property.Address} ", Agent.Email);
        message.Attachments.Add(new EmailAttachment(attachmentFilePath));

        try
        {
            await Email.ComposeAsync(message);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Unable to compose Email: {ex.Message}","OK");
        }
    }

    #endregion

    #region TextToSpeech

    private Command tTSStartCommand;
    public ICommand TTSStartCommand => tTSStartCommand ??= new Command(async () => await TTSStartExecute());
    private CancellationTokenSource cts;
    SpeechOptions options = new SpeechOptions()
    {
        Pitch = 1.2f,   // 0.0 - 2.0
        Volume = 0.75f, // 0.0 - 1.0
        Rate = 1.0f,    // 0.1 - 2.0
    };
    private async Task TTSStartExecute()
    {
        cts = new CancellationTokenSource();

        TTSIsPlaying = true;
        TTSIsNotPlaying = false;

        await TextToSpeech.Default.SpeakAsync(Property.Description, options, cts.Token);

        TTSIsPlaying = false;
        TTSIsNotPlaying = true;
    }

    private Command tTSStopCommand;
    public ICommand TTSStopCommand => tTSStopCommand ??= new Command(async () => await TTSStopExecute());
    private async Task TTSStopExecute()
    {
        await cts.CancelAsync();
        TTSIsPlaying = false;
        TTSIsNotPlaying = true;
    }

    private bool tTSIsPlaying;
    public bool TTSIsPlaying
    {
        get => tTSIsPlaying;
        set => SetProperty(ref tTSIsPlaying, value);
    }

    private bool tTSIsNotPlaying = true;
    public bool TTSIsNotPlaying
    {
        get => tTSIsNotPlaying;
        set => SetProperty(ref tTSIsNotPlaying, value);
    }

    #endregion

    #region Maps

    private Command openMapMarkedCommand;
    public ICommand OpenMapMarkedCommand => openMapMarkedCommand ??= new Command(async () => await OpenMapMarkedExecute());

    private async Task OpenMapMarkedExecute()
    {
        if (Property == null)
        {
            await Shell.Current.DisplayAlertAsync("Error", "Property is not available.", "OK");
            return;
        }

        var location = new Location(Property.Latitude.Value, Property.Longitude.Value);
        var options = new MapLaunchOptions { Name = Property.Address };
        await Map.Default.OpenAsync(location, options);
    }

    private Command openMapDirectionsCommand;
    public ICommand OpenMapDirectionsCommand => openMapDirectionsCommand ??= new Command(async () => await OpenMapDirectionsExecute());

    private async Task OpenMapDirectionsExecute()
    {
        if (Property == null)
        {
            await Shell.Current.DisplayAlertAsync("Error", "Property is not available.", "OK");
            return;
        }

        var location = new Location(Property.Latitude.Value, Property.Longitude.Value);
        var options = new MapLaunchOptions { Name = Property.Address, NavigationMode = NavigationMode.Driving };
        await location.OpenMapsAsync(options);
    }

    #endregion

    #region Browser

    private Command openBrowserCommand;
    public ICommand OpenBrowserCommand => openBrowserCommand ??= new Command(async () => await OpenBrowserExecute());

    public async Task OpenBrowserExecute()
    {
        if (Property == null || string.IsNullOrWhiteSpace(Property.NeighborhoodUrl))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Property neighbourhood URL is not available.", "OK");
            return;
        }

        var options = new BrowserLaunchOptions
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            TitleMode = BrowserTitleMode.Show,
            PreferredToolbarColor = Color.Parse("Green"),
            PreferredControlColor = Color.Parse("LightBlue")
        };

        try
        {
            await Browser.Default.OpenAsync(Property.NeighborhoodUrl, options);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Unable to open browser: {ex.Message}", "OK");
        }
    }

    #endregion

    #region Contract

    private Command openContractCommand;
    public ICommand OpenContractCommand => openContractCommand ??= new Command(async () => await OpenContractExecute());

    public async Task OpenContractExecute()
    {
        if (Property == null || string.IsNullOrWhiteSpace(Property.ContractFilePath))
        {
            await Shell.Current.DisplayAlertAsync("Error", "Property contract file path is not available.", "OK");
            return;
        }

        try
        {
            await Launcher.Default.OpenAsync(new OpenFileRequest("Contract", new ReadOnlyFile(Property.ContractFilePath)));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Unable to open contract file: {ex.Message}", "OK");
        }
    }

    #endregion
}
