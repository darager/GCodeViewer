using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public class BasicScene : IViewerScene
    {
        public IRenderService RenderService { get; private set; }

        #region Printbed Parts

        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);

        #endregion

        public List<ICompositeRenderable> AllRenderables = new List<ICompositeRenderable>();

        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible == value) return;
                _visible = value;

                UpdateVisibility(_visible);
            }
        }

        private bool _visible;
        private float _scalingFactor;

        public BasicScene(IRenderService renderService, Settings settings)
        {
            this.RenderService = renderService;

            AllRenderables.Add(_printbed);
            AllRenderables.Add(_coordinateSystem);

            SetPrintBedDiameter(settings.PrinterDimensions.PrintBedDiameter);
        }

        private void UpdateVisibility(bool visible)
        {
            if (Visible)
                AllRenderables.ForEach(r => RenderService.Add(r));
            else
                AllRenderables.ForEach(r => RenderService.Remove(r));
        }

        public void Add(ICompositeRenderable renderable)
        {
        }

        public void Remove(ICompositeRenderable renderable)
        {
        }

        public void SetOffset(ICompositeRenderable renderable, Point3D offset)
        {
        }

        public void SetPrintBedDiameter(float printBedDiameter)
        {
            float scalingFactor = 2 / printBedDiameter;

            if (_scalingFactor == scalingFactor)
                return;

            _scalingFactor = scalingFactor;
        }
    }
}
