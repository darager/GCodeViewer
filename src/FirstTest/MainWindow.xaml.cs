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

        private void WindowsFormsHost_Initialized(object sender, EventArgs e)
        {
            var flags = GraphicsContextFlags.Default;
            var control = new GLControl(new GraphicsMode(32, 24), 2, 0, flags);

            control.MakeCurrent(); // makes control current (GL.something now uses this control)
            control.Dock = DockStyle.Fill;
            (sender as WindowsFormsHost).Child = control;

            control.Paint += (x, y) =>
            {
                GL.ClearColor(0, 0, 255, 1);

                GL.Clear(ClearBufferMask.ColorBufferBit |
                         ClearBufferMask.DepthBufferBit |
                         ClearBufferMask.StencilBufferBit);

                control.SwapBuffers(); // swaps front and back buffers (impo when scene changes?)
            };

            control.Invalidate(); // makes control invalid and causes it to be redrawn
        }
    }
}
