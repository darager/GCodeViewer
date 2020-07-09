using System.Collections.Generic;
using System.Drawing;
using g3;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables.Things
{
    public class Wireframe : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public Wireframe(Mesh mesh, Color lineColor, Color? fillColor = null)
        {
            var triangleIndices = mesh.TriangleIndices();

            var points = new List<Point3D>();
            foreach (int idx in triangleIndices)
            {
                var triangle = mesh.GetTriangle(idx);

                // TODO: some lines are drawn twice
                AddPoint(triangle.c);
                AddPoint(triangle.a);
                AddPoint(triangle.a);
                AddPoint(triangle.b);
                AddPoint(triangle.b);
                AddPoint(triangle.c);

                void AddPoint(int vertexIndex)
                {
                    Vector3d vert = mesh.GetVertex(vertexIndex);

                    float x = (float)vert.x / 10;
                    float y = (float)vert.y / 10;
                    float z = (float)vert.z / 10;

                    points.Add(new Point3D(x, y, z));
                }
            }

            _parts.Add(new Renderable(lineColor, points, RenderableType.Lines));
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
