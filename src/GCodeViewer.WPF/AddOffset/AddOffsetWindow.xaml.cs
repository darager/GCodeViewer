using System.Windows;
using GCodeViewer.Library.GCodeParsing;

namespace GCodeViewer.WPF.AddOffset
{
    public partial class AddOffsetWindow : Window
    {
        private TextEditor.TextEditorPageViewModel _textEditorPageViewModel { get; }

        public AddOffsetWindow(TextEditor.TextEditorPageViewModel textEditorPageViewModel)
        {
            InitializeComponent();
            _textEditorPageViewModel = textEditorPageViewModel;
        }

        private void AddOffset(object sender, RoutedEventArgs e)
        {
            if (_textEditorPageViewModel.TextEditor is null)
                return;

            var newLineBreak = '\n';
            string[] lines = _textEditorPageViewModel.GetText().Split(newLineBreak);

            uint from = (uint)fromLine.Value;
            uint to = (uint)toLine.Value;

            var extractor = new GCodeAxisValueExtractor();
            var newlines = extractor.AddOffset(lines, from, to, xOffset.Value, yOffset.Value, zOffset.Value);

            _textEditorPageViewModel.SetText(string.Join(newLineBreak, newlines));
        }
    }
}
