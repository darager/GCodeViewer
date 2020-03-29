using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.OpenTK.Helpers.Objects3D
{
    public class Object3D
    {
        public readonly Color Color;
        public readonly float[] Vertices;
        public readonly PrimitiveType Type;

        public Object3D(Color color, float[] vertices, ObjectType type)
        {
            if (vertices.Length % 3 != 0)
                throw new Exception("The vertices contain at least one incomplete Vector!");

            this.Color = color;
            Vertices = vertices;
            Type = type switch
            {
                ObjectType.Lines => PrimitiveType.Lines,
                ObjectType.Points => PrimitiveType.Points,
                ObjectType.Triangles => PrimitiveType.Triangles,
                _ => PrimitiveType.Points
            };
        }
    }
}
