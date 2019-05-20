using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.RenderWindow.Utils
{
    public class FileChangedEventArgs : EventArgs
    {
        public string FilePath { get; set; }

        public FileChangedEventArgs(string path)
        {
            this.FilePath = path;
        }
    }
}