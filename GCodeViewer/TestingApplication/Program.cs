using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\florager\source\repos\GCodeViewer\GCodeViewer\GCodeExamples\SinkingBenchy.gcode";

            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < 100; i++)
                Console.WriteLine(lines[i]);

            Console.ReadKey();
        }
    }
}
