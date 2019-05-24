using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;

namespace GCodeViewer.ViewModels
{
    public class ToolbarViewModel : IToolbarViewModel
    {
        public ITextViewModel TextViewModel { get; set; }
        public IFileSaver FileSaver { get; set; }
        public IFileChooser FileChooser { get; set; }

        public ToolbarViewModel(ITextViewModel textVieModel, IFileSaver fileSaver, IFileChooser fileChooser)
        {
            this.TextViewModel = textVieModel;
            this.FileChooser = fileChooser;
            this.FileSaver = FileSaver;
        }

        public void Close()
        {
        }
        public void Open()
        {
        }
        public void Save()
        {
        }
        public void SaveAs()
        {
        }
    }
}
