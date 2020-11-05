using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public class PrinterScene : IViewerScene
    {
        public IRenderService RenderService { get; private set; }

        #region Printbed Parts

        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);

        #endregion

        private Dictionary<ICompositeRenderable, (ICompositeRenderable Renderable, Point3D Offset, (float x, float y, float z) Rotation)> _renderables;

        private float _scalingFactor;

        public PrinterScene(IRenderService renderService, SettingsService settingsService)
        {
            RenderService = renderService;
            _renderables = new Dictionary<ICompositeRenderable, (ICompositeRenderable, Point3D, (float x, float y, float z) Rotation)>();

            var printerBuilder = new ScaledAndOffsetRenderableBuilder(_printbed);
            RenderService.Add(printerBuilder.Build());

            var coordinateSystemBuilder = new ScaledAndOffsetRenderableBuilder(_coordinateSystem);
            RenderService.Add(coordinateSystemBuilder.Build());

            float diameter = settingsService.Settings.PrinterDimensions.PrintBedDiameter;
            SetPrintBedDiameter(diameter);
        }

        public void Add(ICompositeRenderable renderable, Point3D offset, (float x, float y, float z) rotation)
        {
            var builder = new ScaledAndOffsetRenderableBuilder(renderable);
            builder.SetScalingFactor(_scalingFactor);
            builder.SetOffset(offset);
            builder.SetRotation(rotation);
            var offsetRenderable = builder.Build();

            _renderables.Add(renderable, (offsetRenderable, offset, rotation));
            RenderService.Add(offsetRenderable);
        }

        public void Remove(ICompositeRenderable renderable)
        {
            if (!(_renderables.ContainsKey(renderable))) return;

            var offsetRenderable = _renderables[renderable].Renderable;
            _renderables.Remove(renderable);

            RenderService.Remove(offsetRenderable);
        }

        public void UpdateOffsetAndRotation(ICompositeRenderable renderable, Point3D offset, (float x, float y, float z) rotation)
        {
            Remove(renderable);
            Add(renderable, offset, rotation);
        }

        public void SetPrintBedDiameter(float printBedDiameter)
        {
            float scalingFactor = 2 / printBedDiameter;

            if (_scalingFactor == scalingFactor)
                return;

            _scalingFactor = scalingFactor;

            UpdateEveryRenderable();
        }

        private void UpdateEveryRenderable()
        {
            foreach (var renderable in _renderables.Keys.ToList())
            {
                var offset = _renderables[renderable].Offset;
                var rotation = _renderables[renderable].Rotation;

                Remove(renderable);
                Add(renderable, offset, rotation);
            }
        }
    }
}
