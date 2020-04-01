using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.Library.PointExtraction.Tests
{
    [TestFixture]
    public class PointExtractorTests
    {
        private static object[] _extractUniquePointsTestCases =
        {
            new object[]
            {
                new string[] { "X100.0 Y9.0", "Z10"},
                new List<(float X, float Y, float Z)> { (100,9,0), (100,9,10) }
            },
            new object[]
            {
                new string[] { "X0.0 Y0.0 Z0.0", "X10", "X10.0 Z0.0", "Y10.0" },
                new List<(float X, float Y, float Z)> { (0,0,0), (10,0,0), (10,10,0) }
            }
        };
        [TestCaseSource(nameof(_extractUniquePointsTestCases))]
        public void ExtractPoints_ExtractsCorrectUniquePoints(string[] lines, IEnumerable<(float X, float Y, float Z)> expectedPoints)
        {
            var pointExtractor = new PointExtractor();

            var actual = pointExtractor.ExtractUniquePoints(lines);

            actual.Should().Equal(expectedPoints);
        }
    }
}