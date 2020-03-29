using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            pclViewer.DataContext = new PointCloudViewModel();
        }
    }
}
