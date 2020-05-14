using System.Collections.Generic;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Renderables
{
    public interface ICompositeRenderable
    {
        void AddTo(ICollection<Renderable> collection);
    }
}
