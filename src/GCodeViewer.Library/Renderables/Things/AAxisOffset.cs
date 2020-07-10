using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.Renderables.Shapes;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables.Things
{
    public class AAxisOffset : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public AAxisOffset(float offset, Color highlightColor, Color color)
        {
            _parts.Add(Cylinder.With()
                               .Height(-offset)
                               .RotationX(-90)
                               .Radius(1.5f)
                               .Color(highlightColor)
                               .Build());

            _parts.Add(Cylinder.With()
                               .Position(new Point3D(-25, -offset, 0))
                               .Height(50)
                               .RotationY(90)
                               .Radius(1.5f)
                               .Color(color)
                               .Build());
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
