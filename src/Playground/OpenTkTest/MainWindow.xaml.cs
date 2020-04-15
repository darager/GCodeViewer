using System.IO;
using System.Windows;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private PointCloudViewModel vm;
        public MainWindow()
        {
            InitializeComponent();

            vm = new PointCloudViewModel();
            this.DataContext = vm;

            LoadGCodeFile();
            SetupSyntaxHighlighting();
        }

        private void LoadGCodeFile()
        {
            var doc = new TextDocument();
            doc.TextChanged += (s, e) => vm.Update3DModel(doc.Text);

            var path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode";
            doc.Text = File.ReadAllText(path);

            TextEditor.Document = doc;
        }
        private void SetupSyntaxHighlighting()
        {
            string path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Playground\OpenTkTest\GCodeSyntaxHighlighting.xml";

            using var stream = File.OpenRead(path);
            using var reader = new XmlTextReader(stream);

            TextEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            reader.Close();
            stream.Close();
        }
    }
}
