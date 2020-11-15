using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.Settings
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        #region Bindable Properties

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

        public string AAxisGCodePattern
        {
            get => _aAxisGCodePattern;
            set
            {
                if (_aAxisGCodePattern == value) return;
                _aAxisGCodePattern = value;
                OnPropertyChanged("AAxisGCodePattern");
            }
        }

        public float MinValueAAxis
        {
            get => _minValueAAxis;
            set
            {
                if (_minValueAAxis == value) return;
                _minValueAAxis = value;
                OnPropertyChanged("MinValueAAxised");
            }
        }

        public float MaxValueAAxis
        {
            get => _maxValueAAxis;
            set
            {
                if (_maxValueAAxis == value) return;
                _maxValueAAxis = value;
                OnPropertyChanged("MaxValueAAxis");
            }
        }

        public float MinDegreesAAxis
        {
            get => _minDegreesAAxis;
            set
            {
                if (_minDegreesAAxis == value) return;
                _minDegreesAAxis = value;
                OnPropertyChanged("MinDegreesAAxis");
            }
        }

        public float MaxDegreesAAxis
        {
            get => _maxDegreesAAxis;
            set
            {
                if (_maxDegreesAAxis == value) return;
                _maxDegreesAAxis = value;
                OnPropertyChanged("MaxDegreesAAxis");
            }
        }

        public string CAxisGCodePattern
        {
            get => _cAxisGCodePattern;
            set
            {
                if (_cAxisGCodePattern == value) return; ;
                _cAxisGCodePattern = value;
                OnPropertyChanged("CAxisGCodePattern");
            }
        }

        public float CAxisValueAt360Degrees
        {
            get => _cAxisValueAt360Degrees;
            set
            {
                if (_cAxisValueAt360Degrees == value) return;
                _cAxisValueAt360Degrees = value;
                OnPropertyChanged("CAxisValueAt360Degrees");
            }
        }

        public ICommand GoBack { get; private set; }
        public ICommand SaveAndApplySettings { get; private set; }

        private float _printBedDiameter;
        private float _printVolumeHeight;
        private float _aAxisOffset;

        private string _aAxisGCodePattern;
        private float _minValueAAxis;
        private float _maxValueAAxis;
        private float _minDegreesAAxis;
        private float _maxDegreesAAxis;
        private string _cAxisGCodePattern;
        private float _cAxisValueAt360Degrees;

        #endregion

        private readonly SettingsService _settingsService;
        private readonly PageNavigationService _navigationService;
        private readonly IViewerScene _printerScene;

        private Library.PrinterSettings.Settings _settings;
        private AAxisOffset _aAxisOffsetRenderable;

        public SettingsPageViewModel(PageNavigationService navigationService,
                                     SettingsService settingsService,
                                     IViewerScene printerScene)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _printerScene = printerScene;

            GoBack = new RelayCommand(GoBackAndResetSettings);
            SaveAndApplySettings = new RelayCommand(ApplySettingsAndSaveThem);

            LoadSettings();
            _aAxisOffsetRenderable = GetAAxisRenderable();
        }

        public void ShowAAxisOffset()
        {
            _printerScene.Add(_aAxisOffsetRenderable, new Point3D(0, 0, 0), (0, 0, 0));
        }

        private AAxisOffset GetAAxisRenderable()
        {
            return new AAxisOffset(_settings.PrinterDimensions.AAxisOffset,
                                   ColorTranslator.FromHtml("#03dac5"),
                                   ColorTranslator.FromHtml("#2D2D30"));
        }

        private void GoBackAndResetSettings(object _)
        {
            LoadSettings();
            _printerScene.Remove(_aAxisOffsetRenderable);
            _navigationService.GoBack();
        }

        private void ApplySettingsAndSaveThem(object _)
        {
            StoreSettings();

            UpdateAAxisRenderable();
            _printerScene.SetPrintBedDiameter(PrintBedDiameter);
        }

        private void UpdateAAxisRenderable()
        {
            _printerScene.Remove(_aAxisOffsetRenderable);
            _aAxisOffsetRenderable = GetAAxisRenderable();
            ShowAAxisOffset();
        }

        private void LoadSettings()
        {
            _settings = _settingsService.Settings;

            this.AAxisOffset = _settings.PrinterDimensions.AAxisOffset;
            this.PrintBedDiameter = _settings.PrinterDimensions.PrintBedDiameter;
            this.PrintVolumeHeight = _settings.PrinterDimensions.PrintVolumeHeight;

            this.AAxisGCodePattern = _settings.AAxisParserInfo.GCodePattern;
            this.MinValueAAxis = _settings.AAxisParserInfo.MinValueAAxis;
            this.MaxValueAAxis = _settings.AAxisParserInfo.MaxValueAAxis;
            this.MinDegreesAAxis = _settings.AAxisParserInfo.MinDegreesAAxis;
            this.MaxDegreesAAxis = _settings.AAxisParserInfo.MaxDegreesAAxis;

            this.CAxisGCodePattern = _settings.CAxisParserInfo.GCodePattern;
            this.CAxisValueAt360Degrees = _settings.CAxisParserInfo.ValueAt360Degrees;
        }

        private void StoreSettings()
        {
            _settings.PrinterDimensions.AAxisOffset = this.AAxisOffset;
            _settings.PrinterDimensions.PrintBedDiameter = this.PrintBedDiameter;
            _settings.PrinterDimensions.PrintVolumeHeight = this.PrintVolumeHeight;

            _settings.AAxisParserInfo.GCodePattern = this.AAxisGCodePattern;
            _settings.AAxisParserInfo.MinValueAAxis = this.MinValueAAxis;
            _settings.AAxisParserInfo.MaxValueAAxis = this.MaxValueAAxis;
            _settings.AAxisParserInfo.MinDegreesAAxis = this.MinDegreesAAxis;
            _settings.AAxisParserInfo.MaxDegreesAAxis = this.MaxDegreesAAxis;

            _settings.CAxisParserInfo.GCodePattern = this.CAxisGCodePattern;
            _settings.CAxisParserInfo.ValueAt360Degrees = this.CAxisValueAt360Degrees;

            _settingsService.StoreSettings(_settings);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
