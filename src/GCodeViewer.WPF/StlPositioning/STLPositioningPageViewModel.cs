using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Input;
using GCodeViewer.Library;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.StlPositioning
{
    public class STLPositioningPageViewModel : INotifyPropertyChanged
    {
        private IViewerScene _scene;
        private ICompositeRenderable _model;
        private PageNavigationService _pageNavigation;

        public ICommand Cancel { get; private set; }

        public STLPositioningPageViewModel(IViewerScene scene, PageNavigationService navService)
        {
            _scene = scene;
            _pageNavigation = navService;

            this.Cancel = new RelayCommand(RemoveModelFromView);
        }

        public void LoadSTL(string filePath)
        {
            var file = new STLFile(filePath);
            List<Mesh> meshes = file.LoadMeshes();

            _model = new Wireframe(meshes[0], Color.GreenYellow, Color.DarkGreen);

            _scene.Add(_model, new Point3D(0, 0, 0));
        }

        private void RemoveModelFromView(object _)
        {
            _scene.Remove(_model);
            _model = null;
            _pageNavigation.GoBack();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
