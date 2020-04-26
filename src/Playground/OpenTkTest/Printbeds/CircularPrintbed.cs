using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Printbeds
{
    public class CircularPrintbed : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CircularPrintbed(float radius, Color color, Color lineColo, int triangleCount = 32)
        {
            List<float> printbedVerts = GetPrintbedVerts(radius, triangleCount);
            var printbed = new Renderable(color, printbedVerts.ToArray(), RenderableType.Triangles);

            _parts.Add(printbed);
        }

        private static List<float> GetPrintbedVerts(float radius, int triangleCount)
        {
            var verts = new List<float>();
            float dAngle = (float)(2 * Math.PI / triangleCount);
            for (int i = 0; i < triangleCount; i++)
            {
                float angle = i * dAngle;

                verts.AddRange(new List<float> { 0, 0, 0 });

                verts.Add(0);
                verts.Add((float)Math.Cos(angle) * radius);
                verts.Add((float)Math.Sin(angle) * radius);

                verts.Add(0);
                verts.Add((float)Math.Cos(angle + dAngle) * radius);
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
