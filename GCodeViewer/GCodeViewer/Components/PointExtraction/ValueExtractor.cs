using System;
using System.Text.RegularExpressions;

namespace GCodeViewer.WPF.Components.PointExtraction
{
    public class ValueExtractor
    {
        private Regex numberRegex = new Regex("(\\d+|\\d+\\.\\d+)");

        public double ExtractValue(string matchValue)
        {
            string valueString = numberRegex.Match(matchValue).Value;
            double value = Convert.ToDouble(valueString);

            return value;
        }
    }
}
