using System;

namespace GCodeViewer.WPF.TextEditor
{
    public interface ITextEditor
    {
        void SetText(string text);

        string GetText();

        event EventHandler TextChanged;
    }
}
