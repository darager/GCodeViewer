namespace GCodeViewer.Interfaces.FileAccess
{
    public interface IFile
    {
        string FilePath { get; }
        string[] GetContent();
    }
}
