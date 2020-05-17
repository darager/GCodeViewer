using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.Pages
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        public ICommand GoBack { get; private set; }

        private PageNavigationService _navigationService;

        public SettingsPageViewModel(PageNavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBack = new RelayCommand((_) => _navigationService.GoBack());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}