using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls.PointCloud
{
    public class Renderable
    {
        public readonly Color Color;
        public readonly float[] Vertices;
        public readonly PrimitiveType Type;

        public Renderable(Color color, IEnumerable<IPoint3F> points, RenderableType type)
        {
            float[] verts = ToVertices(points);

            if (verts.Length % 3 != 0)
                throw new Exception("The vertices contain at least one incomplete Vector!");

            this.Color = color;
            Vertices = verts;
            Type = type switch
            {
                RenderableType.Lines => PrimitiveType.Lines,
                RenderableType.Points => PrimitiveType.Points,
                RenderableType.Triangles => PrimitiveType.Triangles,
                _ => PrimitiveType.Points
            };
        }

        private float[] ToVertices(IEnumerable<IPoint3F> points)
        {
            var verts = new List<float>();

            foreach (var point in points)
            {
                verts.Add(point.X);
                verts.Add(point.Y);
                verts.Add(point.Z);
            }

            return verts.ToArray();
        }
    }
}
