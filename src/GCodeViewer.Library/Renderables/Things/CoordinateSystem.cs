using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.Renderables.Shapes;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables.Things
{
    public class CoordinateSystem : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CoordinateSystem(Point3D position, float length, int opacity = 255)
        {
            var r = Color.FromArgb(opacity, 255, 0, 0);
            var g = Color.FromArgb(opacity, 0, 255, 0);
            var b = Color.FromArgb(opacity, 0, 0, 255);

            var cylinder = Cylinder.With()
                                   .Position(position)
                                   .Height(length)
                                   .Radius(length / 25);
            // X-Axis
            _parts.Add(cylinder.Color(r)
                               .RotationX(0)
                               .RotationY(90)
                               .Build());
            // Y-Axis
            _parts.Add(cylinder.Color(g)
                               .RotationX(-90)
                               .RotationY(0)
                               .Build());
            // Z-Axis
            _parts.Add(cylinder.Color(b)
                               .RotationX(0)
                               .RotationY(0)
                               .Build());
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
