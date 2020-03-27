using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GCodeViewer.OpenTK.Helpers;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GCodeViewer.WPF.Controls
{
    public class PointCloudViewer : WindowsFormsHost
    {
        private GLControl _control;

        private Shader _shader;
        private Camera _camera;

        private List<VertexBufferObject> _vbos = new List<VertexBufferObject>();

        private readonly float[] _cubeVertices =
        {
            // bottom
            0.25f, 0.25f, 0.25f,  0.7f, 0.25f, 0.25f,
            0.25f, 0.25f, 0.25f,  0.25f, 0.25f, 0.7f,
            0.7f, 0.25f, 0.7f,  0.7f, 0.25f, 0.25f,
            0.7f, 0.25f, 0.7f,  0.25f, 0.25f, 0.7f,

            // top
            0.25f, 0.7f, 0.25f,  0.7f, 0.7f, 0.25f,
            0.25f, 0.7f, 0.25f,  0.25f, 0.7f, 0.7f,
            0.7f, 0.7f, 0.7f,  0.7f, 0.7f, 0.25f,
            0.7f, 0.7f, 0.7f,  0.25f, 0.7f, 0.7f,

            // middle
            0.25f, 0.25f, 0.25f,  0.25f, 0.7f, 0.25f,
            0.25f, 0.25f, 0.7f,  0.25f, 0.7f, 0.7f,
            0.7f, 0.25f, 0.25f,  0.7f, 0.7f, 0.25f,
            0.7f, 0.25f, 0.7f,  0.7f, 0.7f, 0.7f,
        };
        float[] _coordinateSytemVertices =
        {
            0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
            0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
            0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f  // Z
        };

        public PointCloudViewer()
        {
            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default);
            _control.Dock = DockStyle.Fill;
            _control.MakeCurrent(); // makes control current (GL.something now uses this control)

            this.Child = _control;

            string vertShaderSource = File.ReadAllText("Shaders/shader.vert");
            string fragmentShaderSource = File.ReadAllText("Shaders/shader.frag");
            _shader = new Shader(vertShaderSource, fragmentShaderSource);

            _camera = new Camera(_shader, startScale: 0.5f);

            _control.Paint += OnPaint;
            _control.MouseMove += OnMouseMove;
            _control.MouseWheel += OnMouseWheel;

            this.Unloaded += OnUnloaded;
            this.SizeChanged += OnSizeChanged;

            _control.Invalidate(); // makes control invalid and causes it to be redrawn

            _vbos.Add(new VertexBufferObject(_coordinateSytemVertices, PrimitiveType.Lines, _shader));
            _vbos.Add(new VertexBufferObject(_cubeVertices, PrimitiveType.Lines, _shader));

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, normalized: false, 3 * sizeof(float), offset: 0);
            GL.EnableVertexAttribArray(0);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
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
            int height = (int)this.ActualHeight;
            int width = (int)this.ActualWidth;
            GL.Viewport(0, 0, width, height);
        }

        #region Rotation and Zooming
        private Point previousPosition = new Point(0, 0);
        private float mouseSensitivity = 0.25f;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            float dx = (float)(e.X - previousPosition.X);
            float dy = (float)(e.Y - previousPosition.Y);

            if ((Control.MouseButtons & MouseButtons.Left) != 0)
            {
                _camera.RotationX += (-dy * mouseSensitivity);
                _camera.RotationY += (-dx * mouseSensitivity);
            }

            previousPosition = new Point(e.X, e.Y);
        }

        private float wheelSensitivity = 0.1f;
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int direction = e.Delta / 120;

            _camera.Scale += direction * wheelSensitivity;

            if (_camera.Scale >= 1.0f) _camera.Scale = 1.0f;
            if (_camera.Scale <= 0.01f) _camera.Scale = 0.01f;
        }
        #endregion

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            _vbos.ForEach(v => v.Dispose());

            _shader.Dispose();
        }

    }
}
