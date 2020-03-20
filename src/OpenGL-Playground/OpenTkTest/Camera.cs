using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTkTest
{
    public class Camera
    {
        Shader _shader;

        Matrix4 _originalMatrix;

        Matrix4 _currentMatrix;

        public Camera(Shader shader, Vector3 originalPosition, Vector3 focusPoint)
        {
            _shader = shader;

            _originalMatrix = Matrix4.LookAt(originalPosition, focusPoint, Vector3.UnitY);
            _currentMatrix = _originalMatrix;
            this.Update();
        }

        public void Translate(Vector3 vector)
        {
            _currentMatrix *= Matrix4.CreateTranslation(vector);
            this.Update();
        }
        public void RotateX(float angle)
        {
            _currentMatrix *= Matrix4.CreateRotationX(angle);
            this.Update();
        }
        public void RotateY(float angle)
        {
            _currentMatrix *= Matrix4.CreateRotationY(angle);
            this.Update();
        }
        public void RotateZ(float angle)
        {
            _currentMatrix *= Matrix4.CreateRotationZ(angle);
            this.Update();
        }

        public void Reset()
        {
            _currentMatrix = _originalMatrix;
            this.Update();
        }

        public void Update()
        {
            _shader.SetMatrix4("view", _currentMatrix);
        }
    }
}
