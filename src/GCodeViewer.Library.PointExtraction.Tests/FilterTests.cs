using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class FilterTests
    {
        [Test]
        public void FilterTest()
        {
            var values = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(10, 3, 0, 0),
                new AxisValues(10, 3, 0, 2),
                new AxisValues(0, 0, 0, 0)
            };

            var expectedValues = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(10, 3, 0, 2),
                new AxisValues(0, 0, 0, 0)
            };

            var filter = new AxisValueFilter();

            var actual = filter.RemoveNonExtrudingValues(values);

            actual.Should().Equal(expectedValues);
        }
    }
}