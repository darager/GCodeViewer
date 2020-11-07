using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.Renderables.Shapes;
using GCodeViewer.WPF.Controls.Viewer3D;

namespace GCodeViewer.Library.Renderables.Things
{
    public class AAxisOffset : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public AAxisOffset(float offset, Color highlightColor, Color color)
        {
            _parts.Add(Cylinder.With()
                               .Height(-offset)
                               .RotationX(0)
                               .Radius(1.5f)
                               .Color(highlightColor)
                               .Build());

            _parts.Add(Cylinder.With()
                               .Position(new Point3D(-25, 0, -offset))
                               .Height(50)
                               .RotationY(90)
                               .Radius(1.5f)
                               .Color(color)
                               .Build());
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
