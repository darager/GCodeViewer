using g3;

namespace GCodeViewer.Library
{
    public struct Point3D
    {
        public float X;
        public float Y;
        public float Z;

        public Point3D(float x, float y, float z)
        {
            (X, Y, Z) = (x, y, z);
        }

        public static Point3D NaN => new Point3D(float.NaN, float.NaN, float.NaN);

        public override bool Equals(object obj)
        {
            return (obj is Point3D other) && this == other;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return this.X.GetHashCode() * 17
                     + this.Y.GetHashCode() * 17
                     + this.Z.GetHashCode() * 17;
            }
        }
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
