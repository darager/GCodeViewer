using FluentAssertions;
using GCodeViewer.Library.Renderables;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class Point3DOperations
    {
        [Test]
        [TestCase(0, 0.0f, 0.0f, 1.0f)]
        [TestCase(90, 0.0f, -1.0f, 0.0f)]
        [TestCase(-90, 0.0f, 1.0f, 0.0f)]
        public void Should_RotateXCorrectly(float degX, float expectedX, float expectedY, float expectedZ)
        {
            var expected = new Point3D(expectedX, expectedY, expectedZ);
            var point = new Point3D(0, 0, 1);

            point.RotateX(degX);

            point.X.Should().BeApproximately(expectedX, 0.01f);
            point.Y.Should().BeApproximately(expectedY, 0.01f);
            point.Z.Should().BeApproximately(expectedZ, 0.01f);
        }

        [Test]
        [TestCase(0, 0.0f, 0.0f, 1.0f)]
        [TestCase(90, 1.0f, 0.0f, 0.0f)]
        [TestCase(-90, -1.0f, 0.0f, 0.0f)]
        public void Should_RotateYCorrectly(float degY, float expectedX, float expectedY, float expectedZ)
        {
            var expected = new Point3D(expectedX, expectedY, expectedZ);
            var point = new Point3D(0, 0, 1);

            point.RotateY(degY);

            point.X.Should().BeApproximately(expectedX, 0.01f);
            point.Y.Should().BeApproximately(expectedY, 0.01f);
            point.Z.Should().BeApproximately(expectedZ, 0.01f);
        }

        [Test]
        [TestCase(0, 1.0f, 0.0f, 0.0f)]
        [TestCase(90, 0.0f, 1.0f, 0.0f)]
        [TestCase(-90, 0.0f, -1.0f, 0.0f)]
        public void Should_RotateZCorrectly(float degZ, float expectedX, float expectedY, float expectedZ)
        {
            var expected = new Point3D(expectedX, expectedY, expectedZ);
            var point = new Point3D(1, 0, 0);

            point = point.RotateZ(degZ);

            point.X.Should().BeApproximately(expectedX, 0.01f);
            point.Y.Should().BeApproximately(expectedY, 0.01f);
            point.Z.Should().BeApproximately(expectedZ, 0.01f);
        }
    }
}
