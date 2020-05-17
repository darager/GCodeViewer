using System.Windows.Controls;

namespace GCodeViewer.WPF.Starting
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
