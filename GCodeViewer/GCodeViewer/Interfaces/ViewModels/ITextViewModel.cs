using GCodeViewer.Interfaces.FileAccess;
using System.Collections.ObjectModel;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface ITextViewModel
    {
        ITextBuffer FileBuffer { get; set; }
        ObservableCollection<string> FileContent { get; set; }

        void LoadBufferContent();
        void ChangeLine(int lineIndex, string content);
        string[] GetCurrentContent();
        bool IsCurrentFileSaved();
    }
}
