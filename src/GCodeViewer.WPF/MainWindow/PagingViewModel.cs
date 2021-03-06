﻿using System.ComponentModel;
using System.Windows.Controls;
using GCodeViewer.WPF.Navigation;

namespace GCodeViewer.WPF.MainWindow
{
    public class PagingViewModel : INotifyPropertyChanged
    {
        private Page _page;

        public Page Page
        {
            get => _page;
            set
            {
                if (_page == value) return;
                _page = value;

                OnPropertyChanged("Page");
            }
        }

        private PageNavigationService _navService;

        public PagingViewModel(PageNavigationService navService)
        {
            _navService = navService;

            _navService.PageChanged += (_, newPage) => Page = newPage;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
