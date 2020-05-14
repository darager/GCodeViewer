using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using GCodeViewer.Helpers;
using g3;
using GCodeViewer.WPF.Controls.PointCloud;
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
            using var binaryReader = new BinaryReader(stream);

            var reader = new STLReader();
            var builder = new SimpleMeshBuilder();

            var result = reader.Read(binaryReader, ReadOptions.Defaults, builder);

            if (result.code == IOCode.Ok)
            {
                var mesh = builder.Meshes[0];

                var dVerts = mesh.Vertices;

                float max = (float)dVerts.Max();
                float min = (float)dVerts.Min();

                var verts = new List<float>();
                foreach (float vert in dVerts)
                {
                    verts.Add(vert.Scale(min, max, -0.5f, 0.5f));
                }

                var model = new Renderable(Color.Aqua, verts.ToArray(), RenderableType.Points);
                _viewer3DVM.PointCloudObjects.Add(model);
            }

            binaryReader.Close();
            stream.Close();
        }
    }
}
