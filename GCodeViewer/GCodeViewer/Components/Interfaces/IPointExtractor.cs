using GCodeViewer.Interfaces.ViewModels;
using System.Windows.Media.Media3D;

namespace GCodeViewer.Interfaces
{
    public interface IPointExtractor
    {
        ITextViewModel TextViewModel { get; set; }
        Point3DCollection ExtractPoints(int startLine, int stopLine);
    }
}
