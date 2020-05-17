using System.Windows.Controls;

namespace GCodeViewer.WPF.Settings
{
    public partial class SettingsPage : Page
    {
        public SettingsPage(SettingsPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
