using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using System.Windows.Input;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface IToolbarViewModel
    {
        ITextViewModel TextViewModel { get; set; }
        IFileSaver FileSaver { get; set; }
        IFileChooser FileChooser { get; set; }
        ITextBuffer TextBuffer { get; set; }

        ICommand OpenFileCommand { get; set; }
        ICommand SaveFileCommand { get; set; }
        ICommand SaveAsCommand { get; set; }
    }
}
