﻿using System;
using System.Collections.Generic;
using System.Drawing;
using GCodeViewer.Library.Renderables.Shapes;
using GCodeViewer.WPF.Controls.Viewer3D;

namespace GCodeViewer.Library.Renderables.Things
{
    public class CircularPrintbed : ICompositeRenderable
    {
        private List<Renderable> _parts = new List<Renderable>();

        public CircularPrintbed(float radius, Color color, Color lineColor, float rotationX = 0, float rotationY = 0)
        {
            float height = 0.01f;
            var printbed = Cylinder.With()
                                   .Position(new Point3D(0, 0, -height))
                                   .Height(height)
                                   .Radius(radius)
                                   .RotationX(rotationX)
                                   .RotationY(rotationY)
                                   .Color(color)
                                   .Build();

            var linePoints = GetLinePoints(radius)
                            .RotateXYX(rotationX, rotationY, 0);
            var lines = new Renderable(lineColor, linePoints, RenderableType.Lines);

            _parts.Add(printbed);
            _parts.Add(lines);
        }

        private List<Point3D> GetLinePoints(float radius, int lineCount = 11)
        {
            var verts = new List<Point3D>();

            float lineSpacing = (2 * radius) / lineCount;

            for (int i = 0; i < lineCount - 1; i++)
            {
                float x = i * lineSpacing;
                float y = (float)Math.Sqrt((radius * radius) - (x * x));
                float z = 0.001f; // lines have to be slightly above the printbed to be seen

                // vertical Lines
                verts.Add(new Point3D(x, y, z));
                verts.Add(new Point3D(x, -y, z));
                verts.Add(new Point3D(-x, y, z));
                verts.Add(new Point3D(-x, -y, z));

                // horizontal Lines
                verts.Add(new Point3D(y, x, z));
                verts.Add(new Point3D(-y, x, z));
                verts.Add(new Point3D(y, -x, z));
                verts.Add(new Point3D(-y, -x, z));
            }

            return verts;
        }

        public IEnumerable<Renderable> GetParts() => _parts;
    }
}
