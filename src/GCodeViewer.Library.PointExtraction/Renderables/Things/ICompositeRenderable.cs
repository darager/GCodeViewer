using System.Collections.Generic;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables.Things
{
    public interface ICompositeRenderable
    {
        IEnumerable<Renderable> GetParts();
    }
}
