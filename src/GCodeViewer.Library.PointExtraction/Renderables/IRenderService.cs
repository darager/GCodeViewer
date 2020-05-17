using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public interface IRenderService
    {
        void Add(Renderable renderable);
        void Add(ICompositeRenderable renderable);

        void Remove(Renderable renderable);
        void Remove(ICompositeRenderable renderable);
    }
}
