using System.IO;
using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private Viewer3DViewModel _viewer3DVM;
        private TextEditorViewModel _textEditorVM;

        public MainWindow()
        {
            InitializeComponent();

            _viewer3DVM = new Viewer3DViewModel();
            Viewer3D.DataContext = _viewer3DVM;

            _textEditorVM = new TextEditorViewModel();
            //_textEditorVM.TextChanged += (_, text) => _viewer3DVM.Update3DModel(text);
            TextEditor.DataContext = _textEditorVM;

            string path = @"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode";
            _textEditorVM.Text = File.ReadAllText(path);
        }
    }
}
