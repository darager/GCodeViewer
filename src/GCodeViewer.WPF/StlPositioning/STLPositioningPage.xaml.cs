using System.Windows.Controls;

namespace GCodeViewer.WPF.StlPositioning
{
    public partial class STLPositioningPage : Page
    {
        public STLPositioningPage(STLPositioningPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
