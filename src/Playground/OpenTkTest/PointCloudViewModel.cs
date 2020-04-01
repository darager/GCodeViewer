using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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

            var content = File.ReadLines(@"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode");
            var extractor = new GCodePointExtractor();
            var points = extractor.ExtractPoints(content);

            var verts = new List<float>();
            foreach (var point in points)
            {
                verts.Add(point.X);
                verts.Add(point.Y);
                verts.Add(point.Z);
            }

            float max = verts.Max();
            float min = verts.Min();

            var vertices = verts
                .Take(9000000)
                .Select(n => Scale(n, min, max, -1, 1))
                .Reverse()
                .ToArray();
            PointCloudObjects.Add(new Renderable(Color.GreenYellow, vertices, RenderableType.Points));
        }

        private float Scale(float value, float min, float max, int minScale, int maxScale)
        {
            return minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
