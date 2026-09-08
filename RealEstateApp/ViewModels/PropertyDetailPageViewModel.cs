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
}
