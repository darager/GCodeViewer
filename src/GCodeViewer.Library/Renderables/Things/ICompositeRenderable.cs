using System.Collections.Generic;
using GCodeViewer.WPF.Controls.Viewer3D;

namespace GCodeViewer.Library.Renderables.Things
{
    public interface ICompositeRenderable
    {
        IEnumerable<Renderable> GetParts();
    }
}
