namespace GCodeViewer.Abstractions.FileAccess
{
    public interface ITextBuffer
    {
        IFileChooser FileChooser { get; set; }

        void LoadFileContent();
        string[] GetContent();
    }
}
