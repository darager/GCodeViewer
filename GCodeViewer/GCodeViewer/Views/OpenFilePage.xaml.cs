using GCodeViewer.Interfaces.ViewModels;
using Ninject;
using System.Windows.Controls;
using System.Windows.Input;

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
