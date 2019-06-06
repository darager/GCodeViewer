using GCodeViewer.Views;
using System;
using System.Windows.Controls;

namespace GCodeViewer.Interfaces
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
