using System;
using System.IO;
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
            get => _doc.Text;
            set => _doc.Text = value;
        }

        private TextDocument _doc = new TextDocument();

        public GCodeTextEditor()
        {
            InitializeComponent();

            SetupSyntaxHighlighting();

            _doc.TextChanged += (s, e) => TextChanged?.Invoke(this, _doc.Text);

            TextEditor.Document = _doc;
        }

        private void SetupSyntaxHighlighting()
        {
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
