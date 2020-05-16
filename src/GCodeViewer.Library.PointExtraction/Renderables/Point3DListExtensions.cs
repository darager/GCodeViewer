using System.Collections.Generic;

namespace GCodeViewer.Library.Renderables
{
    public static class Point3DListExtensions
    {
        public static List<Point3D> RotateXYX(this List<Point3D> @this, float rotXdeg, float rotYdeg, float rotX2deg)
        {
            for (int i = 0; i < @this.Count; i++)
            {
                var point = @this[i];

                point = point.RotateX(rotXdeg);
                point = point.RotateY(rotYdeg);
                point = point.RotateX(rotX2deg);

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
    }
}
