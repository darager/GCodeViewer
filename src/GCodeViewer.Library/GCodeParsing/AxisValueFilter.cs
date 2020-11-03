using System.Collections.Generic;

namespace GCodeViewer.Library.GCodeParsing
{
    public static class AxisValueFilter
    {
        public static IEnumerable<AxisValues> RemoveNonExtruding(this IEnumerable<AxisValues> @this)
        {
            var prevPosition = AxisValues.Zero;

            foreach (AxisValues position in @this)
            {
                if (position.E > prevPosition.E)
                    yield return position;

                prevPosition = position;
            }
        }
    }
}
