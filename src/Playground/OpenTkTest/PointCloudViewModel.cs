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


            //var content = File.ReadLines(@"C:\Users\florager\source\repos\darager\GCodeViewer\src\Examples\SinkingBenchy.gcode");
            //var extractor = new GCodePointExtractor();
            //var points = extractor.ExtractPoints(content);

            //var verts = new List<float>();
            //foreach (var point in points)
            //{
            //    verts.Add(point.Y);
            //    verts.Add(point.X);
            //    verts.Add(point.Z);
            //}

            //float max = verts.Max();
            //float min = verts.Min();

            //var vertices = verts
            //    .Skip(3000)
            //    //.Take(9000000)
            //    //.Select(n => Scale(n, min, max, 0, 1))
            //    .ToArray();
            //PointCloudObjects.Add(new Renderable(Color.GreenYellow, vertices, RenderableType.Points));
        }

        private float Scale(float value, float min, float max, int minScale, int maxScale)
        {
            return minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
