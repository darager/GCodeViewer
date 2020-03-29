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

        public Dictionary<Object3D, VertexBufferObject> _vbos = new Dictionary<Object3D, VertexBufferObject>();

        public PointCloudViewer()
        {
            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default);
            _control.Dock = DockStyle.Fill;
            _control.MakeCurrent(); // makes GL.something refer to this control

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

            _control.Invalidate(); // causes control to be redrawn
        }

        public void Add3DObject(Object3D object3D)
        {
            var shader = _shaderFactory.FromColor(object3D.Color);
            var vbo = new VertexBufferObject(object3D.Vertices, object3D.Type, shader);

            _vbos.Add(object3D, vbo);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.ProgramPointSize);

            _camera.ApplyTransformation();

            foreach (var vbo in _vbos.Values)
                vbo.Draw();

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

            foreach (var vbo in _vbos.Values)
                vbo.Dispose();

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
