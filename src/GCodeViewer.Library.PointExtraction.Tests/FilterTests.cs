using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.Library.Tests
{
    [TestFixture]
    public class FilterTests
    {
        private readonly static object[] _caseSource =
        {
            new object[]
            {
                new List<AxisValues>()
                {
                    new AxisValues(0, 0, 0, 0),
                    new AxisValues(10, 3, 0, 0),
                    new AxisValues(10, 3, 0, 2),
                    new AxisValues(0, 0, 0, 0)
                },
                new List<AxisValues>()
                {
                    new AxisValues(0, 0, 0, 0),
                    new AxisValues(10, 3, 0, 2),
                    new AxisValues(0, 0, 0, 0)
                }
            }
        };

        [TestCaseSource(nameof(_caseSource))]
        public void FilterNonExtrudingValues_ReturnsExtrudingValues(IEnumerable<AxisValues> values, IEnumerable<AxisValues> expectedValues)
        {
            var filter = new AxisValueFilter();

            var actual = filter.FilterNonExtrudingValues(values);

            actual.Should().Equal(expectedValues);
        }
    }
}