using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GCodeViewer.RenderWindow.Utils
{
    public class PointExtractor
    {
        private FileStream stream;

        public PointExtractor(string gcodeFilePath)
        {
            if (File.Exists(gcodeFilePath))
                throw new FileNotFoundException();

            stream = new FileStream(gcodeFilePath, FileMode.Open);
        }

        public Point3DCollection ExtractPoints()
        {
            Point3DCollection points = new Point3DCollection();



            return points;
        }
    }
}
