using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class CoordinateSystem : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CoordinateSystem(Point3D position, float length)
        {
            float radius = length / 25;
            int opacity = 255;

            // X-Axis
            _parts.Add(Cylinder.With()
                               .Color(Color.FromArgb(opacity, 255, 0, 0))
                               .Position(position)
                               .Height(length)
                               .Radius(radius)
                               .RotationY(90)
                               .Build());
            // Y-Axis
            _parts.Add(Cylinder.With()
                               .Color(Color.FromArgb(opacity, 0, 255, 0))
                               .Position(position)
                               .Height(length)
                               .Radius(radius)
                               .RotationX(-90)
                               .Build());
            // Z-Axis
            _parts.Add(Cylinder.With()
                               .Color(Color.FromArgb(opacity, 0, 0, 255))
                               .Position(position)
                               .Height(length)
                               .Radius(radius)
                               .Build());
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
