﻿using System;
using System.Windows;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows.Input;
using System.Timers;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private GLControl _control;
        private int _vertexBufferObject;

        private Shader _shader;
        private Camera _camera;
        private Random _random = new Random();

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
            WinFormsHost.Child = _control;
            _control.MouseMove += Control_MouseMove;

            _shader = new Shader("shader/shader.vert", "shader/shader.frag");
            _camera = new Camera(_shader, new Vector3(0, 0, 6), Vector3.Zero);

            _control.Invalidate(); // makes control invalid and causes it to be redrawn
        }


        private void Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            #region draw points

            float[] vertices =
            {
                0, 0, 0,
                0.1f, 0, 0,

                0, 0, 0,
                0, 0.1f, 0,

                0, 0, 0,
                0, 0, 0.1f,
            };

            int count = vertices.Length / 3;

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          vertices.Length * sizeof(float),
                          vertices,
                          BufferUsageHint.StaticDraw);

            // before caling the VertexAttribPointer method the VBO has to be bound
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, normalized: false, 3 * sizeof(float), offset: 0);
            GL.EnableVertexAttribArray(0);

            _shader.Use();

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.ProgramPointSize);

            GL.DrawArrays(PrimitiveType.Lines, 0, count);
            #endregion


            //_camera.Reset(); // has to be reset since camera keeps state (resizing not done properly?)
            //_camera.RotateZ(45);

            _control.SwapBuffers(); // swaps front and back buffers (impo when scene changes?)
            _control.Invalidate();
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

        Point previousPosition = new Point(0, 0);
        private void Control_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double dx = e.X - previousPosition.X;
            double dy = e.Y - previousPosition.Y;

            if ((Control.MouseButtons & MouseButtons.Left) != 0)
            {
                _camera.RotateY(-(float)dx / 5);
                _camera.RotateX(-(float)dy / 5);
            }

            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance > 2)
                previousPosition = new Point(e.X, e.Y);
        }
    }
}
