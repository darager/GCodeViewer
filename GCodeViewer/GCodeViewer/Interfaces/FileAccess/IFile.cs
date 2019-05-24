using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.Interfaces.FileAccess
{
    public interface IFile
    {
        string FilePath { get; }
        string[] GetContent();
    }
}
