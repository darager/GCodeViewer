using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.Library.Renderables;
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

        public float AAxisOffset
        {
            get => _aAxisOffset;
            set
            {
                if (_aAxisOffset == value) return;

                _aAxisOffset = value;
                OnPropertyChanged("AAxisOffset");
            }
        }
        private float _aAxisOffset;

        public ICommand GoBack { get; private set; }
        public ICommand SaveAndApplySettings { get; private set; }

        private Library.Settings _settings;

        private readonly SettingsService _settingsService;
        private readonly PageNavigationService _navigationService;
        private readonly PrinterScene _printerScene;

        public SettingsPageViewModel(PageNavigationService navigationService,
                                     SettingsService settingsService,
                                     PrinterScene printerScene)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _printerScene = printerScene;

            GoBack = new RelayCommand(GoBackAndResetSettings);
            SaveAndApplySettings = new RelayCommand(ApplySettingsAndSaveThem);

            LoadSettings();
        }

        private void GoBackAndResetSettings(object _)
        {
            LoadSettings();
            _navigationService.GoBack();
        }
        private void ApplySettingsAndSaveThem(object _)
        {
            StoreSettings();

            _printerScene.SetPrintBedDiameter(PrintBedDiameter);
            _printerScene.UpdateAAxisOffset(AAxisOffset);
        }

        private void LoadSettings()
        {
            _settings = _settingsService.Settings;

            this.AAxisOffset = _settings.PrinterDimensions.AAxisOffset;
            this.PrintBedDiameter = _settings.PrinterDimensions.PrintBedDiameter;
            this.PrintVolumeHeight = _settings.PrinterDimensions.PrintVolumeHeight;
        }
        private void StoreSettings()
        {
            _settings.PrinterDimensions.AAxisOffset = this.AAxisOffset;
            _settings.PrinterDimensions.PrintBedDiameter = this.PrintBedDiameter;
            _settings.PrinterDimensions.PrintVolumeHeight = this.PrintVolumeHeight;

            _settingsService.StoreSettings(_settings).Wait();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}