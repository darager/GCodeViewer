using System.Collections.Generic;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class ScaledAndOffsetRenderables : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public void Add(Renderable renderable)
        {
            _parts.Add(renderable);
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
