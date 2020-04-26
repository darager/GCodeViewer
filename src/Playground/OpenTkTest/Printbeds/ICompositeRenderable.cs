using System.Collections.Generic;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Printbeds
{
    public interface ICompositeRenderable
    {
        List<Renderable> Parts { get; }
    }
}
