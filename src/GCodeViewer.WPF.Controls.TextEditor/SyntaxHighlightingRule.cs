using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using FindReplace;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Text.RegularExpressions;
using ICSharpCode.AvalonEdit.Rendering;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Core.Converters;

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
            var converter = new BrushConverter();

            return new HighlightingRule()
            {
                Regex = new Regex(_pattern),
                Color = new HighlightingColor()
                {
                    Foreground = new CustomHighlightingBrush((Brush)converter.ConvertFrom(_color))
                }
            };
        }
    }
}
