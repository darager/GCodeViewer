using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using GCodeViewer.WPF.Controls.PointCloud.Helpers;

namespace GCodeViewer.WPF.Controls.PointCloud.Shaders
{
    internal class ShaderBuilder
    {
        private const string _projectionMatrixName = "transform";
        private const string _originalVertexShaderSource =
            "#version 330 core\n" +
            "layout (location = 0) in vec3 aPosition;\n" +
            "uniform mat4 %UNIFORMNAME%;\n" +
            "void main()\n" +
            "{\n" +
            "   gl_PointSize = 1.0;\n" +
            "   gl_Position = %UNIFORMNAME% * vec4(aPosition, 1.0);\n" +
            "}";
        private const string _originalFragmentShaderSource =
            "#version 330 core\n" +
            "out vec4 FragColor;\n" +
            "void main()\n" +
            "{\n" +
            "   FragColor = vec4(%RED%, %GREEN%, %BLUE%, %ALPHA%);\n" +
            "}";

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
            string red = ScaleToFloat(color.R).ToString();
            string green = ScaleToFloat(color.G).ToString();
            string blue = ScaleToFloat(color.B).ToString();
            string alpha = ScaleToFloat(color.A).ToString();

            return _originalFragmentShaderSource
                       .Replace("%RED%", red)
                       .Replace("%GREEN%", green)
                       .Replace("%BLUE%", blue)
                       .Replace("%ALPHA%", alpha);
        }
        private string GetVertexShaderSource()
        {
            return _originalVertexShaderSource
                       .Replace("%UNIFORMNAME%", _projectionMatrixName);
        }
        private float ScaleToFloat(int value) => ((float)value).Scale(0, 255, 0, 1);
    }
}
