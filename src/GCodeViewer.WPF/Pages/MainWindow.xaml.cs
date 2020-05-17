using System.Windows;
using GCodeViewer.WPF.ViewModels;

namespace GCodeViewer.WPF.Pages
{
    public partial class MainWindow : Window
    {
        public MainWindow(Viewer3DViewModel viewerVM)
        {
            InitializeComponent();

            Viewer3D.DataContext = viewerVM;
        }
    }
}
