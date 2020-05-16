﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GCodeViewer.Helpers;
using GCodeViewer.Library.GCodeParsing;
using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.ViewModels
{
    public class Viewer3DViewModel : INotifyPropertyChanged
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

        private AxisValueFilter _filter = new AxisValueFilter();
        private GCodeAxisValueExtractor _extractor = new GCodeAxisValueExtractor();

        private Renderable _model;
        private ICompositeRenderable _printbed = new CircularPrintbed(1.0f, Color.DarkGray, Color.White);

        public Viewer3DViewModel()
        {
            PointCloudObjects = new ObservableCollection<Renderable>();

            AddCoordinateSystem();

            // TODO: the scaling of the renderables should be according to the printbed at first when the height has not changed yet
            _printbed.AddTo(PointCloudObjects);
        }

        public async void Update3DModel(string newText)
        {
            var uiThread = Application.Current.Dispatcher;

            await Task.Factory.StartNew(() =>
            {
                var content = newText.Split();
                var allPoints = _extractor.ExtractPrinterAxisValues(content);
                var filteredPoints = _filter.FilterNonExtrudingValues(allPoints);

                var verts = new List<float>();
                foreach (var point in filteredPoints)
                {
                    verts.Add(point.Y);
                    verts.Add(point.Z);
                    verts.Add(point.X);
                }
                float max = verts.Max();
                float min = verts.Min();

                var points = new List<Point3D>();
                foreach (var point in filteredPoints)
                {
                    float x = point.Y.Scale(min, max, -1, 1);
                    float y = point.Z.Scale(min, max, -1, 1) + 1;
                    float z = point.X.Scale(min, max, -1, 1);
                    points.Add(new Point3D(x, y, z));
                }

                uiThread.Invoke(() =>
                {
                    if (_model != null)
                        PointCloudObjects.Remove(_model);

                    _model = new Renderable(Color.GreenYellow, points, RenderableType.Points);
                    PointCloudObjects.Add(_model);
                });
            }).ConfigureAwait(false);
        }

        private void AddCoordinateSystem()
        {
            var coordinateSytemVertices = new List<Point3D>()
            {
                new Point3D(0.0f, 0.0f, 0.0f),
                new Point3D(0.1f, 0.0f, 0.0f), // X
                new Point3D(0.0f, 0.0f, 0.0f),
                new Point3D(0.0f, 0.1f, 0.0f), // Y
                new Point3D(0.0f, 0.0f, 0.0f),
                new Point3D(0.0f, 0.0f, 0.1f)  // Z
            };
            PointCloudObjects.Add(new Renderable(Color.Red, coordinateSytemVertices, RenderableType.Lines));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
