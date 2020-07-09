using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using g3;
using GCodeViewer.Library;
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
            var stlFile = new STLFile(filePath);

            var meshes = stlFile.LoadMeshes();

            // cutting meshes
            var cutMeshes = meshes[0].Cut(new Vector3d(1, 1, 10), new Vector3d(0, 0, 1));

            DisplayMesh(cutMeshes.BaseMesh, Color.White);
            DisplayMesh(cutMeshes.CutOffMesh, Color.Green);

            stlFile.SaveMeshes(new List<Mesh> { cutMeshes.BaseMesh });
        }

        // TODO: move this to own Renderable class
        private void DisplayMesh(DMesh3 mesh, Color color)
        {
            var triangleIndices = mesh.TriangleIndices();

            var points = new List<Point3D>();
            foreach (int idx in triangleIndices)
            {
                var triangle = mesh.GetTriangle(idx);

                // TODO: some lines are drawn twice
                AddPoint(triangle.c);
                AddPoint(triangle.a);
                AddPoint(triangle.a);
                AddPoint(triangle.b);
                AddPoint(triangle.b);
                AddPoint(triangle.c);

                void AddPoint(int vertexIndex)
                {
                    Vector3d vert = mesh.GetVertex(vertexIndex);

                    float x = (float)vert.x / 10;
                    float y = (float)vert.y / 10;
                    float z = (float)vert.z / 10;

                    points.Add(new Point3D(x, y, z));
                }
            }

            var model = new Renderable(color, points, RenderableType.Lines);
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
