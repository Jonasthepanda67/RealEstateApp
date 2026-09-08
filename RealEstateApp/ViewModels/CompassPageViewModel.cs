using RealEstateApp.Models;
using RealEstateApp.Services;
using RealEstateApp.Views;
using System.Windows.Input;

namespace RealEstateApp.ViewModels
{
    [QueryProperty(nameof(Property), "MyProperty")]
    public class CompassPageViewModel : BaseViewModel
    {
        private readonly IPropertyService service;

        public CompassPageViewModel(IPropertyService service)
        {
            this.service = service;
            Compass.ReadingChanged += (sender, args) => OnCompassReadingChanged(sender, args);
            Compass.Start(SensorSpeed.UI);
        }

        #region Properties

        private Property property;
        public Property Property
        {
            get => property;
            set
            {
                if (SetProperty(ref property, value))
                {
                    CurrentAspect = property?.Aspect;
                }
            }

        }

        private string _currentAspect;
        public string CurrentAspect
        {
            get => _currentAspect;
            set => SetProperty(ref _currentAspect, value);
        }

        private string _currentHeading;
        public string CurrentHeading
        {
            get => _currentHeading;
            set => SetProperty(ref _currentHeading, value);
        }

        private string _rotationAngle;
        public string RotationAngle
        {
            get => _rotationAngle;
            set => SetProperty(ref _rotationAngle, value);
        }

        #endregion

        #region Compass

        private async void OnCompassReadingChanged(object sender, CompassChangedEventArgs e) => await SetCompassReadings(e);

        public async Task SetCompassReadings(CompassChangedEventArgs e)
        {
            if (Property == null)
                return;
            var heading = e.Reading;
            GetAspectFromHeading(heading.HeadingMagneticNorth);
            CurrentHeading = heading.HeadingMagneticNorth.ToString("N2");
        }

        private void GetAspectFromHeading(double heading)
        {
            if (heading >= 337.5 || heading < 22.5)
            {
                CurrentAspect = "North";
                RotationAngle = "-360";
            }
            else if (heading >= 22.5 && heading < 67.5)
            {
                CurrentAspect = "NorthEast";
                RotationAngle = "-45";
            }
            else if (heading >= 67.5 && heading < 112.5)
            {
                CurrentAspect = "East";
                RotationAngle = "-90";
            }
            else if (heading >= 112.5 && heading < 157.5)
            {
                CurrentAspect = "SouthEast";
                RotationAngle = "-135";
            }
            else if (heading >= 157.5 && heading < 202.5)
            {
                CurrentAspect = "South";
                RotationAngle = "180";
            }
            else if (heading >= 202.5 && heading < 247.5)
            {
                CurrentAspect = "SouthWest";
                RotationAngle = "-225";
            }
            else if (heading >= 247.5 && heading < 292.5)
            {
                CurrentAspect = "West";
                RotationAngle = "-270";
            }
            else if (heading >= 292.5 && heading < 337.5)
            {
                CurrentAspect = "NorthWest";
                RotationAngle = "-315";
            }
            else
            {
                CurrentAspect = "North";
                RotationAngle = "-0";
            }
        }

        #endregion

        #region Navigation

        private Command editPropertyCommand;
        public ICommand EditPropertyCommand => editPropertyCommand ??= new Command(async () => await GotoEditProperty());
        async Task GotoEditProperty()
        {
            Compass.Stop();
            Property.Aspect = CurrentAspect;
            Compass.ReadingChanged -= OnCompassReadingChanged;
            await Shell.Current.GoToAsync($"{nameof(AddEditPropertyPage)}?mode=editproperty", true, new Dictionary<string, object>
            {
                {"MyProperty", Property }
            });
        }

        #endregion
    }
}
