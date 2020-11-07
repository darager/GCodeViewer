using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GCodeViewer.Helpers;
using OpenTK;

namespace GCodeViewer.WPF.Controls.Viewer3D.Shaders
{
    internal class ShaderBuilder
    {
        private const string _projectionMatrixName = "transform";

        private Dictionary<Color, Shader> _shaders = new Dictionary<Color, Shader>();

        public Shader FromColor(Color color)
        {
            if (_shaders.ContainsKey(color))
                return _shaders[color];

            string fragmentShaderSource = GetFragmentShaderSource(color);
            string vertexShaderSource = GetVertexShaderSource();

            var shader = new Shader(vertexShaderSource, fragmentShaderSource);

            _shaders.Add(color, shader);

            return shader;
        }

        public void SetProjectionMatrix(Matrix4 matrix)
        {
            foreach (Shader shader in _shaders.Values)
                shader.SetMatrix4(_projectionMatrixName, matrix);
        }

        public void DisposeAll()
        {
            foreach (Shader shader in _shaders.Values)
                shader.Dispose();

            _shaders.Clear();
        }

        private string GetFragmentShaderSource(Color color)
        {
            return File.ReadAllText(@"Shaders\shader.frag")
                       .Replace("%RED%", ScaleToFloat(color.R))
                       .Replace("%GREEN%", ScaleToFloat(color.G))
                       .Replace("%BLUE%", ScaleToFloat(color.B))
                       .Replace("%ALPHA%", ScaleToFloat(color.A));
        }

        private string GetVertexShaderSource()
        {
            return File.ReadAllText(@"Shaders\shader.vert")
                       .Replace("%UNIFORMNAME%", _projectionMatrixName);
        }

        private string ScaleToFloat(int value) => ((float)value).Scale(0, 255, 0, 1).ToString();
    }
}
