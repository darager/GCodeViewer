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
        private char X = 'X';
        private char Y = 'Y';
        private char Z = 'Z';
        private char CommentChar = ';';

        public PointExtractor(string gcodeFilePath)
        {
            if (!File.Exists(gcodeFilePath))
                throw new FileNotFoundException();

            stream = new FileStream(gcodeFilePath, FileMode.Open);

            xRegex = GetRegex(X);
            yRegex = GetRegex(Y);
            zRegex = GetRegex(Z);
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

            if (string.IsNullOrEmpty(line))
                return point;
            if (line[0] == CommentChar)
                return point;

            Match xMatch = xRegex.Match(line);
            Match yMatch = yRegex.Match(line);
            Match zMatch = zRegex.Match(line);

            if (xMatch.Success)
                point.X = GetValue(xMatch);
            if (yMatch.Success)
                point.Y = GetValue(yMatch);
            if (zMatch.Success)
                point.Z = GetValue(zMatch);

            return point;
        }
        private Regex GetRegex(char character)
        {
            var regex = new Regex(character + "(\\d+\\.\\d+|\\d+)");
            return regex;
        }
        private double GetValue(Match match)
        {
            string valueString = match.ToString().Substring(1);
            double value = Convert.ToDouble(valueString);

            return value;
        }
    }
}
