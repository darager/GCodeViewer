using GCodeViewer.Interfaces.FileAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface IToolbarViewModel
    {
        ITextViewModel TextViewModel { get; set; }
        IFileSaver FileSaver { get; set; }
        IFileChooser FileChooser { get; set; }

        void Save();
        void SaveAs();
        void Open();
        void Close();
    }
}
