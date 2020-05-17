using System.ComponentModel;
using System.Windows.Input;

namespace GCodeViewer.WPF.Pages
{
    public class StartingPageViewModel : INotifyPropertyChanged
    {
        // implement these
        public ICommand GoToSettingsPage { get; set; }
        public ICommand ImportSTLFile { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
