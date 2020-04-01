using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using GCodeViewer.Library.PointExtraction;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest
{
    public class PointCloudViewModel : INotifyPropertyChanged
    {
        #region Vertices
        float[] _coordinateSytemVertices =
        {
            0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
            0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
            0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f  // Z
        };
        private readonly float[] _smallCubeVertices =
        {
            0.25f, 0.25f, 0.25f,   0.7f,  0.25f, 0.25f,
            0.25f, 0.25f, 0.25f,   0.25f, 0.25f, 0.7f,
            0.7f,  0.25f, 0.7f,    0.7f,  0.25f, 0.25f,
            0.7f,  0.25f, 0.7f,    0.25f, 0.25f, 0.7f,
            0.25f, 0.7f,  0.25f,   0.7f,  0.7f,  0.25f,
            0.25f, 0.7f,  0.25f,   0.25f, 0.7f,  0.7f,
            0.7f,  0.7f,  0.7f,    0.7f,  0.7f,  0.25f,
            0.7f,  0.7f,  0.7f,    0.25f, 0.7f,  0.7f,
            0.25f, 0.25f, 0.25f,   0.25f, 0.7f,  0.25f,
            0.25f, 0.25f, 0.7f,    0.25f, 0.7f,  0.7f,
            0.7f,  0.25f, 0.25f,   0.7f,  0.7f,  0.25f,
            0.7f,  0.25f, 0.7f,    0.7f,  0.7f,  0.7f
        };
        private readonly float[] _bigCubeVertices =
        {
           -1.0f, -1.0f, -1.0f,    1.0f, -1.0f, -1.0f,
           -1.0f, -1.0f, -1.0f,   -1.0f, -1.0f,  1.0f,
            1.0f, -1.0f,  1.0f,    1.0f, -1.0f, -1.0f,
            1.0f, -1.0f,  1.0f,   -1.0f, -1.0f,  1.0f,
           -1.0f,  1.0f, -1.0f,    1.0f,  1.0f, -1.0f,
           -1.0f,  1.0f, -1.0f,   -1.0f,  1.0f,  1.0f,
            1.0f,  1.0f,  1.0f,    1.0f,  1.0f, -1.0f,
            1.0f,  1.0f,  1.0f,   -1.0f,  1.0f,  1.0f,
           -1.0f, -1.0f, -1.0f,   -1.0f,  1.0f, -1.0f,
           -1.0f, -1.0f,  1.0f,   -1.0f,  1.0f,  1.0f,
            1.0f, -1.0f, -1.0f,    1.0f,  1.0f, -1.0f,
            1.0f, -1.0f,  1.0f,    1.0f,  1.0f,  1.0f
        };
        #endregion

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
            var extractor = new PointExtractor();
            var points = extractor.ExtractUniquePoints(content);

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
