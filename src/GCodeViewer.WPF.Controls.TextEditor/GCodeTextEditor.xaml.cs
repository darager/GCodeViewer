using System;
using System.IO;
using System.Windows.Controls;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Windows.Input;
using System.ComponentModel;
using System.Text.RegularExpressions;

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

            TextEditor.TextChanged += (s, e) => TextChanged?.Invoke(this, TextEditor.Text);
            TextEditor.Document = _doc;


            TextEditor.MouseDown += TextEditor_MouseDown;

            this.DataContext = vm;
        }


        private SimpleTestVM vm = new SimpleTestVM();
        private void TextEditor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var offset = TextEditor.CaretOffset;

            var textlocation = _doc.GetLocation(offset);

            //var newloc = new TextLocation(300, 0);
            //TextEditor.CaretOffset = _doc.GetOffset(newloc);

            vm.CurrentLine = textlocation.Line.ToString();
            //locationlabel.Content = $"Ln:{textlocation.Line} / {_doc.LineCount}";
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
