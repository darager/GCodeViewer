using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Navigation;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.PointCloud;
using OpenTK.Graphics.OpenGL;
using OpenToolkit.Graphics.GL;

namespace GCodeViewer.Library.Renderables
{
    public class BasicScene : IViewerScene
    {
        public IRenderService RenderService { get; private set; }

        #region Printbed Parts

        private CircularPrintbed _printbed = new CircularPrintbed(radius: 1.0f, Color.DarkGray, Color.White);
        private CoordinateSystem _coordinateSystem = new CoordinateSystem(new Point3D(0, 0, 0), 0.2f);

        #endregion

        private Dictionary<ICompositeRenderable, OffsetScalingRenderable> _renderables;

        private float _scalingFactor;

        public BasicScene(IRenderService renderService, Settings settings)
        {
            RenderService = renderService;
            _renderables = new Dictionary<ICompositeRenderable, OffsetScalingRenderable>();

            RenderService.Add(_printbed);
            RenderService.Add(_coordinateSystem);

            SetPrintBedDiameter(settings.PrinterDimensions.PrintBedDiameter);
        }

        public void Add(ICompositeRenderable renderable)
        {
            var offsetRenderable = new OffsetScalingRenderable(renderable);

            _renderables.Add(renderable, offsetRenderable);

            RenderService.Add(offsetRenderable.GetScaledAndOffsetRenderable());
        }

        public void Remove(ICompositeRenderable renderable)
        {
        }

        public void SetOffset(ICompositeRenderable renderable, Point3D offset)
        {
        }

        public void SetPrintBedDiameter(float printBedDiameter)
        {
            float scalingFactor = 2 / printBedDiameter;

            if (_scalingFactor == scalingFactor)
                return;

            _scalingFactor = scalingFactor;
        }
    }

    public class OffsetScalingRenderable
    {
        private ICompositeRenderable _renderable;

        private float _scalingFactor;
        private Point3D _offset;

        public OffsetScalingRenderable(ICompositeRenderable renderable)
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

        public ICompositeRenderable GetScaledAndOffsetRenderable()
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
