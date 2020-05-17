using System;
using System.Windows.Controls;

namespace GCodeViewer.WPF.TextEditor
{
    public partial class TextEditorPage : Page, ITextEditor
    {
        public TextEditorPage(TextEditorPageViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;

            textEditor.TextChanged
        }

        public event EventHandler TextChanged;

        public string GetText()
        {
            return textEditor.Text;
        }
        public void SetText(string text)
        {
            textEditor.Text = text;
        }
    }
}
