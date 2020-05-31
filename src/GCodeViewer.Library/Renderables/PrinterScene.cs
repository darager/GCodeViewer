using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.Library.Renderables
{
    public class BasicScene : IViewerScene
    {
        public IRenderService RenderService { get; private set; }

        #region Printbed Parts

        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);

        #endregion

        private Dictionary<ICompositeRenderable, (ICompositeRenderable, Point3D)> _renderables;

        private float _scalingFactor;

        public BasicScene(IRenderService renderService, Settings settings)
        {
            RenderService = renderService;
            _renderables = new Dictionary<ICompositeRenderable, (ICompositeRenderable, Point3D)>();

            RenderService.Add(_printbed);
            RenderService.Add(_coordinateSystem);

            SetPrintBedDiameter(settings.PrinterDimensions.PrintBedDiameter);
        }

        public void Add(ICompositeRenderable renderable, Point3D offset)
        {
            var builder = new OffsetScalingRenderableBuilder(renderable);
            builder.SetScalingFactor(_scalingFactor);
            builder.SetOffset(offset);
            var offsetRenderable = builder.Build();

            _renderables.Add(renderable, (offsetRenderable, offset));
            RenderService.Add(offsetRenderable);
        }

        public void Remove(ICompositeRenderable renderable)
        {
            if (!(_renderables.ContainsKey(renderable))) return;

            var offsetRenderable = _renderables[renderable].Item1;
            _renderables.Remove(renderable);

            RenderService.Remove(offsetRenderable);
        }

        public void UpdateOffset(ICompositeRenderable renderable, Point3D offset)
        {
            Remove(renderable);
            Add(renderable, offset);
        }

        public void SetPrintBedDiameter(float printBedDiameter)
        {
            float scalingFactor = 2 / printBedDiameter;

            if (_scalingFactor == scalingFactor)
                return;

            _scalingFactor = scalingFactor;

            UpdateEveryRenderable();
        }

        private void UpdateEveryRenderable()
        {
            foreach (var key in _renderables.Keys)
            {
                Remove(key);
                Add(key, _renderables[key].Item2);
            }
        }
    }

    public class OffsetScalingRenderableBuilder
    {
        private ICompositeRenderable _renderable;

        private float _scalingFactor;
        private Point3D _offset;

        public OffsetScalingRenderableBuilder(ICompositeRenderable renderable)
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

    public class ScaledAndOffsetRenderables : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public void Add(Renderable renderable)
        {
            _parts.Add(renderable);
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
