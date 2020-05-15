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
                                 .Radius(radius)
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
            vertices.Add(0.001f);
            vertices.Add(y);
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

    public class Circle
    {
        private float _radius;

        public Point3D _position;
        public float _rotationX;
        public float _rotationY;

        public int _triangleCount;
        public Color _color;

        private Circle() { }

        public static Circle With()
        {
            var circle = new Circle()
            {
                _radius = 1,
                _position = new Point3D(0, 0, 0),
                _rotationX = 0,
                _rotationY = 0,
                _triangleCount = 100,
                _color = System.Drawing.Color.LightGray
            };

            return circle;
        }

        public Circle Radius(float radius)
        {
            _radius = radius;
            return this;
        }
        public Circle Position(Point3D position)
        {
            _position = position;
            return this;
        }
        public Circle RotationX(float rotationX)
        {
            _rotationX = rotationX;
            return this;
        }
        public Circle RotationY(float rotationY)
        {
            _rotationY = rotationY;
            return this;
        }
        public Circle Color(Color color)
        {
            _color = color;
            return this;
        }
        public Circle TriangleCount(int triangleCount)
        {
            _triangleCount = triangleCount;
            return this;
        }

        private float[] _vertices;
        public Renderable Build()
        {
            var points = GetCirclePoints(_radius, _position, _triangleCount);

            return new Renderable(_color, _vertices, RenderableType.Triangles);
        }

        private List<Point3D> GetCirclePoints(float radius, Point3D position, int triangleCount)
        {
            var result = new List<Point3D>();

            float dAngle = (float)(2 * Math.PI / triangleCount);
            for (int i = 0; i < triangleCount; i++)
            {
                float angle = i * dAngle;

                result.Add(position);
                result.Add(GetPointOnCircle(angle));
                result.Add(GetPointOnCircle(angle + dAngle));

                Point3D GetPointOnCircle(float ang)
                {
                    return new Point3D(
                        position.X + (float)Math.Cos(ang) * radius,
                        position.Y + 0.0f,
                        position.Z + (float)Math.Sin(ang) * radius
                    );
                }
            }

            return result;
        }
    }
}

