using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class CoordinateSystem : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CoordinateSystem(Point3D position, float length, float rotationX, float rotationY)
        {
            float radius = length / 10;

            // X-Axis
            _parts.Add(Cylinder.With()
                               .Color(Color.Red)
                               .Position(position)
                               .Height(length)
                               .Radius(radius)
                               .RotationX(rotationX)
                               .RotationY(90 + rotationY)
                               .Build());
            // Y-Axis
            _parts.Add(Cylinder.With()
                               .Color(Color.Green)
                               .Position(position)
                               .Height(length)
                               .Radius(radius)
                               .RotationX(-90 + rotationX)
                               .RotationY(rotationY)
                               .Build());
            // Z-Axis
            _parts.Add(Cylinder.With()
                               .Color(Color.Blue)
                               .Position(position)
                               .Height(length)
                               .Radius(radius)
                               .RotationX(rotationX)
                               .RotationY(rotationY)
                               .Build());
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
