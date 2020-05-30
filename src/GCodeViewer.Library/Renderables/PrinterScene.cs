using System.Drawing;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public class BasicScene : IViewerScene
    {
        #region Printer Components

        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);

        #endregion

        public IRenderService RenderService { get; private set; }

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

        private void UpdateVisibility(bool visible)
        {
        }

        public BasicScene(IRenderService renderService, Settings settings)
        {
            this.RenderService = renderService;

            SetPrintBedDiameter(settings.PrinterDimensions.PrintBedDiameter);
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
