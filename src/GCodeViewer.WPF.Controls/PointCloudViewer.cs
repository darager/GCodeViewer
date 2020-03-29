using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GCodeViewer.OpenTK.Helpers;
using GCodeViewer.OpenTK.Helpers.Objects3D;
using GCodeViewer.OpenTK.Helpers.Shaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Point = System.Windows.Point;

namespace GCodeViewer.WPF.Controls
{
    public class PointCloudViewer : WindowsFormsHost
    {
        private GLControl _control;
        private OrbitCamera _camera;
        internal ShaderFactory _shaderFactory;

        public List<VertexBufferObject> _vbos = new List<VertexBufferObject>();

        public ObservableCollection<Object3D> ObjectsSource
        {
            get => (ObservableCollection<Object3D>)GetValue(Objects3DProperty);
            set => SetValue(Objects3DProperty, value);
        }
        public readonly DependencyProperty Objects3DProperty =
            DependencyProperty.Register(
                nameof(ObjectsSource),
                typeof(ObservableCollection<Object3D>), typeof(PointCloudViewer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(Objects3D_CollectionChanged)));
        private static void Objects3D_CollectionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = (PointCloudViewer)source;
            var items = e.NewValue;

            var item = (Object3D)e.NewValue;

            Shader shader = control._shaderFactory.FromColor(item.Color);
            var vbo = new VertexBufferObject(item.Vertices, item.Type, shader);

            control._vbos.Add(vbo);
        }

        public PointCloudViewer()
        {
            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default);
            _control.Dock = DockStyle.Fill;
            _control.MakeCurrent(); // makes control current (GL.something now uses this control)

            this.Child = _control;

            _shaderFactory = new ShaderFactory();
            _camera = new OrbitCamera(startScale: 0.5f, _shaderFactory);

            _control.Paint += OnPaint;
            _control.MouseMove += OnMouseMove;
            _control.MouseWheel += OnMouseWheel;

            this.Unloaded += OnUnloaded;
            this.SizeChanged += OnSizeChanged;

            GL.Enable(EnableCap.DepthTest);
            GL.EnableVertexAttribArray(0);

            _control.Invalidate(); // makes control invalid and causes it to be redrawn
        }


        private void OnPaint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.ProgramPointSize);

            _camera.ApplyTransformation();

            _vbos.ForEach(v => v.Draw());

            _control.SwapBuffers(); // swaps front and back buffers
            _control.Invalidate();
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            GL.Viewport(0, 0, (int)this.ActualWidth, (int)this.ActualHeight);
        }
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            _vbos.ForEach(v => v.Dispose());

            _shaderFactory.DisposeAll();
        }

        private Point _previousMousePosition = new Point(0, 0);
        private float _mouseSensitivity = 0.25f;
        private float _mouseWheelSensitivity = 0.05f;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            float dx = (float)(e.X - _previousMousePosition.X);
            float dy = (float)(e.Y - _previousMousePosition.Y);

            if ((Control.MouseButtons & MouseButtons.Left) != 0)
            {
                _camera.RotationX += (-dy * _mouseSensitivity);
                _camera.RotationY += (-dx * _mouseSensitivity);
            }

            _previousMousePosition = new Point(e.X, e.Y);
        }
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int direction = e.Delta / 120;
            float newScale = _camera.Scale + (direction * _mouseWheelSensitivity);

            if (newScale >= 3.0f) newScale = 3.0f;
            if (newScale <= 0.05f) newScale = 0.05f;

            _camera.Scale = newScale;
        }
    }
}
