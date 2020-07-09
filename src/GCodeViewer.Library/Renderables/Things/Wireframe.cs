using System;
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
            var linePoints = GetLinePoints(mesh);
            _parts.Add(new Renderable(lineColor, linePoints, RenderableType.Lines));

            if (fillColor.HasValue)
            {
                var color = fillColor.Value;
                var trianglePoints = GetTrianglePoints(mesh);

                var renderable = new Renderable(color, trianglePoints, RenderableType.Triangles);

                _parts.Add(renderable);
            }
        }

        private List<Point3D> GetLinePoints(Mesh mesh)
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

                    float x = (float)vert.x;
                    float y = (float)vert.y;
                    float z = (float)vert.z;

                    points.Add(new Point3D(x, y, z));
                }
            }

            return points;
        }

        private List<Point3D> GetTrianglePoints(Mesh mesh)
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

                    float x = (float)vert.x;
                    float y = (float)vert.y;
                    float z = (float)vert.z;

                    points.Add(new Point3D(x, y, z));
                }
            }

            return points;
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
