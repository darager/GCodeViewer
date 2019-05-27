using System;
using System.IO;
using GCodeViewer.Interfaces.FileAccess;

namespace GCodeViewer.Objects
{
    public class CacheFile : IFile
    {
        public string FilePath => Path.Combine(Directory.GetCurrentDirectory(), "CACHEFILE.txt");

        public CacheFile()
        {

        }

        public string[] GetContent()
        {
            string[] content = new string[0];

            if (File.Exists(FilePath))
                content = File.ReadAllLines(FilePath);

            return content;
        }
        public FileStream GetFileStream()
        {
            throw new NotImplementedException();
        }
    }
}
