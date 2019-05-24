using GCodeViewer.Interfaces;
using GCodeViewer.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

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
