using System.Drawing;
using System.Windows;
using OpenGL;

namespace OpenGLTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GlControl_ContextCreated(object sender, OpenGL.GlControlEventArgs e)
        {
            Gl.MatrixMode(MatrixMode.Projection);
            //Gl.LoadIdentity();
            Gl.Ortho(0.0, 1.0f, 0.0, 1.0, 0.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            //Gl.LoadIdentity();
        }

        private void GlControl_Render(object sender, OpenGL.GlControlEventArgs e)
        {
            var senderControl = sender as GlControl;

            int vpx = 0;
            int vpy = 0;
            int vpw = senderControl.ClientSize.Width;
            int vph = senderControl.ClientSize.Height;

            Gl.Viewport(vpx, vpy, vpw, vph);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            // uses NDC (normalized device coordinates)
            float[] triangleVertices =
            {
                -0.5f, -0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                 0.0f,  0.5f, 0.0f
            };

            uint VBO = Gl.GenBuffer();

            Gl.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            Gl.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 9, VBO, BufferUsage.StaticDraw);

            Gl.ClearColor(240, 14, 15, 1);
            senderControl.Invalidate();
        }

        private static void DrawTriangleSimple()
        {
            Gl.Begin(PrimitiveType.Triangles);

            Gl.Color3(1.0f, 0.0f, 0.0f);
            Gl.Vertex2(0.0f, 0.0f);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            Gl.Vertex2(0.5f, 0.8f);
            Gl.Color3(0.0f, 0.0f, 1.0f);
            Gl.Vertex2(1.0f, 0.0f);

            Gl.End();
        }
    }
}
