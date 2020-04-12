using System.Collections.Generic;

namespace GCodeViewer.Library.Tests
{
    public class AxisValueFilter
    {
        public IEnumerable<AxisValues> RemoveNonExtrudingPoints(IEnumerable<AxisValues> values)
        {
            var prev = AxisValues.NaN;

            foreach (AxisValues value in values)
            {
                if (prev.E != value.E)
                    yield return value;

                prev = value;
            }
        }
    }
}