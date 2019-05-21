using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.RenderWindow.Utils.Interfaces
{
    public interface ITextViewModel
    {
        IFileBuffer fileBuffer { get; set; }
        void RestoreOriginalContent();
        void ChangeLine(int lineIndex, string content);
    }
}
