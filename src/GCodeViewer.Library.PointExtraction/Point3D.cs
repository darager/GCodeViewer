namespace GCodeViewer.Library
{
    public struct Point3D
    {
        public float X;
        public float Y;
        public float Z;

        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
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
    };
}
