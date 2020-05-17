using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.Pages
{
    public class StartingPageViewModel : INotifyPropertyChanged
    {
        // implement these
        public ICommand GoToSettingsPage { get; private set; }
        public ICommand ImportSTLFile { get; private set; }

        public StartingPageViewModel(PageNavigationService navigationService)
        {
            GoToSettingsPage = new RelayCommand((_) => navigationService.GoTo(Navigation.Navigation.SettingsPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
