using System;
using GCodeViewer.WPF.Controls.PointCloud.Shaders;
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

            // TODO: when this is only done once the stl model is shown but the rest is not
            //       since the data does not have to be loaded every time performance is much better
            GL.BufferData(BufferTarget.ArrayBuffer,
                          _vertices.Length * sizeof(float),
                          _vertices,
                          _usageHint);
        }

        public void Draw()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);

            GL.VertexAttribPointer(0,
                                   size: 3,
                                   VertexAttribPointerType.Float,
                                   normalized: false,
                                   3 * sizeof(float),
                                   offset: 0);

            _shader.Use();

            int count = _vertices.Length / 3;
            GL.DrawArrays(_type, first: 0, count);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}
