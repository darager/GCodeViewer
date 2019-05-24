using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;

namespace GCodeViewer.Objects
{
    public class FileChooser : IFileChooser
    {
        private IFile _File;
        public IFile File
        {
            get { return _File; }
        }

        public FileChooser(IFile cacheFile)
        {
            SwapFile(cacheFile);
        }

        public IFile GetFile()
        {
            return _File;
        }
        public void SwapFile(IFile file)
        {
            if (file != _File)
            {
                _File = file;
                CallEvent(_File);
            }
        }

        void CallEvent(IFile file)
        {
            var handler = FileSwapped;

            if (handler != null)
                FileSwapped(this, new FileSwappedEventArgs(file));
        }
        public event FileSwappedEventHandler FileSwapped;
    }
}