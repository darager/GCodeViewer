using System.Text.RegularExpressions;

namespace GCodeViewer.Library.PointExtraction
{
    public class PointExtractor
    {
        public (float, float, float) ExtractPoint(string text)
        {
            float x = ExtractValue('X', text);
            float y = ExtractValue('Y', text);
            float z = ExtractValue('Z', text);

            return (x, y, z);
        }

        private float ExtractValue(char c, string text)
        {
            Regex regex = GetNumberRegex(c);

            var match = regex.Match(text);
            var number = match.Groups[1].ToString();
            float value = float.Parse(number);

            return value;
        }
        private Regex GetNumberRegex(char c)
        {
            return new Regex(c + "(\\d+\\.\\d+|\\d+)");
        }
    }
}
