using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Views;
using System;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using GCodeViewer.Interfaces;

namespace GCodeViewer.ViewModels
{
    public class PageSwapper : IPageLocator, INotifyPropertyChanged
    {
        [Inject, Named("OpenFilePage")]
        public Page OpenFilePage { get; set; }
        [Inject,  Named("LiveEditorPage")]
        public Page LiveEditorPage { get; set; }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if(_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }

        private Page _currentPage;

        public void SetStartupPage()
        {
            SwapToOpenFilePage();
        }
        public void SwapToOpenFilePage()
        {
            CurrentPage = OpenFilePage;
        }
        public void SwapToLiveEditorPage()
        {
            CurrentPage = LiveEditorPage;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
