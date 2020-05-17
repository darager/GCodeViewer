using System.Windows;

namespace GCodeViewer.WPF.MainWindow
{
    public partial class MainWindow : Window
    {
        public MainWindow(Viewer3DViewModel viewerVM, PagingViewModel pagingVM)
        {
            InitializeComponent();

            Viewer3D.DataContext = viewerVM;
            Frame.DataContext = pagingVM;
        }
    }
}
