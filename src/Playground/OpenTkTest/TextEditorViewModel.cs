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

        private Viewer3DViewModel _viewer3DVM;

        public TextEditorViewModel(Viewer3DViewModel viewerVM)
        {
            _viewer3DVM = viewerVM;

            HandleTextChanged = new RelayCommand((o) =>
            {
                string newText = o as string;
                _viewer3DVM.Update3DModel(newText);
            });
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
