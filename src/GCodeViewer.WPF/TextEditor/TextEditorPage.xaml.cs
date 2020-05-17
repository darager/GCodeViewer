using System.Windows.Controls;

namespace GCodeViewer.WPF.TextEditor
{
    public partial class TextEditorPage : Page
    {
        public TextEditorPage(TextEditorPageViewModel vm)
        {
            InitializeComponent();

            vm.TextEditor = textEditor;
            this.DataContext = vm;
        }
    }
}
