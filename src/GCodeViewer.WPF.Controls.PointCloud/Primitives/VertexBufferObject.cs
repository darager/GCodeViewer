using System;
using GCodeViewer.WPF.Controls.Pointcloud.Shaders;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls.PointCloud.Primitives
{
    internal class VertexBufferObject : IDisposable
    {
        int _handle;

        Shader _shader;
        float[] _vertices;
        PrimitiveType _type;
        BufferUsageHint _usageHint;

        public VertexBufferObject(float[] vertices,
                                  PrimitiveType type,
                                  Shader shader,
                                  BufferUsageHint usageHint = BufferUsageHint.StaticDraw)
        {
            _vertices = vertices;
            _type = type;
            _shader = shader;
            _usageHint = usageHint;

            _handle = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);

            GL.VertexAttribPointer(0,
                                   size: 3,
                                   VertexAttribPointerType.Float,
                                   normalized: false,
                                   3 * sizeof(float),
                                   offset: 0);
        }

        public void Draw()
        {
            int count = _vertices.Length / 3;

            GL.BufferData(BufferTarget.ArrayBuffer,
                          _vertices.Length * sizeof(float),
                          _vertices,
                          _usageHint);

            _shader.Use();
            GL.DrawArrays(_type, first: 0, count);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}
