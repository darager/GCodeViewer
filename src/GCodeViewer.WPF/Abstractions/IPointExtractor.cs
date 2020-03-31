using GCodeViewer.WPF.Abstractions.ViewModels;
using System.Windows.Media.Media3D;

namespace GCodeViewer.WPF.Abstractions
{
    public interface IPointExtractor
    {
        ITextViewModel TextViewModel { get; set; }
        Point3DCollection ExtractPoints(int startLine, int stopLine);
    }
}
