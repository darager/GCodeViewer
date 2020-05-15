using System.Collections.Generic;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public interface ICompositeRenderable
    {
        void AddTo(ICollection<Renderable> collection);
        void RemoveFrom(ICollection<Renderable> collection);
    }
}
