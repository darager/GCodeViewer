using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GCodeViewer.OpenTK.Helpers;
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
        private ShaderFactory _shaderFactory;

        private List<VertexBufferObject> _vbos = new List<VertexBufferObject>();

        #region Vertices
        float[] _coordinateSytemVertices =
        {
            0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
            0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
            0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f  // Z
        };
        private readonly float[] _smallCubeVertices =
        {
            0.25f, 0.25f, 0.25f,   0.7f,  0.25f, 0.25f,
            0.25f, 0.25f, 0.25f,   0.25f, 0.25f, 0.7f,
            0.7f,  0.25f, 0.7f,    0.7f,  0.25f, 0.25f,
            0.7f,  0.25f, 0.7f,    0.25f, 0.25f, 0.7f,
            0.25f, 0.7f,  0.25f,   0.7f,  0.7f,  0.25f,
            0.25f, 0.7f,  0.25f,   0.25f, 0.7f,  0.7f,
            0.7f,  0.7f,  0.7f,    0.7f,  0.7f,  0.25f,
            0.7f,  0.7f,  0.7f,    0.25f, 0.7f,  0.7f,
            0.25f, 0.25f, 0.25f,   0.25f, 0.7f,  0.25f,
            0.25f, 0.25f, 0.7f,    0.25f, 0.7f,  0.7f,
            0.7f,  0.25f, 0.25f,   0.7f,  0.7f,  0.25f,
            0.7f,  0.25f, 0.7f,    0.7f,  0.7f,  0.7f
        };
        private readonly float[] _bigCubeVertices =
        {
           -1.0f, -1.0f, -1.0f,    1.0f, -1.0f, -1.0f,
           -1.0f, -1.0f, -1.0f,   -1.0f, -1.0f,  1.0f,
            1.0f, -1.0f,  1.0f,    1.0f, -1.0f, -1.0f,
            1.0f, -1.0f,  1.0f,   -1.0f, -1.0f,  1.0f,
           -1.0f,  1.0f, -1.0f,    1.0f,  1.0f, -1.0f,
           -1.0f,  1.0f, -1.0f,   -1.0f,  1.0f,  1.0f,
            1.0f,  1.0f,  1.0f,    1.0f,  1.0f, -1.0f,
            1.0f,  1.0f,  1.0f,   -1.0f,  1.0f,  1.0f,
           -1.0f, -1.0f, -1.0f,   -1.0f,  1.0f, -1.0f,
           -1.0f, -1.0f,  1.0f,   -1.0f,  1.0f,  1.0f,
            1.0f, -1.0f, -1.0f,    1.0f,  1.0f, -1.0f,
            1.0f, -1.0f,  1.0f,    1.0f,  1.0f,  1.0f
        };
        #endregion

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

            AddVBOs();
        }

        private void AddVBOs()
        {
            _vbos.Add(new VertexBufferObject(
                            _coordinateSytemVertices,
                            PrimitiveType.Lines,
                            _shaderFactory.FromColor(Color.Red)));

            _vbos.Add(new VertexBufferObject(
                            _smallCubeVertices,
                            PrimitiveType.Lines,
                            _shaderFactory.FromColor(Color.GreenYellow)));

            _vbos.Add(new VertexBufferObject(
                            _bigCubeVertices,
                            PrimitiveType.Lines,
                            _shaderFactory.FromColor(Color.GreenYellow)));

            var rnd = new Random();
            int count = 1000;
            var pointVertices = Enumerable.Range(0, count * 3)
                .Select(_ => rnd.NextDouble())
                .Select(r => (float)r * 2 - 1)
                .ToArray();
            _vbos.Add(new VertexBufferObject(
                            pointVertices,
                            PrimitiveType.Points,
                            _shaderFactory.FromColor(Color.CornflowerBlue)));
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
