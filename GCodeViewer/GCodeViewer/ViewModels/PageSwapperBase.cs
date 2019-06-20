using GCodeViewer.Abstractions.ViewModels;
using Ninject;
using System.ComponentModel;
using System.Windows.Controls;

namespace GCodeViewer.ViewModels
{
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

        public void SetStartupPage() => SwapToOpenFilePage();
        public void SwapToOpenFilePage() => CurrentPage = OpenFilePage;
        public void SwapToLiveEditorPage() => CurrentPage = LiveEditorPage;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
