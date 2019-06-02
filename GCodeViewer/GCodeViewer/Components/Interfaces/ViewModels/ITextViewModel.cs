using GCodeViewer.Interfaces.FileAccess;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface ITextViewModel
    {
        ITextBuffer FileBuffer { get; set; }
        string[] FileContent { get; set; }

        void LoadBufferContent();
        void ChangeLine(int lineIndex, string content);
        string[] GetCurrentContent();
        bool IsCurrentStateSaved();
        bool IsFileLoaded();
    }
}
