
using System;
using System.Drawing;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class PrinterScene
    {
        private float _scalingFactor;

        #region Printer Components
        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);
        private Renderable _aAxis;
        #endregion

        private IRenderService _renderService;

        public PrinterScene(IRenderService renderService)
        {
            _renderService = renderService;

            _renderService.Add(_printbed);
            _renderService.Add(_coordinateSystem);
        }

        public void SetPrintBedDiameter(float printBedDiameter)
        {
        }
        public void UpdateAAxisOffset(float aAxisOffset)
        {
        }
    }
}
