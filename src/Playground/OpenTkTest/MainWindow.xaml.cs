using System.IO;
using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private PointCloudViewModel _vm = new PointCloudViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;

            string path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode";

            TextEditor.TextChanged += (_, text) => _vm.Update3DModel(text);
            TextEditor.Text = File.ReadAllText(path);
        }
    }
}
