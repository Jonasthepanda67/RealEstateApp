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

    private Command editPropertyCommand;
    public ICommand EditPropertyCommand => editPropertyCommand ??= new Command(async () => await GotoEditProperty());
    async Task GotoEditProperty()
    {
        await Shell.Current.GoToAsync($"{nameof(AddEditPropertyPage)}?mode=editproperty", true, new Dictionary<string, object>
        {
            {"MyProperty", Property }
        });
    }
}
