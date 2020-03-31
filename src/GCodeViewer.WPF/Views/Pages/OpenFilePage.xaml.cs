using GCodeViewer.WPF.Abstractions.ViewModels;
using System.Windows.Controls;

namespace GCodeViewer.WPF.Views.Pages
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
