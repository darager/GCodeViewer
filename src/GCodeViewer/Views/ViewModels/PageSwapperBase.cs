using GCodeViewer.WPF.Abstractions.ViewModels;
using Ninject;
using System.ComponentModel;
using System.Windows.Controls;

namespace GCodeViewer.WPF.Views.ViewModels
{
    public enum FramePage
    {
        OpenFile, LiveEditor
    }
    public class PageSwapperBase : IPageLocator, INotifyPropertyChanged
    {
        [Inject, Named("OpenFilePage")]
        public Page OpenFilePage { get; set; }
        [Inject, Named("LiveEditorPage")]
        public Page LiveEditorPage { get; set; }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value)
                {
                    return;
                }

                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        private Page _currentPage;

        public void SetStartupPage() => SwapPage(FramePage.OpenFile);
        public void SwapPage(FramePage newPage)
        {
            switch (newPage)
            {
                case FramePage.OpenFile:
                    CurrentPage = OpenFilePage;
                    break;
                case FramePage.LiveEditor:
                    CurrentPage = LiveEditorPage;
                    break;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
