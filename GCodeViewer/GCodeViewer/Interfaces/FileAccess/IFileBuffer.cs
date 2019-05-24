using GCodeViewer.Interfaces.FileAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.Interfaces.FileAccess
{
    public interface IFileBuffer
    {
        IFileChooser FileChooser { get; set; }

        string[] OriginalText { get; set; }
    }
}
