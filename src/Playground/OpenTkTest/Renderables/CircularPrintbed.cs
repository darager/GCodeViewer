using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Renderables
{
    public class CircularPrintbed : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CircularPrintbed(float radius, Color color, Color lineColo, int triangleCount = 100)
        {
            List<float> printbedVerts = GetPrintbedVerts(radius, triangleCount);
            var printbed = new Renderable(color, printbedVerts.ToArray(), RenderableType.Triangles);

            List<float> lineVerts = GetLineVertices(radius);
            var lines = new Renderable(lineColo, lineVerts.ToArray(), RenderableType.Lines);

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
            vertices.Add(0);
            vertices.Add(y);
        }

        private static List<float> GetPrintbedVerts(float radius, int triangleCount)
        {
            var verts = new List<float>();
            float dAngle = (float)(2 * Math.PI / triangleCount);
            for (int i = 0; i < triangleCount; i++)
            {
                float angle = i * dAngle;

                verts.AddRange(new List<float> { 0, 0, 0 });

                verts.Add((float)Math.Cos(angle) * radius);
                verts.Add(0);
                verts.Add((float)Math.Sin(angle) * radius);

                verts.Add((float)Math.Cos(angle + dAngle) * radius);
                verts.Add(0);
                verts.Add((float)Math.Sin(angle + dAngle) * radius);
            }

            return verts;
        }

        public void AddTo(ICollection<Renderable> collection)
        {
            foreach (var part in _parts)
                collection.Add(part);
        }
    }
}
