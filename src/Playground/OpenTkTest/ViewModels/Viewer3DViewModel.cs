using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GCodeViewer.Library;
using GCodeViewer.WPF.Controls.PointCloud;
using OpenTkTest.Printbeds;

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

        public Viewer3DViewModel()
        {
            PointCloudObjects = new ObservableCollection<Renderable>();

            float[] coordinateSytemVertices =
            {
                0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
                0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
                0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f  // Z
            };
            PointCloudObjects.Add(new Renderable(Color.Red, coordinateSytemVertices, RenderableType.Lines));

            // TODO: the scaling of the renderables should be according to the printbed at first when the height has not changed yet
            var printbed = new CircularPrintbed(5, Color.DarkGray, Color.White);
            printbed.AddTo(PointCloudObjects);
        }

        private Renderable _model;
        public async void Update3DModel(string newText)
        {
            var uiThread = Application.Current.Dispatcher;

            await Task.Factory.StartNew(() =>
            {
                var content = newText.Split();
                var extractor = new GCodeAxisValueExtractor();
                var filter = new AxisValueFilter();
                var allPoints = extractor.ExtractPrinterAxisValues(content);
                var points = filter.FilterNonExtrudingValues(allPoints);

                var verts = new List<float>();
                foreach (var point in points)
                {
                    verts.Add(point.Y);
                    verts.Add(point.Z);
                    verts.Add(point.X);
                }
                float max = verts.Max();
                float min = verts.Min();

                verts.Clear();
                foreach (var point in points)
                {
                    verts.Add(Scale(point.Y, min, max, -1, 1));
                    verts.Add(Scale(point.Z, min, max, -1, 1) + 1);
                    verts.Add(Scale(point.X, min, max, -1, 1));
                }

                uiThread.Invoke(() =>
                {
                    if (_model != null)
                        PointCloudObjects.Remove(_model);

                    _model = new Renderable(Color.GreenYellow, verts.ToArray(), RenderableType.Points);
                    PointCloudObjects.Add(_model);
                });
            }).ConfigureAwait(false);
        }

        private float Scale(float value, float min, float max, int minScale, int maxScale)
        {
            return minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
