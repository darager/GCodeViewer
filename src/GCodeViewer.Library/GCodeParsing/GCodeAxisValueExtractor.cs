using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GCodeViewer.Helpers;
using GCodeViewer.Library.PrinterSettings;

namespace GCodeViewer.Library.GCodeParsing
{
    // TODO: Refactor this class
    public class GCodeAxisValueExtractor
    {
        private Dictionary<string, Regex> _expressions = new Dictionary<string, Regex>();
        private string _floatPattern = "(\\d+\\.\\d+|\\d+)";

        public IEnumerable<AxisValues> ExtractAxisValues(IEnumerable<string> lines, AAxisParserInfo aAxisInfo, CAxisParserInfo cAxisInfo)
        {
            var position = new AxisValues(0, 0, 0, 0);
            var prevPosition = AxisValues.NaN;

            var aAxisPattern = GetAxisSpecificPattern(aAxisInfo.GCodePattern);
            var cAxisPattern = GetAxisSpecificPattern(cAxisInfo.GCodePattern);

            foreach (string gCodeLine in lines)
            {
                string line = RemoveComment(gCodeLine);

                if (ContainsValue("X", line))
                    position.X = ExtractValue("X", line);
                if (ContainsValue("Y", line))
                    position.Y = ExtractValue("Y", line);
                if (ContainsValue("Z", line))
                    position.Z = ExtractValue("Z", line);

                if (Contains(aAxisPattern, line))
                    position.A = ExtractAAxisDegree(aAxisPattern, line, aAxisInfo);
                if (Contains(cAxisPattern, line))
                    position.C = ExtractCAxisDegree(cAxisPattern, line, cAxisInfo);

                if (ContainsValue("E", line))
                    position.E = ExtractValue("E", line);

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

        private bool ContainsValue(string c, string text)
        {
            Regex regex = GetNumberRegex(c);
            return Contains(regex, text);
        }

        private bool Contains(Regex regex, string text)
        {
            return regex.Match(text).Success;
        }

        private float ExtractValue(string c, string text)
        {
            Regex regex = GetNumberRegex(c);

            return ExtractFloat(text, regex);
        }

        private static float ExtractFloat(string text, Regex regex)
        {
            Match match = regex.Match(text);
            string number = match.Groups[1].ToString();

            if (string.IsNullOrEmpty(number))
                return 0;

            return float.Parse(number);
        }

        private Regex GetNumberRegex(string c)
        {
            if (_expressions.ContainsKey(c))
                return _expressions[c];

            var regex = new Regex(c + _floatPattern);
            _expressions[c] = regex;

            return regex;
        }

        private Regex GetAxisSpecificPattern(string gCodePattern)
        {
            string pattern = gCodePattern;

            pattern = pattern.Replace(" ", "\\s*");
            pattern = pattern.Replace("{{value}}", _floatPattern);

            return new Regex(pattern);
        }

        private float ExtractAAxisDegree(Regex aAxisPattern, string text, AAxisParserInfo info)
        {
            float number = ExtractFloat(text, aAxisPattern);

            return number.Scale(
                            info.MinValueAAxis,
                            info.MaxValueAAxis,
                            info.MinDegreesAAxis,
                            info.MaxDegreesAAxis);
        }

        private float ExtractCAxisDegree(Regex cAxisPattern, string text, CAxisParserInfo info)
        {
            float number = ExtractFloat(text, cAxisPattern);

            return number / info.ValueAt360Degrees * 360;
        }
    }
}
