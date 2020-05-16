using System.Collections.Generic;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public interface ICompositeRenderable
    {
        IEnumerable<Renderable> GetParts();
    }
}
