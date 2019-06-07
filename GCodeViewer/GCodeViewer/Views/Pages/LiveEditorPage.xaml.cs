using GCodeViewer.Abstractions.ViewModels;
using System.Windows.Controls;

namespace GCodeViewer.Views
{
    /// <summary>
    /// Interaction logic for LiveEditorPage.xaml
    /// </summary>
    public partial class LiveEditorPage : Page
    {
        public LiveEditorPage(ITextViewModel textViewModel)
        {
            InitializeComponent();
            this.DataContext = textViewModel;
        }
    }
}
