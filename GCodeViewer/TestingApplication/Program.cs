using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Media3D;
using GCodeViewer.RenderWindow.Utils;

namespace TestingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "WitcherWolf.gcode";
            //string fileName = "SinkingBenchy.gcode";
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var watch = new Stopwatch();
            watch.Start();

            PointExtractor extractor = new PointExtractor(path);
            Point3DCollection points = extractor.ExtractPoints();

            watch.Stop();

            Console.WriteLine($"The gcode file contains {points.Count} points");
            Console.WriteLine($"And retrieving them took {watch.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }
    }
}
