using System;
using System.ComponentModel;

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
                TextChanged?.Invoke(this, Text);
            }
        }
        private string _text;

        public event EventHandler<string> TextChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
