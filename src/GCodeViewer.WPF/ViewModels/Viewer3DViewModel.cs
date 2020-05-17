using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using GCodeViewer.Library.GCodeParsing;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.WPF.ViewModels
{
    public class Viewer3DViewModel : INotifyPropertyChanged, IRenderService
    {
        public ObservableCollection<Renderable> PointCloudObjects
        {
            get => _pointCloudObjects;
            set
            {
                _pointCloudObjects = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PointCloudObjects"));
            }
        }
        private ObservableCollection<Renderable> _pointCloudObjects;

        public Viewer3DViewModel()
        {
            PointCloudObjects = new ObservableCollection<Renderable>();

            // TODO: the scaling of the renderables should be according to the printbed at first when the height has not changed yet
            this.Add(new CoordinateSystem(new Point3D(0, 0, 0), 0.2f));
            this.Add(new CircularPrintbed(1.0f, Color.DarkGray, Color.White));
        }

        public void Add(Renderable renderable)
        {
            _pointCloudObjects.Add(renderable);
        }
        public void Add(ICompositeRenderable compositeRenderable)
        {
            foreach (var renderable in compositeRenderable.GetParts())
                _pointCloudObjects.Add(renderable);
        }
        public void Remove(Renderable renderable)
        {
            _pointCloudObjects.Remove(renderable);
        }
        public void Remove(ICompositeRenderable compositeRenderable)
        {
            foreach (var renderable in compositeRenderable.GetParts())
                _pointCloudObjects.Remove(renderable);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
