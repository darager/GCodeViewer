using System.Collections.Generic;
using FluentAssertions;
using GCodeViewer.Library.GCodeParsing;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class AxisValueFilterTests
    {
        [Test]
        public void FilterNonExtrudingValues_ReturnsExtrudingValues()
        {
            var values = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(10, 3, 0, 0),
                new AxisValues(10, 3, 0, 2),
                new AxisValues(0, 0, 0, 0)
            };
            var expected = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(10, 3, 0, 2),
                new AxisValues(0, 0, 0, 0)
            };

            var filter = new AxisValueFilter();

            var actual = filter.FilterNonExtrudingValues(values);

            actual.Should().Equal(expected);
        }
    }
}
