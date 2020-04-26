using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace OpenTkTest.Printbeds
{
    public class SquarePrintbed : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public SquarePrintbed(float x, float y, Color color, Color lineColor)
        {
            var lineVerts = GetLineVertices().ToArray();
            var lines = new Renderable(lineColor, lineVerts, RenderableType.Lines);
            _parts.Add(lines);

            var bedVerts = GetPrintbedVertices(x, y).ToArray();
            var bed = new Renderable(color, bedVerts, RenderableType.Triangles);
            _parts.Add(bed);
        }

        private List<float> GetLineVertices(int lineCount = 10)
        {
            var gridverts = new List<float>();

            float lineSpacing = 2.0f / lineCount;
            for (int i = 0; i < lineCount + 1; i++)
            {
                float spacing = lineSpacing * i;
                gridverts.Add(-1 + spacing);
                gridverts.Add(0);
                gridverts.Add(1);

                gridverts.Add(-1 + spacing);
                gridverts.Add(0);
                gridverts.Add(-1);

                gridverts.Add(-1);
                gridverts.Add(0);
                gridverts.Add(-1 + spacing);

                gridverts.Add(1);
                gridverts.Add(0);
                gridverts.Add(-1 + spacing);
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
