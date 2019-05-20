using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.RenderWindow.Utils
{
    public interface IFileSelector
    {
        FileStream GCodeFile { get; set; }

        void OpenFile();
        void CloseFile();

        event FileChangedEventHandler FileChanged;
    }
}
