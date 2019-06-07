using GCodeViewer.Abstractions.ViewModels;
using System.Windows.Controls;

namespace GCodeViewer.Views
{
    /// <summary>
    /// Interaction logic for OpenFilePage.xaml
    /// </summary>
    public partial class OpenFilePage : Page
    {
        public OpenFilePage(IToolbarViewModel toolbarViewModel)
        {
            InitializeComponent();
            DataContext = toolbarViewModel;
        }
    }
}
