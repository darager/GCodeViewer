using System;
using OpenTK;

namespace OpenTkTest
{
    public class Camera
    {
        public Vector3 Translation;
        public float RotationX = 0.0f;
        public float RotationY = 0.0f;
        public float Zoom;

        private Shader _shader;

        public Camera(Shader shader, Vector3 translation, float zoom)
        {
            this._shader = shader;
            this.Translation = translation;
            this.Zoom = zoom;
        }

        public void ApplyTransformation()
        {
            var transform = Matrix4.Identity;

            transform *= Matrix4.CreateTranslation(Translation);
            transform *= Matrix4.CreateRotationX(DegToRad(RotationX));
            transform *= Matrix4.CreateRotationY(DegToRad(RotationY));

            _shader.SetMatrix4("view", transform);
        }

        private float DegToRad(float angleInDeg)
        {
            return (float)(Math.PI / 180) * angleInDeg;
        }
    }
}
