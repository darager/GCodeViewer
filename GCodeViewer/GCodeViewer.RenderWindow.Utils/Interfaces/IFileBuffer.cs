using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.RenderWindow.Utils.Interfaces
{
    public interface IFileBuffer
    {
        string[] OriginalText { get; set; }
    }
}
