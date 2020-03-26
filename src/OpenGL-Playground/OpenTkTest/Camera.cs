using System;
using OpenTK;

namespace OpenTkTest
{
    public class Camera
    {
        public float RotationX = 0.0f;
        public float RotationY = 0.0f;
        public float Scale;

        private Shader _shader;

        public Camera(Shader shader, float startScale)
        {
            this._shader = shader;
            this.Scale = startScale;
        }

        public void ApplyTransformation()
        {
            var transform = Matrix4.Identity;

            transform *= Matrix4.CreateRotationX(DegToRad(RotationX));
            transform *= Matrix4.CreateRotationY(DegToRad(RotationY));
            transform *= Matrix4.CreateScale(Scale);

            _shader.SetMatrix4("view", transform);
        }

        private float DegToRad(float angleInDeg)
        {
            return (float)(Math.PI / 180) * angleInDeg;
        }
    }
}
