using GCodeViewer.WPF.Controls.Pointcloud.Shaders;
using OpenTK;

namespace GCodeViewer.WPF.Controls.Pointcloud.Camera
{
    internal class OrbitCamera
    {
        public float RotationX = 0.0f;
        public float RotationY = 0.0f;
        public float Scale;

        private ShaderBuilder _shaderFactory;

        public OrbitCamera(float startScale, ShaderBuilder shaderFactory)
        {
            this._shaderFactory = shaderFactory;
            this.Scale = startScale;
        }

        public void ApplyTransformation()
        {
            var transform = Matrix4.Identity;

            transform *= Matrix4.LookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
            transform *= Matrix4.CreatePerspectiveFieldOfView(0.5f, 1, 1.0f, 100.0f);
            transform *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-RotationX));
            transform *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-RotationY));
            transform *= Matrix4.CreateScale(Scale);

            _shaderFactory.SetProjectionMatrix(transform);
        }
    }
}
