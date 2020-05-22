using System;
using System.Collections.Generic;
using System.Windows.Controls;
using GCodeViewer.WPF.Settings;
using GCodeViewer.WPF.Starting;
using GCodeViewer.WPF.TextEditor;
using Ninject;

namespace GCodeViewer.WPF.Navigation
{
    public class PageNavigationService
    {
        private Stack<Navigation> _visitedPages = new Stack<Navigation>();

        [Inject]
        public StartingPage StartingPage { get; set; }

        [Inject]
        public SettingsPage SettingsPage { get; set; }

        [Inject]
        public TextEditorPage TextEditorPage { get; set; }

        private Page GetPage(Navigation page) => page switch
        {
            Navigation.StartingPage => StartingPage,
            Navigation.SettingsPage => SettingsPage,
            Navigation.GCodePreviewPage => TextEditorPage,
            _ => throw new Exception("This page is not added in the NavigatorService!")
        };

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
            PageChanged?.Invoke(this, GetPage(page));
        }

        public event EventHandler<Page> PageChanged;
    }
}
