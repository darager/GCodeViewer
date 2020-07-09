using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using g3;
using GCodeViewer.Helpers;
using GCodeViewer.Library;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Shapes;
using GCodeViewer.WPF.Controls.PointCloud;
using gs;
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
            var origMesh = meshes[0];
            var cutMeshes = CutMesh(origMesh, new Vector3d(1, 1, 10), new Vector3d(0, 0, 1));

            meshes.Remove(origMesh);

            DisplayMesh(cutMeshes.BaseMesh, Color.White);
            DisplayMesh(cutMeshes.CutOffMesh, Color.Green);

            stlFile.SaveMeshes(meshes);
        }

        private (DMesh3 BaseMesh, DMesh3 CutOffMesh) CutMesh(DMesh3 meshToCut, Vector3d position, Vector3d normal)
        {
            var treeCut = new MeshPlaneCut(new DMesh3(meshToCut), position, normal);
            treeCut.Cut();
            CloseHoleInMesh(treeCut);

            var branchCut = new MeshPlaneCut(new DMesh3(meshToCut), position, new Vector3d(-normal.x, -normal.y, -normal.z));
            branchCut.Cut();
            CloseHoleInMesh(branchCut);

            return (treeCut.Mesh, branchCut.Mesh);
        }

        private void CloseHoleInMesh(MeshPlaneCut cut)
        {
            foreach (var loop in cut.CutLoops)
            {
                var holeFill = new MinimalHoleFill(cut.Mesh, loop);
                holeFill.Apply();
            }
        }

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
