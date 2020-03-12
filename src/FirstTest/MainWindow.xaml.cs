using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;

namespace FirstTest
{
    public partial class MainWindow : Window
    {
        private GLControl _control;

        public MainWindow()
        {
            InitializeComponent();

            var flags = GraphicsContextFlags.Default;

            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, flags);
            _control.MakeCurrent(); // makes control current (GL.something now uses this control)
            _control.Dock = DockStyle.Fill;
            _control.Paint += Paint;

            winFormsHost.Child = _control;
        }

        private void Paint(object sender, EventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            SetupViewPort();


            _control.SwapBuffers(); // swaps front and back buffers (impo when scene changes?)
            _control.Invalidate(); // make control invalid and redraw it
        }

        private void SetupViewPort()
        {
            int w = _control.Width;
            int h = _control.Height;
            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area
        }

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText("shader.vert"));
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText("shader.frag"));
            GL.CompileShader(fragmentShader);

            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            return program;
        }
    }
}
