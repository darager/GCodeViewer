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
        private Dictionary<Navigation, Page> _pages = new Dictionary<Navigation, Page>();

        public PageNavigationService(StartingPage startingPage, TextEditorPage textEditorPage)
        {
            _pages.Add(Navigation.StartingPage, startingPage);
            _pages.Add(Navigation.GCodePreviewPage, textEditorPage);
        }

        public void GoTo(Navigation page)
        {
            SetPage(_pages[page]);
        }

        private void SetPage(Page page)
        {
            PageChanged?.Invoke(this, page);
        }

        public event EventHandler<Page> PageChanged;
    }
}
