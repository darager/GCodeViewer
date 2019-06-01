using System;
using System.Windows.Controls;

namespace GCodeViewer.Interfaces.ViewModels
{
    public interface IPageLocator
    {
        Page GetOpenFilePage();
        Page GetMainPage();
    }
}
