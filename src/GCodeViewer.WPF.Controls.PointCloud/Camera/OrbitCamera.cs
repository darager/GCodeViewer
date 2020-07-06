using GCodeViewer.WPF.Controls.PointCloud.Shaders;
using OpenTK;

namespace GCodeViewer.WPF.Controls.PointCloud.Camera
{
    internal class OrbitCamera
    {
        public float Scale;
        public float RotationX = 0.0f;
        public float RotationY = 0.0f;

        private ShaderBuilder _shaderFactory;

        public OrbitCamera(ShaderBuilder shaderFactory, float startScale = 0.5f)
        {
            this._shaderFactory = shaderFactory;
            this.Scale = startScale;
        }

        public void ApplyTransformation()
        {
            var transform =
                Matrix4.Identity
              * Matrix4.LookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY)
              * Matrix4.CreatePerspectiveFieldOfView(0.5f, 1, 1.0f, 100.0f)
              * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-RotationX))
              * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-RotationY))
              * Matrix4.CreateScale(Scale);

            _shaderFactory.SetProjectionMatrix(transform);
        }
    }
}
