using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.Viewer3D;

namespace GCodeViewer.Library.Renderables.Things
{
    public class PointCloud : ICompositeRenderable
    {
        private Renderable _points;

        public PointCloud(IEnumerable<Point3D> points)
        {
            _points = new Renderable(Color.GreenYellow, points, RenderableType.Points);
        }

        public IEnumerable<Renderable> GetParts()
        {
            return new List<Renderable> { _points };
        }
    }
}
