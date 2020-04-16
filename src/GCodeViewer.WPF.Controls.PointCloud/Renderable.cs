using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls.PointCloud
{
    public class Renderable
    {
        public readonly Color Color;
        public readonly float[] Vertices;
        public readonly PrimitiveType Type;

        public Renderable(Color color, float[] vertices, RenderableType type)
        {
            if (vertices.Length % 3 != 0)
                throw new Exception("The vertices contain at least one incomplete Vector!");

            this.Color = color;
            Vertices = vertices;
            Type = type switch
            {
                RenderableType.Lines => PrimitiveType.Lines,
                RenderableType.Points => PrimitiveType.Points,
                RenderableType.Triangles => PrimitiveType.Triangles,
                _ => PrimitiveType.Points
            };
        }
    }
}
