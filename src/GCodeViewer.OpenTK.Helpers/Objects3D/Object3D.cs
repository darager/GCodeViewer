using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.OpenTK.Helpers.Objects3D
{
    public class Object3D
    {
        private readonly Color _color;
        private readonly float[] _vertices;
        private readonly PrimitiveType _type;

        public Object3D(Color color, float[] vertices, ObjectType type)
        {
            if (vertices.Length % 3 != 0)
                throw new Exception("The vertices contain at least one incomplete Vector!");

            _color = color;
            _vertices = vertices;
            _type = type switch
            {
                ObjectType.Lines => PrimitiveType.Lines,
                ObjectType.Points => PrimitiveType.Points,
                ObjectType.Triangles => PrimitiveType.Triangles,
                _ => PrimitiveType.Points
            };
        }
    }
}
