using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class Zylinder
    {
        private float _height;
        private float _radius;

        private Point3D _position;
        private float _rotationX;
        private float _rotationY;
        private int _triangleCount = 300;

        private Color _color;

        private Zylinder() { }

        public static Zylinder With()
        {
            var circle = new Zylinder()
            {
                _radius = 1,
                _position = new Point3D(0, 0, 0),
                _height = 2,
                _rotationX = 0,
                _rotationY = 0,
                _color = System.Drawing.Color.LightGray
            };

            return circle;
        }

        public Zylinder Height(float height)
        {
            _height = height;
            return this;
        }
        public Zylinder Radius(float radius)
        {
            _radius = radius;
            return this;
        }
        public Zylinder Position(Point3D position)
        {
            _position = position;
            return this;
        }
        public Zylinder RotationX(float rotationX)
        {
            _rotationX = rotationX;
            return this;
        }
        public Zylinder RotationY(float rotationY)
        {
            _rotationY = rotationY;
            return this;
        }
        public Zylinder Color(Color color)
        {
            _color = color;
            return this;
        }

        public Renderable Build()
        {
            var points = GetZylinderPoints(_radius, _triangleCount)
                        .RotateXYX(_rotationX, _rotationY, 0)
                        .Translate(_position);

            return new Renderable(_color, points, RenderableType.Triangles);
        }

        private List<Point3D> GetZylinderPoints(float radius, int triangleCount)
        {
            var result = new List<Point3D>();

            var c1middle = _position;
            var c2middle = new Point3D(0, 0, _height);

            float dAngle = (float)(2 * Math.PI / triangleCount);
            for (int i = 0; i < triangleCount; i++)
            {
                float angle = i * dAngle;

                // circle 1
                result.Add(c1middle);
                result.Add(GetPointOnZylinder(angle, 0));
                result.Add(GetPointOnZylinder(angle + dAngle, 0));

                result.Add(GetPointOnZylinder(angle, 0));
                result.Add(GetPointOnZylinder(angle + dAngle, 0));
                result.Add(GetPointOnZylinder(angle, _height));

                result.Add(GetPointOnZylinder(angle, _height));
                result.Add(GetPointOnZylinder(angle + dAngle, _height));
                result.Add(GetPointOnZylinder(angle + dAngle, 0));

                // circle 2
                result.Add(c2middle);
                result.Add(GetPointOnZylinder(angle, _height));
                result.Add(GetPointOnZylinder(angle + dAngle, _height));

                Point3D GetPointOnZylinder(float ang, float z)
                {
                    return new Point3D(
                        (float)Math.Cos(ang) * radius,
                        (float)Math.Sin(ang) * radius,
                        z
                    );
                }
            }

            return result;
        }
    }
}
