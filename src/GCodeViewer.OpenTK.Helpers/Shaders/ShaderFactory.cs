﻿using System.Collections.Generic;
using System.Drawing;
using OpenTK;

namespace GCodeViewer.OpenTK.Helpers.Shaders
{
    public static class ShaderFactory
    {
        private static string _uniformName = "view";
        private static string _originalVertexShaderSource =
            "#version 330 core\n" +
            "layout (location = 0) in vec3 aPosition;\n" +
            "uniform mat4 %UNIFORMNAME%;\n" +
            "void main()\n" +
            "{\n" +
                "gl_Position = %UNIFORMNAME% * vec4(aPosition, 1.0);\n" +
                "gl_PointSize = 5.0;\n" +
            "}";
        private static string _originalFragmentShaderSource =
            "#version 330 core\n" +
            "out vec4 FragColor;\n" +
            "void main()\n" +
            "{\n" +
                "FragColor = vec4(%RED%, %GREEN%, %BLUE%, %ALPHA%);\n" +
            "}";

        private static List<Shader> _shaders = new List<Shader>();

        public static Shader FromColor(Color color)
        {
            string red = ScaleFromRGBValueToFloat(color.R).ToString();
            string green = ScaleFromRGBValueToFloat(color.G).ToString();
            string blue = ScaleFromRGBValueToFloat(color.B).ToString();
            string alpha = ScaleFromRGBValueToFloat(color.A).ToString();

            string fragmentShaderSource = _originalFragmentShaderSource
                                            .Replace("%RED%", red)
                                            .Replace("%GREEN%", green)
                                            .Replace("%BLUE%", blue)
                                            .Replace("%ALPHA%", alpha);

            string vertexShaderSource = _originalVertexShaderSource
                                            .Replace("%UNIFORMNAME%", _uniformName);

            var shader = new Shader(vertexShaderSource, fragmentShaderSource);

            _shaders.Add(shader);

            return shader;
        }
        public static void SetRotationMatrix(Matrix4 matrix)
        {
            foreach (Shader shader in _shaders)
                shader.SetMatrix4(_uniformName, matrix);
        }
        public static void DisposeAll()
        {
            foreach (Shader shader in _shaders)
                shader.Dispose();
        }

        private static float ScaleFromRGBValueToFloat(int value)
        {
            return Scale(value, 0, 255, 0, 1);

            static float Scale(int value, int min, int max, int minScale, int maxScale)
            {
                return minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
            }
        }
    }
}
