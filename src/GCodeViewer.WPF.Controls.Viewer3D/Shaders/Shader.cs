using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls.Viewer3D.Shaders
{
    internal class Shader
    {
        private readonly int _handle;

        private readonly int _vertexShader;
        private readonly int _fragmentShader;

        private Dictionary<string, int> _uniformLocations;

        public Shader(string vertexShaderSource, string fragmentShaderSource)
        {
            _vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
            _fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

            _handle = LinkShaders(_vertexShader, _fragmentShader);
            CleanUpShaders();

            AddUniforms();
        }

        private int CompileShader(ShaderType type, string shaderSource)
        {
            int shaderHandle = GL.CreateShader(type);
            GL.ShaderSource(shaderHandle, shaderSource);
            GL.CompileShader(shaderHandle);

            return shaderHandle;
        }

        private int LinkShaders(int vertexShader, int fragmentShader)
        {
            int handle = GL.CreateProgram();
            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);
            GL.LinkProgram(handle);

            return handle;
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
