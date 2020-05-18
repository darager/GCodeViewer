using System.Windows;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.WPF.MainWindow
{
    public partial class MainWindow : Window
    {
        private Viewer3DViewModel _viewerVM;
        private PagingViewModel _pagingVM;

        public MainWindow(Viewer3DViewModel viewerVM, PagingViewModel pagingVM)
        {
            InitializeComponent();

            _viewerVM = viewerVM;
            _pagingVM = pagingVM;

            Viewer3D.DataContext = _viewerVM;
            Frame.DataContext = _pagingVM;
        }
    }
}
