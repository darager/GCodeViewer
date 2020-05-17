using System.Windows;
using GCodeViewer.WPF.ViewModels;

namespace GCodeViewer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Viewer3D.DataContext = new Viewer3DViewModel();
        }
    }
}
