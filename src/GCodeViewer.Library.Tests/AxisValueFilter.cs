using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Common;
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
            var values = new List<AxisValues>()
            {
                new AxisValues(0, 0, 0, 0),
                new AxisValues(10, 3, 0, 1),
                new AxisValues(10, 0, 3, 2),
                new AxisValues(100, 100, 100, 1.9f)
            };
            var expected = new List<AxisValues>()
            {
                new AxisValues(10, 3, 0, 1),
                new AxisValues(10, 0, 3, 2)
            };

            var actual = values.RemoveNonExtruding();

            actual.Should().HaveCount(2);
            expected[0].Should().IsSameOrEqualTo(values[1]);
            expected[1].Should().IsSameOrEqualTo(values[2]);
        }
    }
}
