using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var pclViewerViewModel = new PointCloudViewModel(pclViewer);
        }
    }
}
