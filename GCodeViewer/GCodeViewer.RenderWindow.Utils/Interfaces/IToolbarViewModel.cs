using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.RenderWindow.Utils.Interfaces
{
    public interface IToolbarViewModel
    {
        ITextViewModel textViewModel { get; set; }
        IFileSaver fileSaver { get; set; }
        IFileLoader fileLoader { get; set; }

        void Save();
        void SaveAs();
        void Open();
        void Close();
    }
}
