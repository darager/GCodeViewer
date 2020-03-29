using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using GCodeViewer.WPF.Controls;
using GCodeViewer.OpenTK.Helpers.Renderables;

namespace OpenTkTest
{
    public class PointCloudViewModel
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

        public PointCloudViewModel(PointCloudViewer pclViewer)
        {
            pclViewer.Renderables.Add(new Renderable(Color.Red, _coordinateSytemVertices, RenderableType.Lines));
            pclViewer.Renderables.Add(new Renderable(Color.GreenYellow, _smallCubeVertices, RenderableType.Lines));
            pclViewer.Renderables.Add(new Renderable(Color.GreenYellow, _bigCubeVertices, RenderableType.Lines));

            var rnd = new Random();
            int count = 1000;
            var randomPointVertices = Enumerable.Range(0, count * 3)
                .Select(_ => rnd.NextDouble())
                .Select(r => (float)r * 2 - 1)
                .ToArray();
            pclViewer.Renderables.Add(new Renderable(Color.CornflowerBlue, randomPointVertices, RenderableType.Points));
        }
    }
}
