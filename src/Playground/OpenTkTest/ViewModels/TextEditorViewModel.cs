using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;

namespace OpenTkTest.ViewModels
{
    public class TextEditorViewModel : INotifyPropertyChanged
    {
        public string Text
        {
            get => _editor.Text;
            set => _editor.Text = value;
        }

        public ICommand HandleTextChanged { get; private set; }

        private Viewer3DViewModel _viewer3DVM;
        private GCodeTextEditor _editor;

        public TextEditorViewModel(GCodeTextEditor editor, Viewer3DViewModel viewerVM)
        {
            _viewer3DVM = viewerVM;
            _editor = editor;

            HandleTextChanged = new RelayCommand((o) =>
            {
                string newText = o as string;
                _viewer3DVM.Update3DModel(newText);
            });
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
