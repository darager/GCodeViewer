namespace GCodeViewer.Abstractions.FileAccess
{
    public interface IFileChooser
    {
        IFile GetFile();
        void SwapFile(IFile file);
    }
}
