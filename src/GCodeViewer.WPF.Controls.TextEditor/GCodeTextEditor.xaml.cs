using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using FindReplace;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    public partial class GCodeTextEditor : UserControl
    {
        public ICommand TextChanged
        {
            get => (ICommand)this.GetValue(TextChangedProperty);
            set => this.SetValue(TextChangedProperty, value);
        }

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.Register(
                "TextChanged",
                typeof(ICommand),
                typeof(GCodeTextEditor));

        public string Text
        {
            get => _doc.Text;
            set => _doc.Text = value;
        }

        private TextDocument _doc = new TextDocument();
        private FindReplaceMgr _findReplaceWindow = new FindReplaceMgr();

        public GCodeTextEditor()
        {
            InitializeComponent();
            SetupSyntaxHighlighting();

            // this has to be done so the parent window is not null
            this.Loaded += (s, e) => SetupSearchReplace();

            Statusbar.DataContext = new StatusBarViewModel(TextEditor);
            TextEditor.Document = _doc;

            // TODO: this causes performance issues
            _doc.TextChanged += (s, e) => CallTextChangedCommand();
        }

        private void SetupSyntaxHighlighting()
        {
            string path = "GCodeSyntaxHighlighting.xml";

            using var stream = File.OpenRead(path);
            using var reader = new XmlTextReader(stream);

            TextEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            reader.Close();
            stream.Close();
        }

        private void SetupSearchReplace()
        {
            // TODO: after the search/replace functionality is used the texteditor takes a performance hit
            _findReplaceWindow.CurrentEditor = new TextEditorAdapter(TextEditor);
            _findReplaceWindow.ShowSearchIn = false;
            _findReplaceWindow.OwnerWindow = Window.GetWindow(TextEditor);

            CommandBindings.Add(_findReplaceWindow.FindBinding);
            CommandBindings.Add(_findReplaceWindow.ReplaceBinding);
            CommandBindings.Add(_findReplaceWindow.FindNextBinding);
        }

        private void CallTextChangedCommand()
        {
            if (TextChanged is null)
                return;

            if (TextChanged.CanExecute(Text))
                TextChanged.Execute(Text);
        }
    }
}
