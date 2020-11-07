using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using g3;
using GCodeViewer.Library;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Shapes;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.Viewer3D;
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

            _viewer3DVM.Add(new Wireframe(cutMeshes.BaseMesh, Color.Yellow, Color.DarkSlateGray));
            _viewer3DVM.Add(new Wireframe(cutMeshes.CutOffMesh, Color.GreenYellow));

            //stlFile.SaveMeshes(new List<Mesh> { cutMeshes.BaseMesh });
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
