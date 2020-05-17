using System;
using System.Collections.Generic;
using System.Windows.Controls;
using GCodeViewer.WPF.Pages;
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

        private Page GetPage(Navigation page)
        {
            return page switch
            {
                Navigation.StartingPage => StartingPage,
                Navigation.SettingsPage => SettingsPage,

                _ => throw new Exception("this page has not been added yet")
            };
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
            PageChanged?.Invoke(this, GetPage(page));
        }

        public event EventHandler<Page> PageChanged;
    }
}
