using System.IO;
using System.Windows;
using ICSharpCode.AvalonEdit.Document;

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
        }
    }
}
