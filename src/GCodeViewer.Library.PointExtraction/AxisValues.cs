﻿namespace GCodeViewer.Library
{
    public struct AxisValues
    {
        public float X;
        public float Y;
        public float Z;
        public float E;

        public AxisValues(float x, float y, float z, float e)
        {
            (X, Y, Z, E) = (x, y, z, e);
        }

        public static AxisValues NaN => new AxisValues(float.NaN, float.NaN, float.NaN, float.NaN);

        public override bool Equals(object obj)
        {
            return (obj is AxisValues other) && this == other;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return this.X.GetHashCode() * 17
                     + this.Y.GetHashCode() * 17
                     + this.Z.GetHashCode() * 17
                     + this.E.GetHashCode() * 17;
            }
        }
        public static bool operator ==(AxisValues a, AxisValues b)
        {
            return a.X == b.X
                && a.Y == b.Y
                && a.Z == b.Z
                && a.E == b.E;
        }
        public static bool operator !=(AxisValues a, AxisValues b)
        {
            return !(a == b);
        }
    };
}
