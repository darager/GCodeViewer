using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

            var meshes = LoadMeshes(filePath);

            // cutting meshes
            var cutMeshes = CutMesh(meshes[0], new Vector3d(1, 1, 10), new Vector3d(0, 0, 1));

            meshes.Remove(meshes[0]);
            meshes.Add(cutMeshes.branch);

            foreach (var mesh in meshes)
                DisplayMesh(mesh);

            SaveMeshes(meshes, filePath);
        }

        private (DMesh3 tree, DMesh3 branch) CutMesh(DMesh3 meshToCut, Vector3d position, Vector3d normal)
        {
            //var copy = new DMesh3();
            //meshToCut.CompactCopy(copy);

            //var treeCut = new MeshPlaneCut(meshToCut, position, normal);
            //treeCut.Cut();
            //treeCut.FillHoles();

            var branchCut = new MeshPlaneCut(meshToCut, position, new Vector3d(-normal.x, -normal.y, -normal.z));
            branchCut.Cut();
            branchCut.FillHoles();
            //branchCut.CutLoops
            // TODO: use cutloops to fill holes (FillHoles ignores holes and indentations on the cut surface
            // https://github.com/gradientspace/geometry3Sharp/search?q=FillHoles&unscoped_q=FillHoles

            //return (treeCut.Mesh, branchCut.Mesh);
            return (null, branchCut.Mesh);
        }

        private List<DMesh3> LoadMeshes(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var binaryReader = new BinaryReader(stream);

            var builder = new DMesh3Builder();
            var reader = new STLReader();

            var result = reader.Read(binaryReader, ReadOptions.Defaults, builder);

            binaryReader.Close();
            stream.Close();

            if (result.code != IOCode.Ok)
                throw new Exception("Something went wrong when reading the STL file!");

            return builder.Meshes;
        }

        private void SaveMeshes(List<DMesh3> meshes, string filePath)
        {
            using var stream = File.OpenWrite(filePath);
            var binaryWriter = new BinaryWriter(stream);

            var writeMeshes = meshes.Select(m => new WriteMesh(m))
                                    .ToList();

            var writer = new STLWriter();
            writer.Write(binaryWriter, writeMeshes, WriteOptions.Defaults);

            binaryWriter.Close();
            stream.Close();
        }

        private void DisplayMesh(DMesh3 mesh)
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

                    float x = (float)vert.x;
                    float y = (float)vert.y;
                    float z = (float)vert.z;

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
