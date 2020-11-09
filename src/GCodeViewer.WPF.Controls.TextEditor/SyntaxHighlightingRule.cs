using System.Text.RegularExpressions;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    public class SyntaxHighlightingRule
    {
        private string _pattern;
        private Color _color;

        public SyntaxHighlightingRule(string pattern, Color color)
        {
            _pattern = pattern;
            _color = color;
        }

        internal HighlightingRule GetHighlightingRule()
        {
            return new HighlightingRule()
            {
                Regex = new Regex(_pattern),
                Color = new HighlightingColor()
                {
                    Foreground = new CustomHighlightingBrush(new SolidColorBrush(_color))
                }
            };
        }
    }
}
