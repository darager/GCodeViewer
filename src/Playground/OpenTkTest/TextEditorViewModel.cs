using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;

namespace OpenTkTest
{
    public class TextEditorViewModel : INotifyPropertyChanged
    {
        public string Text
        {
            get => _text;
            set
            {
                if (value == _text)
                    return;

                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private string _text;

        public ICommand HandleTextChanged { get; private set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            HandleTextChanged = new RelayCommand((text) =>
            {
                int i = 0;
                i++;
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
