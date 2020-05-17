using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class CircularPrintbed : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CircularPrintbed(float radius, Color color, Color lineColor, float rotationX = 0, float rotationY = 0)
        {
            var printbed = Circle.With()
                                 .Position(new Point3D(0, 0, 0))
                                 .Radius(radius)
                                 .RotationX(rotationX)
                                 .RotationY(rotationY)
                                 .Color(color)
                                 .Build();

            var linePoints = GetLinePoints(radius)
                            .RotateXYX(rotationX, rotationY, 0);
            var lines = new Renderable(lineColor, linePoints, RenderableType.Lines);

            _parts.Add(printbed);
            _parts.Add(lines);
        }

        private List<Point3D> GetLinePoints(float radius, int lineCount = 11)
        {
            var verts = new List<Point3D>();

            float lineSpacing = (2 * radius) / lineCount;

            for (int i = 0; i < lineCount - 1; i++)
            {
                float x = i * lineSpacing;
                float y = (float)Math.Sqrt((radius * radius) - (x * x));
                float z = 0.001f; // lines should be slightly above circle of printbed

                // vertical Lines
                verts.Add(new Point3D(x, y, z));
                verts.Add(new Point3D(x, -y, z));
                verts.Add(new Point3D(-x, y, z));
                verts.Add(new Point3D(-x, -y, z));

                // horizontal Lines
                verts.Add(new Point3D(y, x, z));
                verts.Add(new Point3D(-y, x, z));
                verts.Add(new Point3D(y, -x, z));
                verts.Add(new Point3D(-y, -x, z));
            }

            return verts;
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
