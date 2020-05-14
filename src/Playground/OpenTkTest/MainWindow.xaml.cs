using System;
using System.IO;
using System.Windows;
using g3;
using OpenTkTest.ViewModels;

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

            _textEditorVM = new TextEditorViewModel(TextEditor, _viewer3DVM);
            TextEditor.DataContext = _textEditorVM;

            Statusbar.DataContext = new StatusBarViewModel(_textEditorVM);

            LoadAndDisplayModelFromStl();
        }

        private void LoadAndDisplayModelFromStl()
        {
            string file = "Benchy_Christmas_1.stl";

            using var stream = File.OpenRead(file);
            using var breader = new BinaryReader(stream);

            var stlReader = new STLReader();
            //stlReader.Read(breader, ReadOptions.Defaults,)

            stream.Close();
        }
    }
}
