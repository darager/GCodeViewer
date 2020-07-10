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
        public float XOffset
        {
            get => _xOffset;
            set
            {
                if (_xOffset == value)
                    return;

                _xOffset = value;
                OnPropertyChanged("XOffset");
                SetModelOffset();
            }
        }

        private float _xOffset;

        public float YOffset
        {
            get => _yOffset;
            set
            {
                if (_yOffset == value)
                    return;

                _yOffset = value;
                OnPropertyChanged("YOffset");
                SetModelOffset();
            }
        }

        private float _yOffset;

        public float ZOffset
        {
            get => _zOffset;
            set
            {
                if (_zOffset == value)
                    return;

                _zOffset = value;
                OnPropertyChanged("ZOffset");
                SetModelOffset();
            }
        }

        private float _zOffset;

        public ICommand Cancel { get; private set; }

        private ICompositeRenderable _model;

        private IViewerScene _scene;
        private PageNavigationService _pageNavigation;

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

            _model = new Wireframe(meshes[0], Color.GreenYellow, Color.DarkSlateGray);

            _scene.Add(_model, new Point3D(0, 0, 0), (0, 0, 0));
        }

        private void RemoveModelFromView(object _)
        {
            _scene.Remove(_model);
            _model = null;
            _pageNavigation.GoBack();
        }

        private void SetModelOffset()
        {
            _scene.UpdateOffsetAndRotation(_model, new Point3D(XOffset, YOffset, ZOffset), (0, 0, 0)); ////////////////////// HACK HACK
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
