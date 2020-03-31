using GCodeViewer.WPF.Views.ViewModels;
using System.Windows.Controls;

namespace GCodeViewer.WPF.Abstractions.ViewModels
{
    public interface IPageLocator
    {
        Page OpenFilePage { get; set; }
        Page LiveEditorPage { get; set; }

        void SetStartupPage();
        void SwapPage(FramePage page);
    }
}
