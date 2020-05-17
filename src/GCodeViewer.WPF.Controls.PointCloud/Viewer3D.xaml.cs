using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Forms;
using GCodeViewer.Helpers;
using GCodeViewer.WPF.Controls.PointCloud.Camera;
using GCodeViewer.WPF.Controls.PointCloud.Primitives;
using GCodeViewer.WPF.Controls.PointCloud.Shaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls.PointCloud
{
    public partial class Viewer3D : System.Windows.Controls.UserControl
    {
        public ObservableCollection<Renderable> Renderables
        {
            get => (ObservableCollection<Renderable>)this.GetValue(RenderablesProperty);
            set => this.SetValue(RenderablesProperty, value);
        }
        public static readonly DependencyProperty RenderablesProperty =
            DependencyProperty.Register(
                "Renderables",
                typeof(ObservableCollection<Renderable>),
                typeof(Viewer3D),
                new FrameworkPropertyMetadata(UpdateRenderables));
        private static void UpdateRenderables(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var pclViewer = (Viewer3D)source;

            if (args.OldValue != null)
            {
                var oldRenderables = (ObservableCollection<Renderable>)args.OldValue;
                foreach (Renderable renderable in oldRenderables)
                    pclViewer._vbos.Remove(renderable);
            }

            if (args.NewValue != null)
            {
                pclViewer.Renderables = (ObservableCollection<Renderable>)args.NewValue;
                foreach (var renderable in pclViewer.Renderables)
                    AddRenderable(pclViewer, renderable);
                pclViewer.Renderables.CollectionChanged += (_, e) => Renderables_CollectionChanged(e, pclViewer);
            }
        }

        private readonly GLControl _control;
        private readonly OrbitCamera _camera;
        internal readonly ShaderBuilder _shaderBuilder;

        internal Dictionary<Renderable, VertexBufferObject> _vbos = new Dictionary<Renderable, VertexBufferObject>();

        public Viewer3D()
        {
            InitializeComponent();

            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default)
            {
                Dock = DockStyle.Fill
            };
            _control.MakeCurrent(); // makes GL.something refer to this control

            Host.Child = _control;

            _shaderBuilder = new ShaderBuilder();
            _camera = new OrbitCamera(startScale: 0.5f, _shaderBuilder);

            _control.Paint += Draw;
            _control.MouseMove += OnMouseMove;
            _control.MouseWheel += OnMouseWheel;

            this.Unloaded += DisposeEverything;
            this.SizeChanged += ResizeWindow;

            GL.EnableVertexAttribArray(0);

            _control.Invalidate();
        }

        private static void Renderables_CollectionChanged(NotifyCollectionChangedEventArgs args, Viewer3D pclViewer)
        {
            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Renderable renderable in args.OldItems)
                    pclViewer._vbos.Remove(renderable);
            }
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Renderable renderable in args.NewItems)
                    AddRenderable(pclViewer, renderable);
            }
        }
        private static void AddRenderable(Viewer3D pclViewer, Renderable renderable)
        {
            var shader = pclViewer._shaderBuilder.FromColor(renderable.Color);
            var vbo = new VertexBufferObject(renderable.Vertices, renderable.Type, shader);

            pclViewer._vbos.Add(renderable, vbo);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.117f, 0.117f, 0.117f, 1.0f);

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.ProgramPointSize);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthRange(1, 0); // reverse mappings have to be used  ¯\_(ツ)_/¯

            _camera.ApplyTransformation();

            foreach (var vbo in _vbos.Values)
                vbo.Draw();

            _control.SwapBuffers();
            _control.Invalidate();
        }
        private void ResizeWindow(object sender, SizeChangedEventArgs e)
        {
            GL.Viewport(0, 0, (int)this.ActualWidth, (int)this.ActualHeight);
        }
        private void DisposeEverything(object sender, RoutedEventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            foreach (var vbo in _vbos.Values)
                vbo.Dispose();

            _shaderBuilder.DisposeAll();
        }

        private Point _previousMousePosition = new Point(0, 0);
        private readonly float _mouseSensitivity = 0.25f;
        private readonly float _mouseWheelSensitivity = 0.05f;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            float dx = (float)(e.X - _previousMousePosition.X);
            float dy = (float)(e.Y - _previousMousePosition.Y);

            // this fixes the camera jumping after a slider or something has been adjusted
            if (dx > 60 || dy > 60)
            {
                _previousMousePosition = new Point(e.X, e.Y);
                return;
            }

            bool leftMouseButtonPressed = (Control.MouseButtons & MouseButtons.Left) != 0;

            if (leftMouseButtonPressed)
            {
                float newRotationX = (-dy * _mouseSensitivity) + _camera.RotationX;
                _camera.RotationX = newRotationX.Constrain(-90, 90);

                _camera.RotationY += (-dx * _mouseSensitivity);
            }

            _previousMousePosition = new Point(e.X, e.Y);
        }
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int direction = e.Delta / 120;

            float newScale = _camera.Scale + (direction * _mouseWheelSensitivity);

            _camera.Scale = newScale.Constrain(0.05f, 2.0f);
        }
    }
}
