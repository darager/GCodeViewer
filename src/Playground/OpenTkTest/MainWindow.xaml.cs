using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using g3;
using GCodeViewer.Helpers;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Shapes;
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

            string filePath = @"STL-Files\xyz-calibrationCube.stl";

            var mesh = LoadMesh(filePath);
            DisplayMesh(mesh);
            SaveMesh(mesh, filePath);
        }

        private SimpleMesh LoadMesh(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var binaryReader = new BinaryReader(stream);

            var builder = new SimpleMeshBuilder();
            var reader = new STLReader();

            var result = reader.Read(binaryReader, ReadOptions.Defaults, builder);

            binaryReader.Close();
            stream.Close();

            if (result.code == IOCode.Ok)
            {
                return builder.Meshes[0];
            }
            else
                throw new Exception("Reading the STL file went wrong.");
        }

        private void SaveMesh(SimpleMesh mesh, string filePath)
        {
            using var stream = File.OpenWrite(filePath);
            var binaryWriter = new BinaryWriter(stream);

            var writemesh = new WriteMesh(mesh);

            var writer = new STLWriter();
            writer.Write(binaryWriter, new List<WriteMesh>() { writemesh }, WriteOptions.Defaults);

            binaryWriter.Close();
            stream.Close();
        }

        private void DisplayMesh(SimpleMesh mesh)
        {
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

        private Renderable movemodel;

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (movemodel != null)
                _viewer3DVM.Remove(movemodel);

            movemodel = Cylinder.With()
                                .Position(new Point3D((float)e.OldValue, 0, (float)e.NewValue))
                                .Height((float)e.NewValue)
                                .Radius(0.5f)
                                .Build();

            _viewer3DVM?.Add(movemodel);
        }
    }
}
