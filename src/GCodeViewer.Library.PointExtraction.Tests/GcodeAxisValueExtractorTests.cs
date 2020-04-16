using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class GcodeAxisValueExtractorTests
    {
        private readonly static object[] _extractPrinterAxisValuesTestCases =
        {
            new object[]
            {
                new string[] { "X100.0 Y9.0", "Z10"},
                new List<AxisValues> { new AxisValues(100,9,0,0), new AxisValues(100,9,10,0) }
            },
            new object[]
            {
                new string[] { "X0.0 Y0.0 Z0.0", "X10", "X10.0 Z0.0", "Y10.0" },
                new List<AxisValues> { new AxisValues(0,0,0,0), new AxisValues(10,0,0,0), new AxisValues(10,10,0,0) }
            },
            new object[]
            {
                new string[] { "X0.0 E1.0", "X10 E2.0", "X10.0 Z0.0", "Y10.0" },
                new List<AxisValues> { new AxisValues(0,0,0,1), new AxisValues(10,0,0,2), new AxisValues(10,10,0,2) }
            }
        };
        [TestCaseSource(nameof(_extractPrinterAxisValuesTestCases))]
        public void ExtractPrinterAxisValues_ExtractsCorrectValues(string[] lines, IEnumerable<AxisValues> expectedValues)
        {
            var axisValueExtractor = new GCodeAxisValueExtractor();

            var actual = axisValueExtractor.ExtractPrinterAxisValues(lines);

            actual.Should().Equal(expectedValues);
        }
    }
}