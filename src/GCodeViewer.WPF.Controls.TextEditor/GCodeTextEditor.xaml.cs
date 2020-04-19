using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    public partial class GCodeTextEditor : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(GCodeTextEditor),
                new FrameworkPropertyMetadata(HandleTextChange));
        private static void HandleTextChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is null)
                return;

            var editor = (GCodeTextEditor)sender;
            editor._doc.Text = (string)e.NewValue;
        }

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.Register(
                "TextChanged",
                typeof(ICommand),
                typeof(GCodeTextEditor));

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }
        public ICommand TextChanged
        {
            get => (ICommand)this.GetValue(TextChangedProperty);
            set => this.SetValue(TextChangedProperty, value);
        }

        private TextDocument _doc;
        private StatusBarViewModel _statusbarViewModel;

        public GCodeTextEditor()
        {
            InitializeComponent();
            SetupSyntaxHighlighting();

            _statusbarViewModel = new StatusBarViewModel(TextEditor);
            Statusbar.DataContext = _statusbarViewModel;

            _doc = new TextDocument();

            TextEditor.TextChanged += (s, e) => CallTextChangedCommand();
            TextEditor.Document = _doc;
        }

        private void SetupSyntaxHighlighting()
        {
            // TODO: you know what to do
            string path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\GCodeViewer.WPF.Controls.TextEditor\GCodeSyntaxHighlighting.xml";

            using var stream = File.OpenRead(path);
            using var reader = new XmlTextReader(stream);

            TextEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            reader.Close();
            stream.Close();
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
