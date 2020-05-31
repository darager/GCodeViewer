using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.PrinterSettings;
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

        public ICompositeRenderable Build()
        {
            var result = new ScaledAndOffsetRenderables();

            foreach (Renderable renderable in _renderable.GetParts())
            {
                var verts = GetOffsetAndScaledVertices(renderable);
                var type = GetType(renderable.Type);

                result.Add(new Renderable(renderable.Color, verts, type));
            }

            return result;
        }

        private RenderableType GetType(PrimitiveType type) => type switch
        {
            PrimitiveType.Points => RenderableType.Points,
            PrimitiveType.Lines => RenderableType.Lines,
            PrimitiveType.Triangles => RenderableType.Triangles,
            _ => throw new Exception("This primitive Type is not supported!")
        };

        private List<Point3D> GetOffsetAndScaledVertices(Renderable renderable)
        {
            var points = new List<Point3D>();

            for (int i = 0; i < renderable.Vertices.Length; i += 3)
            {
                float x = OffsetAndScale(i, _offset.X);     // X
                float y = OffsetAndScale(i + 1, _offset.Y); // Y
                float z = OffsetAndScale(i + 2, _offset.Z); // Z

                points.Add(new Point3D(x, y, z));

                float OffsetAndScale(int index, float offset)
                {
                    float value = renderable.Vertices[index];

                    value += offset;
                    value *= _scalingFactor;

                    return value;
                }
            }

            return points;
        }
    }
}
