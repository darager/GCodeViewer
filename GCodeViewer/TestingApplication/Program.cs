using System;
using System.Windows.Media.Media3D;
using GCodeViewer.RenderWindow.Utils;

namespace TestingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\flori\source\repos\GCodeViewer\GCodeViewer\GCodeExamples\SinkingBenchy.gcode";

            PointExtractor extractor = new PointExtractor(path);
            Point3DCollection points = extractor.ExtractPoints();

            //foreach (Point3D point in points)
            //    DisplayPoint(point);

            Console.WriteLine($"The gcode file contains {points.Count} points");

            Console.ReadKey();
        }

        private static void DisplayPoint(Point3D point)
        {
            Console.WriteLine($"{point.X}, {point.Y}, {point.Z}");
        }
    }
}
