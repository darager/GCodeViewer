using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.Pages
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        public float PrintBedRadius
        {
            get => _printBedRadius;
            set
            {
                if (_printBedRadius == value) return;

                _printBedRadius = value;
                OnPropertyChanged("PrintBedRadius");
            }
        }
        private float _printBedRadius;

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

        public ICommand GoBack { get; private set; }

        private PageNavigationService _navigationService;

        public SettingsPageViewModel(PageNavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBack = new RelayCommand((_) => _navigationService.GoBack());
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}