using System.Collections.Generic;

namespace GCodeViewer.Library
{
    public static class Point3DListExtensions
    {
        public static List<Point3D> RotateXYZ(this List<Point3D> @this, float rotX, float rotY, float rotZ)
        {
            for (int i = 0; i < @this.Count; i++)
            {
                var point = @this[i];

                point = point.RotateX(rotX);
                point = point.RotateY(rotY);
                point = point.RotateZ(rotZ);

                @this[i] = point;
            }

            return @this;
        }
        public static List<Point3D> Translate(this List<Point3D> @this, Point3D _position)
        {
            for (int i = 0; i < @this.Count; i++)
            {
                var point = @this[i];

                point.X += _position.X;
                point.Y += _position.Y;
                point.Z += _position.Z;

                @this[i] = point;
            }

            return @this;
        }
        public static float[] ToVertices(this List<Point3D> @this)
        {
            var verts = new List<float>();

            foreach (var point in @this)
            {
                verts.Add(point.X);
                verts.Add(point.Y);
                verts.Add(point.Z);
            }

            return verts.ToArray();
        }
    }
}
