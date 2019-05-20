using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Diagnostics;

namespace GCodeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddCircleModel();
        }

        private void Textblock_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine(e.Changes.ToString());
        }
        private void AddCircleModel()
        {
            var mod = GetCircleModel(3.0, new Vector3D(0, 1, 1), new Point3D(0, -1, 0), 2000);
            mod.Material = new DiffuseMaterial(Brushes.Silver);
            var vis = new ModelVisual3D() { Content = mod };
            mainViewPort.Children.Add(vis);
        }
        private GeometryModel3D GetCircleModel(double radius, Vector3D normal, Point3D center, int resolution)
        {
            var mod = new GeometryModel3D();
            var geo = new MeshGeometry3D();

            // Generate the circle in the XZ-plane
            // Add the center first
            geo.Positions.Add(new Point3D(0, 0, 0));

            // Iterate from angle 0 to 2*PI
            double t = 2 * Math.PI / resolution;
            for (int i = 0; i < resolution; i++)
            {
                geo.Positions.Add(new Point3D(radius * Math.Cos(t * i), 0, -radius * Math.Sin(t * i)));
            }

            // Add points to MeshGeoemtry3D
            for (int i = 0; i < resolution * 3; i++)
            {
                var a = 0;
                var b = i + 1;
                var c = (i < (resolution - 1)) ? i + 2 : 1;

                geo.TriangleIndices.Add(a);
                geo.TriangleIndices.Add(b);
                geo.TriangleIndices.Add(c);
            }

            mod.Geometry = geo;

            // Create transforms
            var trn = new Transform3DGroup();
            // Up Vector (normal for XZ-plane)
            var up = new Vector3D(0, 1, 0);
            // Set normal length to 1
            normal.Normalize();
            var axis = Vector3D.CrossProduct(up, normal); // Cross product is rotation axis
            var angle = Vector3D.AngleBetween(up, normal); // Angle to rotate
            trn.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, angle)));
            trn.Children.Add(new TranslateTransform3D(new Vector3D(center.X, center.Y, center.Z)));

            mod.Transform = trn;

            return mod;
        }
    }
}
