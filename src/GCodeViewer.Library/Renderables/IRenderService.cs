using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public interface IRenderService
    {
        void Add(ICompositeRenderable renderable);

        void Remove(ICompositeRenderable renderable);
    }
}
