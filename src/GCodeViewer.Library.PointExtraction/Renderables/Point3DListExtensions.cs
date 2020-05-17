using System.Collections.Generic;

namespace GCodeViewer.Library.Renderables
{
    public static class Point3DListExtensions
    {
        // If the existing lists are changed instead of new ones being created weird bugs start to appear!
        public static List<Point3D> RotateXYX(this List<Point3D> @this, float rotXdeg, float rotYdeg, float rotX2deg)
        {
            var result = new List<Point3D>();

            foreach (var p in @this)
            {
                var point = new Point3D(p.X, p.Y, p.Z);

                point = point.RotateX(rotXdeg);
                point = point.RotateY(rotYdeg);
                point = point.RotateX(rotX2deg);

                result.Add(point);
            }

            return result;
        }
        public static List<Point3D> Translate(this List<Point3D> @this, Point3D offset)
        {
            var result = new List<Point3D>();

            foreach (var p in @this)
            {
                var point = new Point3D(p.X, p.Y, p.Z);
                point.X += offset.X;
                point.Y += offset.Y;
                point.Z += offset.Z;

                result.Add(point);
            }

            return result;
        }
    }
}
