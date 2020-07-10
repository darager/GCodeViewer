using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using GCodeViewer.Library;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.WPF.StlPositioning
{
    public class STLPositioningPageViewModel : INotifyPropertyChanged
    {
        private IViewerScene _scene;

        public STLPositioningPageViewModel(IViewerScene scene)
        {
            _scene = scene;
        }

        public void LoadSTL(string filePath)
        {
            var file = new STLFile(filePath);
            List<Mesh> meshes = file.LoadMeshes();

            var wireframe = new Wireframe(meshes[0], Color.GreenYellow, Color.DarkGreen);

            _scene.Add(wireframe, new Point3D(0, 0, 0));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
