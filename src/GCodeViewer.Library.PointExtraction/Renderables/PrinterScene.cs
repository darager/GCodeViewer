
using System;
using System.Drawing;
using GCodeViewer.Library.Renderables.Things;

namespace GCodeViewer.Library.Renderables
{
    public class PrinterScene
    {
        private float _scalingFactor;

        #region Printer Components
        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);
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
            throw new NotImplementedException();
        }
        public void UpdateAAxisOffset(float aAxisOffset)
        {
            throw new NotImplementedException();
        }
    }
}
