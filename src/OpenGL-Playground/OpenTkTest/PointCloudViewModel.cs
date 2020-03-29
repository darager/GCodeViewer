using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using GCodeViewer.OpenTK.Helpers.Objects3D;
using GCodeViewer.WPF.Controls;

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

        public ObservableCollection<Object3D> PointCloudObjects = new ObservableCollection<Object3D>();

        public PointCloudViewModel(PointCloudViewer pclViewer)
        {
            pclViewer.Add3DObject(new Object3D(Color.Red, _coordinateSytemVertices, ObjectType.Lines));
            pclViewer.Add3DObject(new Object3D(Color.GreenYellow, _smallCubeVertices, ObjectType.Lines));
            pclViewer.Add3DObject(new Object3D(Color.GreenYellow, _bigCubeVertices, ObjectType.Lines));

            var rnd = new Random();
            int count = 1000;
            var randomPointVertices = Enumerable.Range(0, count * 3)
                .Select(_ => rnd.NextDouble())
                .Select(r => (float)r * 2 - 1)
                .ToArray();
            pclViewer.Add3DObject(new Object3D(Color.CornflowerBlue, randomPointVertices, ObjectType.Points));
        }
    }
}
