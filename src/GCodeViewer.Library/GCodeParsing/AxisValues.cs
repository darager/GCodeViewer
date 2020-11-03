using System;
using GCodeViewer.Library.Renderables;

namespace GCodeViewer.Library.GCodeParsing
{
    public struct AxisValues
    {
        public float X;
        public float Y;
        public float Z;
        public float A;
        public float C;
        public float E;

        public AxisValues(float x, float y, float z, float e, float a = 0, float c = 0)
        {
            (X, Y, Z, E, A, C) = (x, y, z, e, a, c);
        }

        public static AxisValues Zero => new AxisValues(0, 0, 0, 0, 0, 0);
        public static AxisValues NaN => new AxisValues(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);

        public override bool Equals(object obj)
        {
            return (obj is AxisValues other) && this == other;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, E);

        public static bool operator ==(AxisValues a, AxisValues b)
        {
            return a.X == b.X
                && a.Y == b.Y
                && a.Z == b.Z
                && a.A == b.A
                && a.C == b.C
                && a.E == b.E;
        }

        public static bool operator !=(AxisValues a, AxisValues b)
        {
            return !(a == b);
        }

        public Point3D GetEquivalentPoint(float AAxisOffset = 0)
        {
            // HACK: make this work properly
            return new Point3D(X, Y, Z);
        }
    };
}
