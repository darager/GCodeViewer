using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public interface IViewerScene
    {
        public IRenderService RenderService { get; }

        public void SetPrintBedDiameter(float printBedDiameter);

        public void Add(ICompositeRenderable renderable, Point3D offset, (float x, float y, float z) rotation);

        public void Remove(ICompositeRenderable renderable);

        public void UpdateOffsetAndRotation(ICompositeRenderable renderable, Point3D offset, (float x, float y, float z) rotation);
    }
}
