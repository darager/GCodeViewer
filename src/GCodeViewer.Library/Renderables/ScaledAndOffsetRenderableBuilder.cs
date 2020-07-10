using System;
using System.Collections.Generic;
using System.Linq;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.Library.Renderables
{
    public class ScaledAndOffsetRenderableBuilder
    {
        private ICompositeRenderable _renderable;

        private float _scalingFactor;
        private Point3D _offset;
        private (float X, float Y, float Z) _rotation;

        public ScaledAndOffsetRenderableBuilder(ICompositeRenderable renderable)
        {
            _renderable = renderable;
        }

        public void SetScalingFactor(float scalingFactor)
        {
            _scalingFactor = scalingFactor;
        }

        public void SetOffset(Point3D offset)
        {
            _offset = offset;
        }

        public void SetRotation((float rotX, float rotY, float rotZ) rotation)
        {
            _rotation = rotation;
        }

        public ICompositeRenderable Build()
        {
            var result = new ScaledAndOffsetRenderables();

            foreach (Renderable renderable in _renderable.GetParts())
            {
                List<Point3D> points = GetVertexPoints(renderable)
                                      .RotateXYZ(_rotation.X, _rotation.Y, _rotation.Z);

                points.ForEach(p => OffsetPoint(p));
                points.ForEach(p => ScalePoint(p));

                var type = GetType(renderable.Type);

                result.Add(new Renderable(renderable.Color, points, type));
            }

            return result;
        }

        private void OffsetPoint(Point3D p)
        {
            p.X += _offset.X;
            p.Y += _offset.Y;
            p.Z += _offset.Z;
        }

        private void ScalePoint(Point3D p)
        {
            p.X *= _scalingFactor;
            p.Y *= _scalingFactor;
            p.Z *= _scalingFactor;
        }

        private List<Point3D> GetVertexPoints(Renderable renderable)
        {
            var points = new List<Point3D>();

            for (int i = 0; i < renderable.Vertices.Length; i += 3)
            {
                float x = renderable.Vertices[i];
                float y = renderable.Vertices[i + 1];
                float z = renderable.Vertices[i + 2];

                points.Add(new Point3D(x, y, z));
            }

            return points;
        }

        private RenderableType GetType(PrimitiveType type) => type switch
        {
            PrimitiveType.Points => RenderableType.Points,
            PrimitiveType.Lines => RenderableType.Lines,
            PrimitiveType.Triangles => RenderableType.Triangles,
            _ => throw new Exception("This primitive Type is not supported!")
        };
    }
}
