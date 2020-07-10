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
                               .Radius(1f)
                               .Color(highlightColor)
                               .Build());
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
