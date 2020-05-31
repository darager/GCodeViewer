using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables.Things;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.Library.Renderables
{
    public class BasicScene : IViewerScene
    {
        public IRenderService RenderService { get; private set; }

        #region Printbed Parts

        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);

        #endregion

        private Dictionary<ICompositeRenderable, (ICompositeRenderable, Point3D)> _renderables;

        private float _scalingFactor;

        public BasicScene(IRenderService renderService, Settings settings)
        {
            RenderService = renderService;
            _renderables = new Dictionary<ICompositeRenderable, (ICompositeRenderable, Point3D)>();

            RenderService.Add(_printbed);
            RenderService.Add(_coordinateSystem);

            SetPrintBedDiameter(settings.PrinterDimensions.PrintBedDiameter);
        }

        public void Add(ICompositeRenderable renderable, Point3D offset)
        {
            var builder = new ScaledAndOffsetRenderableBuilder(renderable);
            builder.SetScalingFactor(_scalingFactor);
            builder.SetOffset(offset);
            var offsetRenderable = builder.Build();

            _renderables.Add(renderable, (offsetRenderable, offset));
            RenderService.Add(offsetRenderable);
        }

        public void Remove(ICompositeRenderable renderable)
        {
            if (!(_renderables.ContainsKey(renderable))) return;

            var offsetRenderable = _renderables[renderable].Item1;
            _renderables.Remove(renderable);

            RenderService.Remove(offsetRenderable);
        }

        public void UpdateOffset(ICompositeRenderable renderable, Point3D offset)
        {
            Remove(renderable);
            Add(renderable, offset);
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
            foreach (var key in _renderables.Keys)
            {
                Remove(key);
                Add(key, _renderables[key].Item2);
            }
        }
    }
}
