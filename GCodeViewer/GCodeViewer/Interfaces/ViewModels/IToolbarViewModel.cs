using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using System.Windows.Input;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface IToolbarViewModel
    {
        ICommand OpenFileCommand { get; set; }
        ICommand SaveFileCommand { get; set; }
        ICommand SaveAsCommand { get; set; }
    }
}
