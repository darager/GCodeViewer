using System;
using GCodeViewer.Library;

namespace Slic3rTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var slicer = new Slic3r(@"C:\Users\florager\Desktop\slic3r\Slic3r-console");

            Console.WriteLine(slicer.Help());
        }
    }
}
