using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;
using Ninject.Activation;

namespace GCodeViewer.WPF.MainWindow
{
    public class Viewer3DViewModel : INotifyPropertyChanged, IRenderService, IProvider
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

        public void Add(ICompositeRenderable compositeRenderable)
        {
            foreach (var renderable in compositeRenderable.GetParts())
                PointCloudObjects.Add(renderable);
        }

        public void Remove(ICompositeRenderable compositeRenderable)
        {
            foreach (var renderable in compositeRenderable.GetParts())
                PointCloudObjects.Remove(renderable);
        }

        public Type Type => typeof(Viewer3DViewModel);

        public object Create(IContext context) => this;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
