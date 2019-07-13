using GCodeViewer.WPF.Abstractions.FileAccess;
using System.IO;

namespace GCodeViewer.WPF.Components.FileAccess
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
            var stream = new FileStream(FilePath, FileMode.OpenOrCreate);

            return stream;
        }
    }
}
