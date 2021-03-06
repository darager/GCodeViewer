using System.Collections.Generic;
using FluentAssertions;
using GCodeViewer.Library.GCodeParsing;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class AxisValueFilter
    {
        [Test]
        public void Should_RemoveNonExtrudingAxisValues()
        {
            var unfilteredPositions = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(10, 3, 0, 1),
                new AxisValues(10, 0, 3, 2),
                new AxisValues(100, 100, 100, 1.9f)
            };
            var expectedPositions = new List<AxisValues>()
            {
                new AxisValues(10, 3, 0, 1),
                new AxisValues(10, 0, 3, 2)
            };

            var actualPositions = unfilteredPositions.RemoveNonExtruding();

            actualPositions.Should().Equal(expectedPositions);
        }
    }
}
