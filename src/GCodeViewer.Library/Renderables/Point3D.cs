using System;
using g3;
using GCodeViewer.WPF.Controls.PointCloud;

namespace GCodeViewer.Library.Renderables
{
    public class Point3D : IPoint3F
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3D(float x, float y, float z)
        {
            (X, Y, Z) = (x, y, z);
        }

        public static Point3D NaN => new Point3D(float.NaN, float.NaN, float.NaN);

        public bool Equals(Point3D other) => Equals((object)other);

        public override bool Equals(object obj)
        {
            return (obj is Point3D other) && this == other;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public static bool operator ==(Point3D a, Point3D b)
        {
            return a.X == b.X
                && a.Y == b.Y
                && a.Z == b.Z;
        }

        public static bool operator !=(Point3D a, Point3D b)
        {
            return !(a == b);
        }

        public Point3D RotateX(float deg)
        {
            Rotate(new Vector3f(1, 0, 0), deg);
            return this;
        }

        public Point3D RotateY(float deg)
        {
            Rotate(new Vector3f(0, 1, 0), deg);
            return this;
        }

        public Point3D RotateZ(float deg)
        {
            Rotate(new Vector3f(0, 0, 1), deg);
            return this;
        }

        private void Rotate(Vector3f axis, float deg)
        {
            var vec = new Vector3f(this.X, this.Y, this.Z);
            var mat = Matrix3f.AxisAngleD(axis, deg);

            vec = mat.Multiply(ref vec);

            this.X = vec.x;
            this.Y = vec.y;
            this.Z = vec.z;
        }
    };
}
