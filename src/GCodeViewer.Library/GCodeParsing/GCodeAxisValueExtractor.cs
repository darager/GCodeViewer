using System.Collections.Generic;
using System.Text.RegularExpressions;
using GCodeViewer.Helpers;
using GCodeViewer.Library.PrinterSettings;

namespace GCodeViewer.Library.GCodeParsing
{
    public class GCodeAxisValueExtractor
    {
        private Regex _xPattern;
        private Regex _yPattern;
        private Regex _zPattern;
        private Regex _ePattern;

        public GCodeAxisValueExtractor()
        {
            _xPattern = "X{{value}}".ConstructPattern();
            _yPattern = "Y{{value}}".ConstructPattern();
            _zPattern = "Z{{value}}".ConstructPattern();
            _ePattern = "E{{value}}".ConstructPattern();
        }

        public IEnumerable<AxisValues> ExtractAxisValues(IEnumerable<string> lines, AAxisParserInfo aAxisInfo, CAxisParserInfo cAxisInfo)
        {
            var position = new AxisValues(x: 0, y: 0, z: 0, e: 0, a: 0, c: 0);
            var prevPosition = AxisValues.NaN;

            var aAxisPattern = aAxisInfo.GCodePattern.ConstructPattern();
            var cAxisPattern = cAxisInfo.GCodePattern.ConstructPattern();

            foreach (string gCodeLine in lines)
            {
                string line = RemoveComment(gCodeLine);

                if (line.Contains(_xPattern))
                    position.X = line.ExtractValue(_xPattern);
                if (line.Contains(_yPattern))
                    position.Y = line.ExtractValue(_yPattern);
                if (line.Contains(_zPattern))
                    position.Z = line.ExtractValue(_zPattern);

                if (line.Contains(aAxisPattern))
                {
                    position.A = line.ExtractValue(aAxisPattern)
                                       .Scale(aAxisInfo.MinValueAAxis,
                                              aAxisInfo.MaxValueAAxis,
                                              aAxisInfo.MinDegreesAAxis,
                                              aAxisInfo.MaxDegreesAAxis);

                    line = aAxisPattern.Replace(line, "");
                }

                if (line.Contains(cAxisPattern))
                {
                    position.C = line.ExtractValue(cAxisPattern)
                                        / cAxisInfo.ValueAt360Degrees * 360;
                    line = cAxisPattern.Replace(line, "");
                }

                // Extracting the Extruder value has to happen after the A/C-Axis
                // Otherwise the value of the A or C-Axis could be seen as the value of the Extruder
                if (line.Contains(_ePattern))
                    position.E = line.ExtractValue(_ePattern);

                if (prevPosition != position)
                    yield return position;

                prevPosition = position;
            }
        }

        private string RemoveComment(string line)
        {
            if (line.Contains(";"))
                line = line.Split(";")[0];

            return line;
        }
    }

    public static class GCodeLineExtensions
    {
        public static bool Contains(this string @this, Regex pattern)
        {
            return pattern.Match(@this).Success;
        }

        public static float ExtractValue(this string @this, Regex pattern)
        {
            Match match = pattern.Match(@this);
            string number = match.Groups[1].ToString();

            if (string.IsNullOrEmpty(number))
                return 0;

            return float.Parse(number);
        }

        public static Regex ConstructPattern(this string @this)
        {
            string floatPattern = "(\\d+\\.\\d+|\\d+)";

            string pattern = @this.Trim()
                                  .Replace("{{value}}", floatPattern);

            return new Regex(pattern);
        }
    }
}
