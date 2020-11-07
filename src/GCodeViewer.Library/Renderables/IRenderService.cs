using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public interface IRenderService
    {
        void Add(ICompositeRenderable renderable);

        void Remove(ICompositeRenderable renderable);
    }
}
