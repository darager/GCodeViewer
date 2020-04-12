﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GCodeViewer.WPF.Controls.Pointcloud.Camera;
using GCodeViewer.WPF.Controls.Pointcloud.Shaders;
using GCodeViewer.WPF.Controls.PointCloud;
using GCodeViewer.WPF.Controls.PointCloud.Primitives;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Point = System.Windows.Point;

namespace GCodeViewer.WPF.Controls.Pointcloud
{
    public class PointCloudViewer : WindowsFormsHost
    {
        private readonly GLControl _control;
        private readonly OrbitCamera _camera;

        internal readonly ShaderBuilder _shaderBuilder;
        internal Dictionary<Renderable, VertexBufferObject> _vbos = new Dictionary<Renderable, VertexBufferObject>();

        public ObservableCollection<Renderable> Renderables
        {
            get => (ObservableCollection<Renderable>)this.GetValue(RenderablesProperty);
            set => this.SetValue(RenderablesProperty, value);
        }
        public static readonly DependencyProperty RenderablesProperty =
            DependencyProperty.Register(
                "Renderables",
                typeof(ObservableCollection<Renderable>),
                typeof(PointCloudViewer),
                new FrameworkPropertyMetadata(DoSomething));

        private static void DoSomething(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var pclViewer = (PointCloudViewer)source;

            if (args.OldValue != null)
            {
                var oldRenderables = (ObservableCollection<Renderable>)args.OldValue;
                foreach (Renderable renderable in oldRenderables)
                    pclViewer._vbos.Remove(renderable);
            }

            if (args.NewValue != null)
            {
                pclViewer.Renderables = (ObservableCollection<Renderable>)args.NewValue;
                foreach (var renderable in pclViewer.Renderables)
                    AddRenderable(pclViewer, renderable);
                pclViewer.Renderables.CollectionChanged += (_, e) => Renderables_CollectionChanged(e, pclViewer);
            }
        }

        private static void Renderables_CollectionChanged(NotifyCollectionChangedEventArgs args, PointCloudViewer pclViewer)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Renderable renderable in args.NewItems)
                {
                    AddRenderable(pclViewer, renderable);
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Renderable renderable in args.OldItems)
                    pclViewer._vbos.Remove(renderable);
            }
            else
                throw new NotImplementedException();
        }

        private static void AddRenderable(PointCloudViewer pclViewer, Renderable renderable)
        {
            var shader = pclViewer._shaderBuilder.FromColor(renderable.Color);
            var vbo = new VertexBufferObject(renderable.Vertices, renderable.Type, shader);

            pclViewer._vbos.Add(renderable, vbo);
        }

        public PointCloudViewer()
        {
            _control = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default)
            {
                Dock = DockStyle.Fill
            };
            _control.MakeCurrent(); // makes GL.something refer to this control

            this.Child = _control;

            _shaderBuilder = new ShaderBuilder();
            _camera = new OrbitCamera(startScale: 0.5f, _shaderBuilder);

            _control.Paint += OnPaint;
            _control.MouseMove += OnMouseMove;
            _control.MouseWheel += OnMouseWheel;

            this.Unloaded += OnUnloaded;
            this.SizeChanged += OnSizeChanged;

            GL.Enable(EnableCap.DepthTest);
            GL.EnableVertexAttribArray(0);

            _control.Invalidate(); // causes control to be redrawn
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.117f, 0.117f, 0.117f, 1.0f);

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.ProgramPointSize);

            _camera.ApplyTransformation();

            foreach (var vbo in _vbos.Values)
                vbo.Draw();

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

            foreach (var vbo in _vbos.Values)
                vbo.Dispose();

            _shaderBuilder.DisposeAll();
        }

        private Point _previousMousePosition = new Point(0, 0);
        private readonly float _mouseSensitivity = 0.25f;
        private readonly float _mouseWheelSensitivity = 0.05f;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            float dx = (float)(e.X - _previousMousePosition.X);
            float dy = (float)(e.Y - _previousMousePosition.Y);

            // TODO: Rotation should be relative to the current rotation!
            if ((Control.MouseButtons & MouseButtons.Left) != 0)
            {
                float newRotationX = (-dy * _mouseSensitivity) + _camera.RotationX;
                if (newRotationX >= 90) newRotationX = 90;
                if (newRotationX <= -90) newRotationX = -90;
                _camera.RotationX = newRotationX;

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