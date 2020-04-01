using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GCodeViewer.Library
{
    public class PointExtractor
    {
        private Dictionary<char, Regex> _expressions = new Dictionary<char, Regex>();

        public List<(float X, float Y, float Z)> ExtractUniquePoints(IEnumerable<string> lines)
        {
            var result = new List<(float X, float Y, float Z)>();

            (float X, float Y, float Z) position = (0, 0, 0);
            (float X, float Y, float Z) prevPosition = (float.NaN, float.NaN, float.NaN);

            foreach (string line in lines)
            {
                if (ContainsValue('X', line))
                    position.X = ExtractValue('X', line);
                if (ContainsValue('Y', line))
                    position.Y = ExtractValue('Y', line);
                if (ContainsValue('Z', line))
                    position.Z = ExtractValue('Z', line);

                if (prevPosition != position)
                    result.Add(position);

                prevPosition = position;
            }

            return result;
        }

        private bool ContainsValue(char c, string text)
        {
            Regex regex = GetNumberRegex(c);
            bool result = regex.Match(text).Success;

            return result;
        }
        private float ExtractValue(char c, string text)
        {
            Regex regex = GetNumberRegex(c);

            Match match = regex.Match(text);
            string number = match.Groups[1].ToString();

            if (string.IsNullOrEmpty(number))
                return 0;

            float value = float.Parse(number);

            return value;
        }
        private Regex GetNumberRegex(char c)
        {
            if (_expressions.ContainsKey(c))
                return _expressions[c];

            var regex = new Regex(c + "(\\d+\\.\\d+|\\d+)");
            _expressions[c] = regex;

            return regex;
        }
    }
}
