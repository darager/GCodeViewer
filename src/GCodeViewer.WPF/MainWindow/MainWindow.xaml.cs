using System.Windows;

namespace GCodeViewer.WPF.MainWindow
{
    public partial class MainWindow : Window
    {
        public Viewer3DViewModel Viewer3DViewModel { get; }
        public PagingViewModel PagingViewModel { get; }

        public MainWindow(Viewer3DViewModel viewerVM, PagingViewModel pagingVM)
        {
            InitializeComponent();

            Viewer3DViewModel = viewerVM;
            PagingViewModel = pagingVM;

            DataContext = this;
        }
    }
}
