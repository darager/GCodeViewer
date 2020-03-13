using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        GLControl _control;
        int _vertexBuffer;

        Shader _shader = new Shader("shader.vert", "shader.frag");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WinFormsHost_Loaded(object sender, RoutedEventArgs args)
        {
            var flags = GraphicsContextFlags.Default;

            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, flags);

            _control.MakeCurrent(); // makes control current (GL.something now uses this control)
            _control.Dock = DockStyle.Fill;

            _control.Paint += Paint;

            _control.Invalidate(); // makes control invalid and causes it to be redrawn
            WinFormsHost.Child = _control;
        }

        private void Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.CornflowerBlue);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            float[] vertices = {
                -0.5f, -0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                 0.0f,  0.5f, 0.0f
            };

            _vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);



            _control.SwapBuffers(); // swaps front and back buffers (impo when scene changes?)
        }

        private void WinFormsHost_Unloaded(object sender, RoutedEventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vertexBuffer);
        }
    }
}
