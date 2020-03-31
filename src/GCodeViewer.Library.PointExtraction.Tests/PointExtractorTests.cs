using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.Library.PointExtraction.Tests
{
    [TestFixture]
    public class PointExtractorTests
    {
        private static object[] _testCases =
        {
            new object[] {"X0 Y0 Z0", (0.0f, 0.0f, 0.0f)},
            new object[] {"X24.5 Y0 Z0", (24.5f, 0.0f, 0.0f)},
            new object[] {"X0 Y100.4 Z0", (0.0f, 100.4f, 0.0f)},
            new object[] {"X0 Y100.4 Z3333.34", (0.0f, 100.4f, 3333.34f)}
        };

        [TestCaseSource("_testCases")]
        public void ExtractPoint_ExtractsRightPoint(string content, (float, float, float) expected)
        {
            var pointExtractor = new PointExtractor();

            (float, float, float) actual = pointExtractor.ExtractPoint(content);

            actual.Should().Be(expected);
        }
    }
}