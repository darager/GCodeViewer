using GCodeViewer.Abstractions.FileAccess;
using System.Collections.ObjectModel;

namespace GCodeViewer.Abstractions.ViewModels
{
    public interface ITextViewModel
    {
        ITextBuffer FileBuffer { get; set; }
        ObservableCollection<string> FileContent { get; set; }

        void LoadFileContent();
        void ChangeLine(int lineIndex, string content);
        string[] GetCurrentContent();
        bool IsCurrentStateSaved();
        bool IsFileLoaded();
    }
}
