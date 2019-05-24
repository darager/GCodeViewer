using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.Interfaces.FileAccess
{
    public interface IFileChooser
    {
        IFile File { get; set; }

        void SwapFile(IFile file);
        IFile GetFile();
    }
}
