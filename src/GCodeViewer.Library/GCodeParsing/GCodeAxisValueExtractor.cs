using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GCodeViewer.Library.GCodeParsing
{
    public class GCodeAxisValueExtractor
    {
        private Dictionary<string, Regex> _expressions = new Dictionary<string, Regex>();

        // TODO: make images of this class
        public IEnumerable<AxisValues> ExtractAxisValues(IEnumerable<string> lines)
        {
            var position = new AxisValues(0, 0, 0, 0);
            var prevPosition = AxisValues.NaN;

            foreach (string line in lines)
            {
                if (ContainsValue("X", line))
                    position.X = ExtractValue("X", line);
                if (ContainsValue("Y", line))
                    position.Y = ExtractValue("Y", line);
                if (ContainsValue("Z", line))
                    position.Z = ExtractValue("Z", line);

                // HACK: can be configured in the future
                if (ContainsValue("A", line))
                    position.A = ExtractValue("A", line);
                if (ContainsValue("C", line))
                    position.C = ExtractValue("C", line);

                if (ContainsValue("E", line))
                    position.E = ExtractValue("E", line);

                if (prevPosition != position)
                    yield return position;

                prevPosition = position;
            }
        }

        private bool ContainsValue(string c, string text)
        {
            Regex regex = GetNumberRegex(c);
            bool result = regex.Match(text).Success;

            return result;
        }

        private float ExtractValue(string c, string text)
        {
            Regex regex = GetNumberRegex(c);

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

            var regex = new Regex(c + "(\\d+\\.\\d+|\\d+)");
            _expressions[c] = regex;

            return regex;
        }
    }
}
