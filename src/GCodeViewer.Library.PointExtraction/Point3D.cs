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

        public bool Equals(Point3D other)
        {
            return this.X == other.X
                && this.Y == other.Y
                && this.Z == other.Z;
        }

        public static Point3D NaN => new Point3D(float.NaN, float.NaN, float.NaN);
    };
}
