using GCodeViewer.Abstractions.FileAccess;

namespace GCodeViewer.Components.FileAccess
{
    public class FileChooser : IFileChooser
    {
        private IFile File;

        public IFile GetFile()
        {
            return File;
        }
        public void SwapFile(IFile file)
        {
            File = file;
        }
    }
}