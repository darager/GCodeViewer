using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library
{
    public interface IRenderService
    {
        void Add(Renderable renderable);
        void Add(ICompositeRenderable renderable);

        void Remove(Renderable renderable);
        void Remove(ICompositeRenderable renderable);
    }
}
