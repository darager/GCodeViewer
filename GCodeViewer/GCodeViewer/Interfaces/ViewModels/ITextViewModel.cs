using GCodeViewer.Interfaces.FileAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface ITextViewModel
    {
        ITextBuffer FileBuffer { get; set; }

        void RestoreOriginalContent();
        void ChangeLine(int lineIndex, string content);
        string[] GetCurrentContent();
    }
}
