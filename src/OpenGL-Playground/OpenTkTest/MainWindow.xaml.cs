using System.Windows;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private GLControl _control;

        private Shader _shader;
        private Camera _camera;

        private int _vertexBufferObject;
        private readonly float[] vertices =
        {
            0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
            0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
            0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f, // Z

            // bottom
            0.25f, 0.25f, 0.25f,  0.5f, 0.25f, 0.25f,
            0.25f, 0.25f, 0.25f,  0.25f, 0.25f, 0.5f,
            0.5f, 0.25f, 0.5f,  0.5f, 0.25f, 0.25f,
            0.5f, 0.25f, 0.5f,  0.25f, 0.25f, 0.5f,

            // top
            0.25f, 0.5f, 0.25f,  0.5f, 0.5f, 0.25f,
            0.25f, 0.5f, 0.25f,  0.25f, 0.5f, 0.5f,
            0.5f, 0.5f, 0.5f,  0.5f, 0.5f, 0.25f,
            0.5f, 0.5f, 0.5f,  0.25f, 0.5f, 0.5f,

            // connections
            0.25f, 0.25f, 0.25f,  0.25f, 0.5f, 0.25f,
            0.25f, 0.25f, 0.5f,  0.25f, 0.5f, 0.5f,
            0.5f, 0.25f, 0.25f,  0.5f, 0.5f, 0.25f,
            0.5f, 0.25f, 0.5f,  0.5f, 0.5f, 0.5f,
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WinFormsHost_Loaded(object sender, RoutedEventArgs args)
        {
            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default);
            _control.MakeCurrent(); // makes control current (GL.something now uses this control)

            _control.Dock = DockStyle.Fill;
            WinFormsHost.Child = _control;

            _control.Paint += Paint;
            _control.MouseMove += Control_MouseMove;
            _control.MouseWheel += Control_MouseWheel;

            _shader = new Shader("shader/shader.vert", "shader/shader.frag");
            _camera = new Camera(_shader, startScale: 0.5f);

            _control.Invalidate(); // makes control invalid and causes it to be redrawn

            CreateAndBindVBO();
        }

        private void CreateAndBindVBO()
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          vertices.Length * sizeof(float),
                          vertices,
                          BufferUsageHint.StaticDraw);

            // before caling the VertexAttribPointer method the VBO has to be bound
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, normalized: false, 3 * sizeof(float), offset: 0);
            GL.EnableVertexAttribArray(0);
        }

        private void Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.ProgramPointSize);

            _camera.ApplyTransformation();
            _shader.Use();

            int count = vertices.Length / 3;
            GL.DrawArrays(PrimitiveType.Lines, 0, count);

            _control.SwapBuffers(); // swaps front and back buffers
            _control.Invalidate();
        }

        private void WinFormsHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int height = (int)WinFormsHost.ActualHeight;
            int width = (int)WinFormsHost.ActualWidth;
            GL.Viewport(0, 0, width, height);
        }

        #region Rotation and Zooming
        private Point previousPosition = new Point(0, 0);
        private float mouseSensitivity = 0.25f;
        private void Control_MouseMove(object sender, MouseEventArgs e)
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
        private void Control_MouseWheel(object sender, MouseEventArgs e)
        {
            int direction = e.Delta / 120;

            _camera.Scale += direction * wheelSensitivity;

            if (_camera.Scale >= 1.0f) _camera.Scale = 1.0f;
            if (_camera.Scale <= 0.01f) _camera.Scale = 0.01f;
        }
        #endregion

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vertexBufferObject);
            _shader.Dispose();
        }
    }
}
