using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GCodeViewer.Library.GCodeParsing;
using GCodeViewer.Library.PrinterSettings;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class AxisValueExtraction
    {
        private AAxisParserInfo _aAxisInfo = new AAxisParserInfo()
        {
            GCodePattern = "A{{value}}"
        };

        private CAxisParserInfo _cAxisInfo = new CAxisParserInfo()
        {
            GCodePattern = "C{{value}}"
        };

        [Test]
        public void Should_WorkForXYZE()
        {
            string[] gcodeInput = new[]{"X0 Y0 Z0 E0",
                                        "X1 Z100 E5.3",
                                        "Y5"};

            var expected = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(1, 0, 100, 5.3f),
                new AxisValues(1, 5, 100, 5.3f)
            };

            var extractor = new GCodeAxisValueExtractor();
            var positions = extractor.ExtractAxisValues(gcodeInput, _aAxisInfo, _cAxisInfo).ToList();

            positions.Should().Equal(expected);
        }

        [Test]
        public void Should_WorksForAllAxes()
        {
            string[] gcodeInput = new[]{"A0 C5",
                                        "X1 Z100 E5.3 A15",
                                        "Y5 A5 C5.001"};

            var expected = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0, 0, 5),
                new AxisValues(1, 0, 100, 5.3f, 15, 5),
                new AxisValues(1, 5, 100, 5.3f, 5, 5.001f)
            };

            var extractor = new GCodeAxisValueExtractor();
            var positions = extractor.ExtractAxisValues(gcodeInput, _aAxisInfo, _cAxisInfo).ToList();

            positions.Should().Equal(expected);
        }
    }
}
