using System;
using System.Collections.Generic;
using System.Windows.Controls;
using GCodeViewer.WPF.Pages;

namespace GCodeViewer.WPF
{
    public enum Navigation
    {
        StartingPage,
        STLPositioningPage,
        SettingsPage,
        PrintSurfacePage,
        SliceSettingsPage,
        GCodePreviewPage
    }

    public class PageNavigationService
    {
        private Stack<Navigation> _visitedPages = new Stack<Navigation>();
        private Dictionary<Navigation, Page> _pages = new Dictionary<Navigation, Page>();

        public PageNavigationService(StartingPage startingPage, TextEditorPage textEditorPage)
        {
            _pages.Add(Navigation.StartingPage, startingPage);
            _pages.Add(Navigation.GCodePreviewPage, textEditorPage);
        }

        public void GoTo(Navigation page)
        {
            _visitedPages.Push(page);
            SetPage(page);
        }
        public void GoBack()
        {
            _visitedPages.Pop();
            var page = _visitedPages.Peek();

            SetPage(page);
        }

        private void SetPage(Navigation page)
        {
            PageChanged?.Invoke(this, _pages[page]);
        }

        public event EventHandler<Page> PageChanged;
    }
}
