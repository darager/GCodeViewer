using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;

namespace GCodeViewer.Objects
{
    public class FileChooser : IFileChooser
    {
        private IFile File;

        public FileChooser()
        {
        }

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