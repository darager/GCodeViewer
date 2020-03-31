using System.Windows.Media.Media3D;

namespace GCodeViewer.WPF.Abstractions.ViewModels
{
    public interface IRenderWindowViewModel
    {
        IPointExtractor PointExtractor { get; set; }
        void Render(Point3DCollection points);
    }
}
