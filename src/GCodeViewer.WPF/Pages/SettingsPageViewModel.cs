using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GCodeViewer.Library;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Settings;

namespace GCodeViewer.WPF.Pages
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

        private PageNavigationService _navigationService;
        private SettingsService _settingsService;

        public SettingsPageViewModel(PageNavigationService navigationService, SettingsService settingsService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;

            GoBackAndSave = new RelayCommand((_) => GoBackAndSaveSettings());

            LoadSettings();
        }

        private Configuration _settings;

        private void GoBackAndSaveSettings()
        {
            StoreSettings();
            _navigationService.GoBack();
        }

        private void LoadSettings()
        {
            _settings = _settingsService.LoadSettings();

            this.PrintVolumeHeight = _settings.PrintArea.Height;
            this.PrintBedDiameter = _settings.PrintArea.Diameter;
        }
        private void StoreSettings()
        {
            _settings.PrintArea.Height = this.PrintVolumeHeight;
            _settings.PrintArea.Diameter = this.PrintBedDiameter;

            _settingsService.StoreSettings(_settings).Wait();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}