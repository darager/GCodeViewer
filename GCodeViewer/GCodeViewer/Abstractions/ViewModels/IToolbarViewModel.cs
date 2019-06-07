using System.Windows.Input;

namespace GCodeViewer.Abstractions.ViewModels
{
    public interface IToolbarViewModel
    {
        ICommand OpenFileCommand { get; set; }
        ICommand SaveFileCommand { get; set; }
        ICommand SaveAsCommand { get; set; }
    }
}
