using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public static class RenderableExtensions
    {
        public static ICompositeRenderable AsComposite(this Renderable @this)
        {
            var composite = new ScaledAndOffsetRenderables();
            composite.Add(@this);

            return composite;
        }
    }
}
