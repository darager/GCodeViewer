using System.ComponentModel;
using System.Windows.Controls;

namespace GCodeViewer.WPF.ViewModels
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

        public PagingViewModel(PageNavigationService navService)
        {
            navService.PageChanged += (s, newPage) => Page = newPage;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
