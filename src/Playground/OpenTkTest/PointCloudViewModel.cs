using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using GCodeViewer.Library;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest
{
    public class PointCloudViewModel : INotifyPropertyChanged
    {
        float[] _coordinateSytemVertices =
        {
            0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
            0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
            0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f  // Z
        };


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

        public PointCloudViewModel()
        {
            PointCloudObjects = new ObservableCollection<Renderable>();

            PointCloudObjects.Add(new Renderable(Color.Red, _coordinateSytemVertices, RenderableType.Lines));

            #region Orientation Grid
            var gridverts = new List<float>();

            int lineCount = 10;
            float xSpacing = 2.0f / lineCount;
            for (int i = 0; i < lineCount + 1; i++)
            {
                float dx = xSpacing * i;
                gridverts.Add(-1 + dx);
                gridverts.Add(0);
                gridverts.Add(1);

                gridverts.Add(-1 + dx);
                gridverts.Add(0);
                gridverts.Add(-1);
            }
            float ySpacing = 2.0f / lineCount;
            for (int i = 0; i < lineCount + 1; i++)
            {
                float dy = xSpacing * i;
                gridverts.Add(-1);
                gridverts.Add(0);
                gridverts.Add(-1 + dy);

                gridverts.Add(1);
                gridverts.Add(0);
                gridverts.Add(-1 + dy);
            }
            PointCloudObjects.Add(new Renderable(Color.LightGray, gridverts.ToArray(), RenderableType.Lines));
            #endregion
        }

        private Renderable _model;
        public void Update3DModel(string newText)
        {
            var content = newText.Split();
            var extractor = new GCodeAxisValueExtractor();
            var points = extractor.ExtractPrinterAxisValues(content);

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

            if (_model != null)
                PointCloudObjects.Remove(_model);

            _model = new Renderable(Color.GreenYellow, verts.ToArray(), RenderableType.Points);
            PointCloudObjects.Add(_model);
        }

        private float Scale(float value, float min, float max, int minScale, int maxScale)
        {
            return minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
