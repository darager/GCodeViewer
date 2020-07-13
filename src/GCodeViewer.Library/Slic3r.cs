using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GCodeViewer.Library
{
    public class Slic3r
    {
        public string ExecutablePath { get; private set; }

        public Slic3r(string executablePath)
        {
            ExecutablePath = executablePath;
        }

        public string Help()
        {
            string strCmdText = ExecutablePath + "--help";
            System.Diagnostics.Process.Start("CMD.exe", "dir");

            return string.Empty;
        }
    }
}
