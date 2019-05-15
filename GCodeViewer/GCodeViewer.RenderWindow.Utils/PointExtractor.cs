using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GCodeViewer.RenderWindow.Utils
{
    public class PointExtractor
    {
        private FileStream stream;
        private StreamReader reader;

        public PointExtractor(string gcodeFilePath)
        {
            if (File.Exists(gcodeFilePath))
                throw new FileNotFoundException();

            stream = new FileStream(gcodeFilePath, FileMode.Open);
        }

        public Point3DCollection ExtractPoints()
        {
            using (var reader = new StreamReader(stream))
            {
                Point3D point = new Point3D();

                string line;
                while((line = reader.ReadLine()) != null)
                {
                }
            }
        }

        private Point3D ExtractPoint(string line, Point3D lastPoint)
        {
            Point3D point = lastPoint;

            if (line[0] == ';')
                return point;

            var xRegex = new Regex("X(/d+/.*/d*)");

            return point;
        }

        private double GetValue(char character)
        {
            var regex = new Regex(character + "((/d+/.+/d+)|(/d+)/s)");


        }
    }
}
