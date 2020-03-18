using System.Windows;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private GLControl _control;
        private int _vertexBufferObject;

        private Shader _shader;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WinFormsHost_Loaded(object sender, RoutedEventArgs args)
        {
            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default);
            _control.MakeCurrent(); // makes control current (GL.something now uses this control)

            _control.Dock = DockStyle.Fill;
            _control.Paint += Paint;

            _shader = new Shader("shader/shader.vert", "shader/shader.frag");

            _control.Invalidate(); // makes control invalid and causes it to be redrawn
            WinFormsHost.Child = _control;
        }

        private void Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            float[] vertices = {
               -0.5f, -0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                0.0f,  0.5f, 0.0f
            };

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // before caling the VertexAttribPointer method the VBO has to be bound
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, normalized: false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader.Use();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            _control.SwapBuffers(); // swaps front and back buffers (impo when scene changes?)
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vertexBufferObject);

            _shader.Dispose();
        }

        private void WinFormsHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int height = (int)WinFormsHost.ActualHeight;
            int width = (int)WinFormsHost.ActualWidth;
            GL.Viewport(0, 0, width, height);
        }
    }
}
