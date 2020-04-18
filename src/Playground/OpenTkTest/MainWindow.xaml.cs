using System.IO;
using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private PointCloudViewModel _viewer3DVM;
        private TextEditorViewModel _textEditorVM;

        public MainWindow()
        {
            InitializeComponent();

            _viewer3DVM = new PointCloudViewModel();
            pclViewer.DataContext = _viewer3DVM;

            _textEditorVM = new TextEditorViewModel();
            _textEditorVM.TextChanged += (_, text) => _viewer3DVM.Update3DModel(text);
            TextEditor.DataContext = _textEditorVM;

            string path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode";
            _textEditorVM.Text = File.ReadAllText(path);
        }
    }
}
