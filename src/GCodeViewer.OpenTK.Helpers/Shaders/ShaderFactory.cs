using System.Collections.Generic;
using System.Drawing;
using OpenTK;

namespace GCodeViewer.OpenTK.Helpers.Shaders
{
    public class ShaderFactory
    {
        private readonly static string _uniformName = "transform";
        private readonly static string _originalVertexShaderSource =
            "#version 330 core\n" +
            "layout (location = 0) in vec3 aPosition;\n" +
            "uniform mat4 %UNIFORMNAME%;\n" +
            "void main()\n" +
            "{\n" +
                "gl_Position = %UNIFORMNAME% * vec4(aPosition, 1.0);\n" +
                "gl_PointSize = 3.0;\n" +
            "}";
        private readonly static string _originalFragmentShaderSource =
            "#version 330 core\n" +
            "out vec4 FragColor;\n" +
            "void main()\n" +
            "{\n" +
                "FragColor = vec4(%RED%, %GREEN%, %BLUE%, %ALPHA%);\n" +
            "}";

        private Dictionary<Color, Shader> _shaders = new Dictionary<Color, Shader>();

        public Shader FromColor(Color color)
        {
            if (_shaders.ContainsKey(color))
                return _shaders[color];

            string red = ToFloat(color.R).ToString();
            string green = ToFloat(color.G).ToString();
            string blue = ToFloat(color.B).ToString();
            string alpha = ToFloat(color.A).ToString();

            string fragmentShaderSource = _originalFragmentShaderSource
                                            .Replace("%RED%", red)
                                            .Replace("%GREEN%", green)
                                            .Replace("%BLUE%", blue)
                                            .Replace("%ALPHA%", alpha);

            string vertexShaderSource = _originalVertexShaderSource
                                            .Replace("%UNIFORMNAME%", _uniformName);

            var shader = new Shader(vertexShaderSource, fragmentShaderSource);

            _shaders.Add(color, shader);

            return shader;
        }
        public void SetTransformationMatrix(Matrix4 matrix)
        {
            foreach (Shader shader in _shaders.Values)
                shader.SetMatrix4(_uniformName, matrix);
        }
        public void DisposeAll()
        {
            foreach (Shader shader in _shaders.Values)
                shader.Dispose();

            _shaders.Clear();
        }

        private float ToFloat(int value)
        {
            return Scale(value, 0, 255, 0, 1);

            static float Scale(int value, int min, int max, int minScale, int maxScale)
            {
                return minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
            }
        }
    }
}
