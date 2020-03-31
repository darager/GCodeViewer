using GCodeViewer.WPF.Abstractions.FileAccess;

namespace GCodeViewer.WPF.Components.FileAccess
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