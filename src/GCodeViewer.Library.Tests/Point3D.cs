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
            var point = new Point3D(1, 0, 0);

            point.RotateZ(degZ);

            point.X.Should().BeApproximately(expectedX, 0.01f);
            point.Y.Should().BeApproximately(expectedY, 0.01f);
            point.Z.Should().BeApproximately(expectedZ, 0.01f);
        }

        [Test]
        [TestCase(180, 0, 0, -1, 0, 0)]
        [TestCase(180, 0, 100, -1, 0, 0)]
        [TestCase(90, 90, 0, 0, 0, 1)]
        [TestCase(0, 90, 100, 1, -100, -100)]
        public void Should_RotateACCorrectly(float cDeg, float aDeg, float aAxisOffset, float expectedX, float expectedY, float expectedZ)
        {
            var point = new Point3D(1, 0, 0);

            point.RotateCA(cDeg, aDeg, aAxisOffset);

            point.X.Should().BeApproximately(expectedX, 0.01f);
            point.Y.Should().BeApproximately(expectedY, 0.01f);
            point.Z.Should().BeApproximately(expectedZ, 0.01f);
        }
    }
}
