using System.Text.RegularExpressions;

namespace GCodeViewer.WPF.Resources
{
    public class RegexBuilder
    {
        public Regex GetAxisPattern(char axisChar)
        {
            string pattern = "\\s" + axisChar + "(\\d+\\.\\d+|\\d+)\\s";
            var regex = new Regex(pattern);

            return regex;
        }
    }
}
