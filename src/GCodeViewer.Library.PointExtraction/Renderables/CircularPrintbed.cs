using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class CircularPrintbed : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CircularPrintbed(float radius, Color color, Color lineColor, int triangleCount = 100)
        {
            var printbed = Circle.With()
                .Position(new Point3D(0, 0, 1))
                .Radius(radius)
                .RotationX(45)
                .Color(color)
                .TriangleCount(triangleCount)
                .Build();

            List<float> lineVerts = GetLineVertices(radius);
            var lines = new Renderable(lineColor, lineVerts.ToArray(), RenderableType.Lines);

            _parts.Add(printbed);
            _parts.Add(lines);
        }

        private List<float> GetLineVertices(float radius, int lineCount = 11)
        {
            var verts = new List<float>();

            float lineSpacing = (2 * radius) / lineCount;

            for (int i = 0; i < lineCount - 1; i++)
            {
                float x = i * lineSpacing;
                float y = (float)Math.Sqrt((radius * radius) - (x * x));

                // vertical Lines
                AddPoint(verts, x, y);
                AddPoint(verts, x, -y);
                AddPoint(verts, -x, y);
                AddPoint(verts, -x, -y);

                // horizontal Lines
                AddPoint(verts, y, x);
                AddPoint(verts, -y, x);
                AddPoint(verts, y, -x);
                AddPoint(verts, -y, -x);
            }

            return verts;
        }
        private void AddPoint(List<float> vertices, float x, float y)
        {
            vertices.Add(x);
            vertices.Add(y);
            vertices.Add(0.001f);
        }

        public void AddTo(ICollection<Renderable> collection)
        {
            foreach (var part in _parts)
                collection.Add(part);
        }

        public void RemoveFrom(ICollection<Renderable> collection)
        {
            foreach (var part in _parts)
                collection.Remove(part);
        }
    }
}
