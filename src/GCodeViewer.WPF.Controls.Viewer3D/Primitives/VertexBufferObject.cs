﻿using System;
using GCodeViewer.WPF.Controls.Viewer3D.Shaders;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls.Viewer3D.Primitives
{
    internal class VertexBufferObject : IDisposable
    {
        private int _handle;

        private Shader _shader;
        private float[] _vertices;
        private PrimitiveType _type;
        private BufferUsageHint _usageHint;

        public VertexBufferObject(Renderable renderable,
                                  Shader shader,
                                  BufferUsageHint usageHint = BufferUsageHint.StaticDraw)
        {
            _vertices = renderable.Vertices;
            _type = renderable.Type;
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

            int pointCount = _vertices.Length / 3;
            GL.DrawArrays(_type, first: 0, pointCount);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}
