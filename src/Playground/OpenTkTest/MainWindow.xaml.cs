using System.IO;
using System.Reflection;
using System.Windows;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new PointCloudViewModel();
            this.DataContext = vm;

            var doc = new TextDocument();
            doc.TextChanged += (s, e) => vm.Update3DModel(doc.Text);

            var path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode";
            doc.Text = File.ReadAllText(path);

            TextEditor.Document = doc;

            SetupSyntaxHighlighting();
        }

        public void SetupSyntaxHighlighting()
        {
            string xshd_path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Playground\OpenTkTest\GCodeSyntaxHighlighting.xml";
            using var xshd_stream = File.OpenRead(xshd_path);

            var xshd_reader = new XmlTextReader(xshd_stream);
            TextEditor.SyntaxHighlighting = HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);
            xshd_reader.Close();
            xshd_stream.Close();
        }
    }
}
