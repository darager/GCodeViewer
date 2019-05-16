using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;

namespace GCodeViewer.RenderWindow.Utils
{
    public class PointExtractor
    {
        private FileStream stream;
        private Regex xRegex;
        private Regex yRegex;
        private Regex zRegex;

        public PointExtractor(string gcodeFilePath)
        {
            if (!File.Exists(gcodeFilePath))
                throw new FileNotFoundException();

            stream = new FileStream(gcodeFilePath, FileMode.Open);

            xRegex = GetRegex('X');
            yRegex = GetRegex('Y');
            zRegex = GetRegex('Z');
        }

        public Point3DCollection ExtractPoints()
        {
            var points = new Point3DCollection();

            using (var reader = new StreamReader(stream))
            {
                var point = new Point3D();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    point = ExtractPoint(line, point);
                    points.Add(point);
                }
            }

            return points;
        }

        private Point3D ExtractPoint(string line, Point3D lastPoint)
        {
            Point3D point = lastPoint;

            if (line.Length == 0)
                return point;
            if (line[0] == ';')
                return point;

            if (xRegex.Match(line).Success)
                point.X = GetValue(xRegex, line);
            if (yRegex.Match(line).Success)
                point.Y = GetValue(yRegex, line);
            if (zRegex.Match(line).Success)
                point.Z = GetValue(zRegex, line);

            return point;
        }
        private Regex GetRegex(char character)
        {
            var regex = new Regex(character + "(\\d+\\.\\d+|\\d+)");
            return regex;
        }
        private double GetValue(Regex regex, string line)
        {
            Match match = regex.Match(line);
            string valueString = match.ToString().Substring(1);
            double value = Convert.ToDouble(valueString);

            return value;
        }
    }
}
