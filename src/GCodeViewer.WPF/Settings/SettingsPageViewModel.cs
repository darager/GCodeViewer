using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.Library;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.Settings
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        public float PrintBedDiameter
        {
            get => _printBedDiameter;
            set
            {
                if (_printBedDiameter == value) return;

                _printBedDiameter = value;
                OnPropertyChanged("PrintBedDiameter");
            }
        }
        private float _printBedDiameter;

        public float PrintVolumeHeight
        {
            get => _printVolumeHeight;
            set
            {
                if (_printVolumeHeight == value) return;

                _printVolumeHeight = value;
                OnPropertyChanged("PrintVolumeHeight");
            }
        }
        private float _printVolumeHeight;

        public ICommand GoBackAndSave { get; private set; }

        private Library.Settings _settings;

        private readonly SettingsService _settingsService;
        private readonly PageNavigationService _navigationService;

        public SettingsPageViewModel(PageNavigationService navigationService, SettingsService settingsService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;

            GoBackAndSave = new RelayCommand(GoBackAndSaveSettings);

            LoadSettings();
        }

        private void GoBackAndSaveSettings(object _)
        {
            StoreSettings();
            _navigationService.GoBack();
        }

        private void LoadSettings()
        {
            _settings = _settingsService.LoadSettings();

            this.PrintVolumeHeight = _settings.PrinterDimensions.Height;
            this.PrintBedDiameter = _settings.PrinterDimensions.Diameter;
        }
        private void StoreSettings()
        {
            _settings.PrinterDimensions.Height = this.PrintVolumeHeight;
            _settings.PrinterDimensions.Diameter = this.PrintBedDiameter;

            _settingsService.StoreSettings(_settings).Wait();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}