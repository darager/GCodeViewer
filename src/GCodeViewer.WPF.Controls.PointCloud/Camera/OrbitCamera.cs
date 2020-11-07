﻿using GCodeViewer.WPF.Controls.PointCloud.Shaders;
using OpenTK;

namespace GCodeViewer.WPF.Controls.PointCloud.Camera
{
    internal class OrbitCamera
    {
        public float Scale = 0.5f;
        public float RotationX = 0;
        public float RotationY = 0;

        private ShaderBuilder _shaderFactory;

        public OrbitCamera(ShaderBuilder shaderFactory)
        {
            this._shaderFactory = shaderFactory;
        }

        public void ShowHomePosition()
        {
            RotationX = -45;
            RotationY = 25;
            Scale = 0.9f;
            ApplyTransformation();
        }

        public void ApplyTransformation()
        {
            var transform =
                Matrix4.Identity
              * Matrix4.LookAt(new Vector3(0, 0, -10), new Vector3(0, 0, 0), Vector3.UnitY)
              * Matrix4.CreatePerspectiveFieldOfView(0.5f, 1, 1.0f, 100.0f)
              * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-RotationX))
              * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(RotationY + 180)) // +180 to make sure the starting view is from the front
              * Matrix4.CreateScale(Scale);

            _shaderFactory.SetProjectionMatrix(transform);
        }
    }
}
