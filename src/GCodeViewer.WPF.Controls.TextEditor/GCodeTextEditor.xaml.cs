using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    public partial class GCodeTextEditor : UserControl
    {
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(GCodeTextEditor),
                new FrameworkPropertyMetadata(HandleTextChange));

        private static void HandleTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //if (e.NewValue != null)
            //  _doc.Text = e.NewValue;
        }

        //public string Text
        //{
        //    get => _doc.Text;
        //    set => _doc.Text = value;
        //}

        private TextDocument _doc;
        private StatusBarViewModel _statusbarViewModel;

        public GCodeTextEditor()
        {
            InitializeComponent();
            SetupSyntaxHighlighting();

            _statusbarViewModel = new StatusBarViewModel(TextEditor);
            Statusbar.DataContext = _statusbarViewModel;

            _doc = new TextDocument();

            TextEditor.TextChanged += (s, e) => TextChanged?.Invoke(this, TextEditor.Text);
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

        public EventHandler<string> TextChanged;
    }
}
