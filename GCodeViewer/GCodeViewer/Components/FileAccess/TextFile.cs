using GCodeViewer.Abstractions.FileAccess;
using System.IO;

namespace GCodeViewer.Components.FileAccess
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
