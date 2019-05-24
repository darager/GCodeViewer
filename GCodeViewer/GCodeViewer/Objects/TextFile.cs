using GCodeViewer.Interfaces.FileAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.Objects
{
    public class TextFile : IFile
    {
        public string FilePath { get; set; }

        public TextFile(string path)
        {
            this.FilePath = path;
        }

        public string[] GetContent()
        {
            string[] content = new string[0];

            if (File.Exists(FilePath))
                content = File.ReadAllLines(FilePath);

            return content;
        }
    }
}
