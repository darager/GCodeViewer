using System;
using OpenTK;

namespace OpenTkTest
{
    public class Camera
    {
        private Shader _shader;
        private Matrix4 _originalPosition;

        public Matrix4 CurrentPosition; // should be private

        public Camera(Shader shader, Vector3 originalPosition, Vector3 focusPoint)
        {
            _shader = shader;

            _originalPosition = Matrix4.LookAt(originalPosition, focusPoint, Vector3.UnitY);
            CurrentPosition = _originalPosition;
            this.Update();
        }

        public void Translate(Vector3 vector)
        {
            CurrentPosition *= Matrix4.CreateTranslation(vector);
            this.Update();
        }

        public void RotateX(float angleInDeg)
        {
            var rotation = Matrix4.CreateRotationX(DegToRad(angleInDeg));
            CurrentPosition *= rotation;
            this.Update();
        }
        public void RotateY(float angleInDeg)
        {
            var rotation = Matrix4.CreateRotationY(DegToRad(angleInDeg));
            CurrentPosition *= rotation;
            this.Update();
        }
        public void RotateZ(float angleInDeg)
        {
            var rotation = Matrix4.CreateRotationZ(DegToRad(angleInDeg));
            CurrentPosition *= rotation;
            this.Update();
        }

        public void Reset()
        {
            CurrentPosition = _originalPosition;
            this.Update();
        }

        private float DegToRad(float angleInDeg)
        {
            return (float)(Math.PI / 180) * angleInDeg;
        }

        // TODO: should be private (relative rotation should be internal)
        public void Update()
        {
            _shader.SetMatrix4("view", CurrentPosition);
        }
    }
}
