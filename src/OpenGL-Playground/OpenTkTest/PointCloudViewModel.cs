using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using GCodeViewer.OpenTK.Helpers.Renderables;

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

        public ObservableCollection<Renderable> PointCloudObjects = new ObservableCollection<Renderable>();

        public PointCloudViewModel()
        {
            var coordSystem = new Renderable(Color.Red, _coordinateSytemVertices, RenderableType.Lines);
            PointCloudObjects.Add(coordSystem);
            PointCloudObjects.Remove(coordSystem);

            PointCloudObjects.Add(new Renderable(Color.GreenYellow, _smallCubeVertices, RenderableType.Lines));
            PointCloudObjects.Add(new Renderable(Color.GreenYellow, _bigCubeVertices, RenderableType.Lines));

            var rnd = new Random();
            int count = 1000;
            var randomPointVertices = Enumerable.Range(0, count * 3)
                .Select(_ => rnd.NextDouble())
                .Select(r => (float)r * 2 - 1)
                .ToArray();
            PointCloudObjects.Add(new Renderable(Color.CornflowerBlue, randomPointVertices, RenderableType.Points));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PointCloudObjects"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
