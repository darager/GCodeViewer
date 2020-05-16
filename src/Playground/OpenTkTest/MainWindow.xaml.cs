using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using g3;
using GCodeViewer.Helpers;
using GCodeViewer.Library.Renderables;
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

            //LoadAndDisplayModelFromStl();
        }

        private void LoadAndDisplayModelFromStl()
        {
            string file = @"STL-Files\xyz-calibrationCube.stl";

            using var stream = File.OpenRead(file);
            using var binaryReader = new BinaryReader(stream);

            var builder = new SimpleMeshBuilder();
            var reader = new STLReader();

            var result = reader.Read(binaryReader, ReadOptions.Defaults, builder);

            if (result.code == IOCode.Ok)
            {
                var mesh = builder.Meshes[0];

                var triangleIndices = mesh.TriangleIndices();

                // TODO: in the future these are controled by the printvolume set in the renderpipeline
                // max and min are not retrieved perfectly
                float max = (float)mesh.Vertices.back;
                float min = (float)mesh.Vertices.front;

                var points = new List<Point3D>();
                foreach (int idx in triangleIndices)
                {
                    var triangle = mesh.GetTriangle(idx);

                    // TODO: this is drawing some lines twice many lines
                    AddPoint(triangle.c);
                    AddPoint(triangle.a);
                    AddPoint(triangle.a);
                    AddPoint(triangle.b);
                    AddPoint(triangle.b);
                    AddPoint(triangle.c);

                    void AddPoint(int vertexIndex)
                    {
                        Vector3d vert = mesh.GetVertex(vertexIndex);

                        float x = ((float)vert.x).Scale(min, max, -0.5f, 0.5f);
                        float y = ((float)vert.y).Scale(min, max, -0.5f, 0.5f);
                        float z = ((float)vert.z).Scale(min, max, -0.5f, 0.5f);

                        points.Add(new Point3D(x, y, z));
                    }
                }

                var model = new Renderable(Color.Aqua, points, RenderableType.Lines);
                _viewer3DVM.Add(model);
            }

            binaryReader.Close();
            stream.Close();
        }
    }
}
