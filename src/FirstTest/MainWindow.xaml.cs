using System;
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
        public MainWindow() => InitializeComponent();

        private void WindowsFormsHost_Initialized(object sender, EventArgs args)
        {
            var flags = GraphicsContextFlags.Default;
            var control = new GLControl(new GraphicsMode(32, 24), 2, 0, flags);

            control.MakeCurrent(); // makes control current (GL.something now uses this control)
            control.Dock = DockStyle.Fill;
            (sender as WindowsFormsHost).Child = control;

            control.Paint += (s, e) =>
            {
                GL.ClearColor(0, 0, 255, 1);

                GL.Clear(ClearBufferMask.ColorBufferBit |
                         ClearBufferMask.DepthBufferBit |
                         ClearBufferMask.StencilBufferBit);

                DrawTriangle();


                control.SwapBuffers(); // swaps front and back buffers (impo when scene changes?)
            };

            control.Invalidate(); // makes control invalid and causes it to be redrawn
        }
        int vertexBufferObject;
        private void DrawTriangle()
        {
            float[] vertices = {
                    -0.5f, -0.5f, 0.0f, //Bottom-left vertex
                     0.5f, -0.5f, 0.0f, //Bottom-right vertex
                     0.0f,  0.5f, 0.0f  //Top vertex
                };

            vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            // the usage changes according to how the data changes over time (StaticDraw, DynamicDraw, StreamDraw (changes every frame))
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }

        private void WindowsFormsHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GL.Viewport(0, 0, 100, 100);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // the buffers have to be cleaned up manually
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // binding a buffer to 0 sets it to null
            GL.DeleteBuffer(vertexBufferObject);
        }
    }
}
