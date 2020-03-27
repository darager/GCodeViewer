﻿using System;
using GCodeViewer.OpenTK.Helpers.Shaders;
using OpenTK;

namespace GCodeViewer.OpenTK.Helpers
{
    public class OrbitCamera
    {
        public float RotationX = 0.0f;
        public float RotationY = 0.0f;
        public float RotationZ = 0.0f;
        public float Scale;

        private Shader _shader;

        public OrbitCamera(Shader shader, float startScale)
        {
            this._shader = shader;
            this.Scale = startScale;
        }

        public void ApplyTransformation()
        {
            var transform = Matrix4.Identity;

            transform *= Matrix4.CreateRotationX(DegToRad(RotationX));
            transform *= Matrix4.CreateRotationY(DegToRad(RotationY));
            transform *= Matrix4.CreateRotationZ(DegToRad(RotationZ));
            transform *= Matrix4.CreateScale(Scale);

            ShaderFactory.SetRotationMatrix(transform);
        }

        private float DegToRad(float angleInDeg)
        {
            return (float)(Math.PI / 180) * angleInDeg;
        }
    }
}
