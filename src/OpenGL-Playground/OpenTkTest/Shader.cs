using System;
using System.Diagnostics;
using System.IO;
using OpenTK.Graphics.ES20;

namespace OpenTkTest
{
    public class Shader : IDisposable
    {
        private int _handle;

        private int _vertexShader;
        private int _fragmentShader;

        public Shader(string vertexShaderPath, string fragShaderPath)
        {
            string vertexShaderSource = File.ReadAllText(vertexShaderPath);
            string fragmentShaderSource = File.ReadAllText(fragShaderPath);


            // create the shaders
            _vertexShader = GL.CreateShader(ShaderType.VertexShader);

            _fragmentShader = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(_vertexShader, vertexShaderSource);
            GL.ShaderSource(_fragmentShader, fragmentShaderSource);

            // Compile the shaders
            GL.CompileShader(_vertexShader);

            string infoLogVert = GL.GetShaderInfoLog(_vertexShader);
            if (infoLogVert != String.Empty)
                Debug.WriteLine(infoLogVert);

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

        ~Shader()
        {
            GL.DeleteProgram(_handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
