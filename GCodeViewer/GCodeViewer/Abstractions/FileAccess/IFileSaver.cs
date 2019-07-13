

using GCodeViewer.WPF.Abstractions.ViewModels;

namespace GCodeViewer.WPF.Abstractions.FileAccess
{
    public interface IFileSaver
    {
        ITextViewModel TextViewModel { get; set; }
        IFileChooser FileChooser { get; set; }

        void SaveCurrentFile();
        void SaveToFile(IFile file);
    }
}
