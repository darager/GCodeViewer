using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class GcodePointExtractorTests
    {
        private readonly static object[] _extractPointsTestCases =
        {
            new object[]
            {
                new string[] { "X100.0 Y9.0", "Z10"},
                new List<Point3D> { new Point3D(100,9,0), new Point3D(100,9,10) }
            },
            new object[]
            {
                new string[] { "X0.0 Y0.0 Z0.0", "X10", "X10.0 Z0.0", "Y10.0" },
                new List<Point3D> { new Point3D(0,0,0), new Point3D(10,0,0), new Point3D(10,10,0) }
            }
        };
        [TestCaseSource(nameof(_extractPointsTestCases))]
        public void ExtractPoints_ExtractsCorrectPoints(string[] lines, IEnumerable<Point3D> expectedPoints)
        {
            var pointExtractor = new GCodePointExtractor();

            var actual = pointExtractor.ExtractPoints(lines);

            actual.Should().Equal(expectedPoints);
        }
    }
}