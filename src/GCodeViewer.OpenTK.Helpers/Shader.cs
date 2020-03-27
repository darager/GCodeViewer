using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK.Graphics.ES20;
using OpenTK;

namespace GCodeViewer.OpenTK.Helpers
{
    public class Shader : IDisposable
    {
        private readonly int _handle;

        private readonly int _vertexShader;
        private readonly int _fragmentShader;

        private Dictionary<string, int> _uniformLocations;

        public Shader(string vertexShaderSource, string fragmentShaderSource)
        {
            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader, vertexShaderSource);
            GL.CompileShader(_vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(_vertexShader);
            if (infoLogVert != String.Empty)
                Debug.WriteLine(infoLogVert);

            _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShader, fragmentShaderSource);
            GL.CompileShader(_fragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(_fragmentShader);
            if (infoLogFrag != String.Empty)
                Debug.WriteLine(infoLogFrag);

            // link shaders together into a program for later usage
            _handle = GL.CreateProgram();
            GL.AttachShader(_handle, _vertexShader);
            GL.AttachShader(_handle, _fragmentShader);
            GL.LinkProgram(_handle);

            // cleanup
            GL.DetachShader(_handle, _vertexShader);
            GL.DetachShader(_handle, _fragmentShader);
            GL.DeleteShader(_vertexShader);
            GL.DeleteShader(_fragmentShader);

            GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(_handle, i, out _, out _);
                var location = GL.GetUniformLocation(_handle, key);
                _uniformLocations.Add(key, location);
            }
        }
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(_handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void Use()
        {
            GL.UseProgram(_handle);
        }

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                GL.DeleteProgram(_handle);

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
