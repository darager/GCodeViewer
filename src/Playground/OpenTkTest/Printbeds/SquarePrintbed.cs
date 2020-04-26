using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Printbeds
{
    public class SquarePrintbed : ICompositeRenderable
    {
        private readonly List<Renderable> _parts = new List<Renderable>();

        public SquarePrintbed(float x, float y, Color color, Color lineColor)
        {
            var lineVerts = GetLineVertices(x, y).ToArray();
            var lines = new Renderable(lineColor, lineVerts, RenderableType.Lines);
            _parts.Add(lines);

            var bedVerts = GetPrintbedVertices(x, y).ToArray();
            var bed = new Renderable(color, bedVerts, RenderableType.Triangles);
            _parts.Add(bed);
        }

        private List<float> GetLineVertices(float x, float y, int lineCount = 10)
        {
            var gridverts = new List<float>();

            float xSpacing = x / lineCount;
            for (int i = 0; i < lineCount + 1; i++)
            {
                float dx = xSpacing * i;
                gridverts.Add(dx);
                gridverts.Add(0);
                gridverts.Add(y);

                gridverts.Add(dx);
                gridverts.Add(0);
                gridverts.Add(0);
            }

            float ySpacing = y / lineCount;
            for (int i = 0; i < lineCount + 1; i++)
            {
                float dy = ySpacing * i;
                gridverts.Add(x);
                gridverts.Add(0);
                gridverts.Add(dy);

                gridverts.Add(0);
                gridverts.Add(0);
                gridverts.Add(dy);
            }

            return gridverts;
        }
        private List<float> GetPrintbedVertices(float x, float y)
        {
            var verts = new List<float>();

            verts.AddRange(new List<float> { 0, 0, 0 });
            verts.AddRange(new List<float> { x, 0, 0 });
            verts.AddRange(new List<float> { x, 0, y });

            verts.AddRange(new List<float> { 0, 0, 0 });
            verts.AddRange(new List<float> { 0, 0, y });
            verts.AddRange(new List<float> { x, 0, y });

            return verts;
        }

        public void AddTo(ICollection<Renderable> collection)
        {
            foreach (Renderable part in _parts)
                collection.Add(part);
        }
    }
}
