using System.Collections.Generic;
using System.Drawing;
using g3;
using GCodeViewer.WPF.Controls.PointCloud;

// TODO: some lines can no be seen correctly
namespace GCodeViewer.Library.Renderables.Things
{
    public class Wireframe : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public Wireframe(Mesh mesh, Color lineColor, Color? fillColor = null)
        {
            var linePoints = GetLinePoints(mesh);
            _parts.Add(new Renderable(lineColor, linePoints, RenderableType.Lines));

            if (fillColor.HasValue)
            {
                var color = fillColor.Value;
                var trianglePoints = GetFacePoints(mesh);

                var renderable = new Renderable(color, trianglePoints, RenderableType.Triangles);

                _parts.Add(renderable);
            }
        }

        private List<Point3D> GetLinePoints(Mesh mesh)
        {
            var drawnLines = new HashSet<(int, int)>();
            var triangleIndices = mesh.TriangleIndices();

            var points = new List<Point3D>();
            foreach (int idx in triangleIndices)
            {
                var triangle = mesh.GetTriangle(idx);

                AddLineIfNotYetAdded(triangle.a, triangle.b);
                AddLineIfNotYetAdded(triangle.b, triangle.c);
                AddLineIfNotYetAdded(triangle.c, triangle.a);

                void AddLineIfNotYetAdded(int point1Idx, int point2Idx)
                {
                    if (drawnLines.Contains((point1Idx, point2Idx)))
                        return;

                    AddPoint(point1Idx);
                    AddPoint(point2Idx);

                    drawnLines.Add((point1Idx, point2Idx));
                }
                void AddPoint(int vertexIndex)
                {
                    Vector3d vertex = mesh.GetVertex(vertexIndex);

                    points.Add(new Point3D((float)vertex.x,
                                           (float)vertex.y,
                                           (float)vertex.z));
                }
            }

            return points;
        }

        private List<Point3D> GetFacePoints(Mesh mesh)
        {
            var triangleIndices = mesh.TriangleIndices();

            var points = new List<Point3D>();
            foreach (int idx in triangleIndices)
            {
                var triangle = mesh.GetTriangle(idx);

                AddPoint(triangle.a);
                AddPoint(triangle.b);
                AddPoint(triangle.c);

                void AddPoint(int vertexIndex)
                {
                    Vector3d vert = mesh.GetVertex(vertexIndex);

                    points.Add(new Point3D((float)vert.x,
                                           (float)vert.y,
                                           (float)vert.z));
                }
            }

            return points;
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
