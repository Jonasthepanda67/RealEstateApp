using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly IPropertyService service;
        public SettingsPageViewModel(IPropertyService service)
        {
            this.service = service;
            LoadSettings();
        }

        #region Properties
        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set
            {
                if (SetProperty(ref _volume, value))
                {
                    Preferences.Set("volume", value);
                }
            }
        }

        private double _pitch;
        public double Pitch
        {
            get { return _pitch; }
            set
            {
                if (SetProperty(ref _pitch, value))
                {
                    Preferences.Set("pitch", value);
                }
            }
        }

        private bool _sortByClosest;
        public bool SortByClosest
        {
            get { return _sortByClosest; }
            set
            {
                if (SetProperty(ref _sortByClosest, value))
                {
                    Preferences.Set("sortByClosest", value);
                }
            }
        }

        #endregion

        #region Settings

        public void LoadSettings()
        {
            Volume = Preferences.Get("volume", 0.6);
            Pitch = Preferences.Get("pitch", 1.2);
            SortByClosest = Preferences.Get("sortByClosest", true);

            if (Volume < 0.1 || Volume > 2.0)
            {
                Volume = 0.6;
            }
            if (Pitch < 0.1 || Pitch > 2.0)
            {
                Pitch = 1.2;
            }
        }

        private Command _resetSettingsCommand;
        public Command ResetSettingsCommand => _resetSettingsCommand ??= new Command(ResetSettings);
        public void ResetSettings()
        {
            Preferences.Clear();
        }

        #endregion
    }
}
