using GCodeViewer.Interfaces.FileAccess.FileChooser;

namespace GCodeViewer.Interfaces.FileAccess
{
    public interface ITextBuffer
    {
        IFileChooser FileChooser { get; set; }

        void LoadFileContent();
        string[] GetContent();
    }
}
