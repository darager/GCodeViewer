using System.Windows.Controls;

namespace GCodeViewer.Abstractions
{
    public interface IPageLocator
    {
        Page OpenFilePage { get; set; }
        Page LiveEditorPage { get; set; }

        void SetStartupPage();
        void SwapToOpenFilePage();
        void SwapToLiveEditorPage();
    }
}
