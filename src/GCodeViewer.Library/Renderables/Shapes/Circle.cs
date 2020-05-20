using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables.Shapes
{
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
        public Circle TriangleCount(int triangleCount)
        {
            _triangleCount = triangleCount;
            return this;
        }
        public Circle Color(Color color)
        {
            _color = color;
            return this;
        }

        public Renderable Build()
        {
            var points = GetCirclePoints(_radius, _triangleCount)
                        .RotateXYX(_rotationX, _rotationY, 0)
                        .Translate(_position);

            return new Renderable(_color, points, RenderableType.Triangles);
        }

        private List<Point3D> GetCirclePoints(float radius, int triangleCount)
        {
            var result = new List<Point3D>();
            var zero = new Point3D(0, 0, 0);

            float dAngle = (float)(2 * Math.PI / triangleCount);
            for (int i = 0; i < triangleCount; i++)
            {
                float angle = i * dAngle;

                result.Add(zero);
                result.Add(GetPointOnCircle(angle));
                result.Add(GetPointOnCircle(angle + dAngle));

                Point3D GetPointOnCircle(float ang)
                {
                    return new Point3D(
                        (float)Math.Cos(ang) * radius,
                        (float)Math.Sin(ang) * radius,
                        0
                    );
                }
            }

            return result;
        }
    }
}
