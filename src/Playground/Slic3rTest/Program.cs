using System;
using System.IO;
using GCodeViewer.Library;

namespace Slic3rTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var slicer = new Slic3r(@"C:\Users\florager\Desktop\slic3r\Slic3r-console");

            string stlFilePath = "calibCube.stl";

            Directory.CreateDirectory("GCODE");
            slicer.Slice(stlFilePath, @"calibrationCube.gcode");
        }
    }
}
