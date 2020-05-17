using System.Windows.Controls;

namespace GCodeViewer.WPF.Pages
{
    public partial class StartingPage : Page
    {
        public StartingPage(StartingPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
