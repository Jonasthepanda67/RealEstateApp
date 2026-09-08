using RealEstateApp.Models;
using RealEstateApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RealEstateApp.ViewModels
{
    public class HeightCalculatorPageViewModel : BaseViewModel
    {
        public ObservableCollection<BarometerMeasurement> MeasurementsCollection { get; } = new();
        private readonly IPropertyService service;

        public HeightCalculatorPageViewModel(IPropertyService service)
        {
            this.service = service;
            Barometer.ReadingChanged += OnBarometerReadingChanged;
            Barometer.Start(SensorSpeed.UI);
        }

        #region Properties

        private double _currentPressure;
        public double CurrentPressure
        {
            get => _currentPressure;
            set => SetProperty(ref _currentPressure, value);
        }

        private double _currentAltitude;
        public double CurrentAltitude
        {
            get => _currentAltitude;
            set => SetProperty(ref _currentAltitude, value);
        }

        private string _measurementLabel;
        public string MeasurementLabel
        {
            get => _measurementLabel;
            set => SetProperty(ref _measurementLabel, value);
        }
        #endregion

        #region Saving
        private ICommand _saveMeasurementCommand;
        public ICommand SaveMeasurementCommand => _saveMeasurementCommand ??= new Command(SaveMeasurement);

        private void SaveMeasurement()
        {
            var measurement = new BarometerMeasurement
            {
                Pressure = CurrentPressure,
                Altitude = CurrentAltitude,
                Label = MeasurementLabel,
                HeightChange = MeasurementsCollection.Count > 0 ? CurrentAltitude - MeasurementsCollection.Last().Altitude : 0
            };
            MeasurementsCollection.Add(measurement);
        }

        #endregion

        #region Barometer
        private async void OnBarometerReadingChanged(object sender, BarometerChangedEventArgs e) => await SetBarometerReadings(e);

        private async Task SetBarometerReadings(BarometerChangedEventArgs e)
        {
            CurrentPressure = e.Reading.PressureInHectopascals;
            var seaLevelPressure = 1006.5;
            var altitudeInMeters = 44307.694 * (1 - Math.Pow(CurrentPressure / seaLevelPressure, 0.190284));
            CurrentAltitude = altitudeInMeters;
        }

        #endregion
    }
}
