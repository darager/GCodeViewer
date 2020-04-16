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
        public GCodeTextEditor()
        {
            InitializeComponent();

            SetupSyntaxHighlighting();
            LoadGCodeFile();
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
        private void LoadGCodeFile()
        {
            var doc = new TextDocument();
            doc.TextChanged += (s, e) => TextChanged?.Invoke(this, doc.Text);

            string path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode";
            doc.Text = File.ReadAllText(path);

            TextEditor.Document = doc;
        }

        public EventHandler<string> TextChanged;
    }
}
