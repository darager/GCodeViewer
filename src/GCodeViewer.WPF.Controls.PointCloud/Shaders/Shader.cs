using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace GCodeViewer.WPF.Controls.Pointcloud.Shaders
{
    internal class Shader
    {
        private readonly int _handle;

        private readonly int _vertexShader;
        private readonly int _fragmentShader;

        private Dictionary<string, int> _uniformLocations;

        public Shader(string vertexShaderSource, string fragmentShaderSource)
        {
            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(_vertexShader, vertexShaderSource);
            GL.ShaderSource(_fragmentShader, fragmentShaderSource);

            GL.CompileShader(_vertexShader);
            GL.CompileShader(_fragmentShader);

            // link shaders together into a program for later usage
            _handle = GL.CreateProgram();
            GL.AttachShader(_handle, _vertexShader);
            GL.AttachShader(_handle, _fragmentShader);
            GL.LinkProgram(_handle);

            CleanUpShaders();

            AddUniforms();
        }
        private void CleanUpShaders()
        {
            GL.DetachShader(_handle, _vertexShader);
            GL.DetachShader(_handle, _fragmentShader);
            GL.DeleteShader(_vertexShader);
            GL.DeleteShader(_fragmentShader);
        }
        private void AddUniforms()
        {
            GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(_handle, i, out _, out _);
                var location = GL.GetUniformLocation(_handle, key);
                _uniformLocations.Add(key, location);
            }
        }

        public void Use()
        {
            GL.UseProgram(_handle);
        }
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(_handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        public void Dispose()
        {
            GL.DeleteProgram(_handle);
            GC.SuppressFinalize(this);
        }
    }
}
