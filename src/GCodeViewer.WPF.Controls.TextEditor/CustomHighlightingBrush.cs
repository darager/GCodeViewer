using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using System.Windows.Media;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    internal class CustomHighlightingBrush : HighlightingBrush
    {
        private Brush _brush;

        public CustomHighlightingBrush(Brush brush)
        {
            _brush = brush;
        }

        public override Brush GetBrush(ITextRunConstructionContext context)
        {
            return _brush;
        }

        public override Color? GetColor(ITextRunConstructionContext context)
        {
            return base.GetColor(context);
        }
    }
}
