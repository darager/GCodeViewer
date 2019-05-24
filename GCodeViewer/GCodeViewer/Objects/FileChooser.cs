using GCodeViewer.Interfaces.FileAccess;

namespace GCodeViewer.Objects
{
    public class FileChooser : IFileChooser
    {
        public IFile File { get; set; }

        public IFile GetFile()
        {
            return File;
        }
        public void SwapFile(IFile file)
        {
            this.File = file;
        }
    }
}
