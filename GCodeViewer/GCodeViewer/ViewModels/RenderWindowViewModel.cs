using System;
using System.Windows.Media.Media3D;
using GCodeViewer.Interfaces;
using GCodeViewer.Interfaces.ViewModels;

namespace GCodeViewer.ViewModels
{
    public class RenderWindowViewModel : IRenderWindowViewModel
    {
        public IPointExtractor PointExtractor { get; set; }

        public RenderWindowViewModel(IPointExtractor pointExtractor)
        {
            this.PointExtractor = pointExtractor;
        }
        public void Render(Point3DCollection points)
        {
            throw new NotImplementedException();
        }
    }
}
