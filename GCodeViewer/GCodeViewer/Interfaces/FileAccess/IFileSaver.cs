using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;

namespace GCodeViewer.Interfaces.FileAccess
{
    public interface IFileSaver
    {
        ITextViewModel TextViewModel { get; set; }
        IFileChooser FileChooser { get; set; }

        void WriteToFile();
    }
}
