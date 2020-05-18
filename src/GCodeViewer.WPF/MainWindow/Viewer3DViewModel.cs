using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.WPF.MainWindow
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
