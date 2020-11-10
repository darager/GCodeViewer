using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
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

        public ObservableCollection<SyntaxHighlightingRule> SyntaxHighlightingRules
        {
            get => (ObservableCollection<SyntaxHighlightingRule>)this.GetValue(SyntaxHighlightingRulesProperty);
            set => this.SetValue(SyntaxHighlightingRulesProperty, value);
        }

        public static readonly DependencyProperty SyntaxHighlightingRulesProperty =
            DependencyProperty.Register(
                "SyntaxHighlightingRules",
                typeof(ObservableCollection<SyntaxHighlightingRule>),
                typeof(GCodeTextEditor),
                new FrameworkPropertyMetadata(OnBackupItemsChanged));

        internal IHighlightingDefinition DefinitionFromFile;
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

        private static void OnBackupItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var old = (ObservableCollection<SyntaxHighlightingRule>)e.OldValue;
            if (old != null)
            {
                old.CollectionChanged -= UpdateSyntaxRules;
                return;
            }

            ((ObservableCollection<SyntaxHighlightingRule>)e.NewValue).CollectionChanged += UpdateSyntaxRules;
            UpdateSyntaxRules(d, null);
        }

        private static void UpdateSyntaxRules(object sender, NotifyCollectionChangedEventArgs e)
        {
            var editor = (GCodeTextEditor)sender;

            var definition = editor.DefinitionFromFile;

            foreach (var rule in editor.SyntaxHighlightingRules)
                definition.MainRuleSet.Rules.Add(rule.GetHighlightingRule());

            editor.DefinitionFromFile = definition;
        }

        private void SetupSyntaxHighlighting()
        {
            string path = "GCodeSyntaxHighlighting.xml";

            using var stream = File.OpenRead(path);
            using var reader = new XmlTextReader(stream);

            DefinitionFromFile = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            TextEditor.SyntaxHighlighting = DefinitionFromFile;

            reader.Close();
            stream.Close();
        }

        private void SetupSearchReplace()
        {
            // TODO: after the search/replace functionality is used the texteditor takes a performance hit
            _findReplaceWindow.CurrentEditor = new TextEditorAdapter(TextEditor);
            _findReplaceWindow.ShowSearchIn = false;
            _findReplaceWindow.ShowReplaceAllConfirmationPrompt = false;
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
