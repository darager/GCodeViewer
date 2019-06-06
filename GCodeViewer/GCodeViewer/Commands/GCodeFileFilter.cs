using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.ViewModels.Commands
{
    public static class GCodeFileFilter
    {
        public static string Filter = "gcode files (*.gcode)|*.gcode|text files (*.txt)|*.txt|all files (*.*)|*.*";
        public static string StandardFileExtension = ".gcode";
    }
}
