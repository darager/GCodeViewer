using GCodeViewer.Interfaces.FileAccess;
using System.IO;

namespace GCodeViewer.Objects
{
    public class TextFile : IFile
    {
        private string FilePath;

        public TextFile(string path)
        {
            this.FilePath = path;
        }

        public FileStream GetFileStream()
        {
            return new FileStream(FilePath, FileMode.OpenOrCreate);
        }
    }
}
