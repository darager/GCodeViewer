using System.Windows.Controls;

namespace GCodeViewer.WPF.Pages
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
