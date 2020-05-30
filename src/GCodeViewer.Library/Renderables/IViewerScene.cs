using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public interface IViewerScene
    {
        public IRenderService RenderService { get; }

        public void SetPrintBedDiameter(float printBedDiameter);

        public void Add(ICompositeRenderable renderable);

        public void Remove(ICompositeRenderable renderable);

        public void SetOffset(ICompositeRenderable renderable, Point3D offset);
    }
}
