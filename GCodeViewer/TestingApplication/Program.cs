using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Media3D;

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


            watch.Stop();


            Console.ReadKey();
        }
    }
}
