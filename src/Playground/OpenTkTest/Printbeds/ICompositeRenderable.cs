using System.Collections.Generic;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Printbeds
{
    public interface ICompositeRenderable
    {
        void AddTo(ICollection<Renderable> collection);
    }
}
