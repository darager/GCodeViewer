using System;
using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorPageViewModel : INotifyPropertyChanged, ITextEditor
    {
        public ICommand HandleTextChanged { get; private set; }

        public GCodeTextEditor TextEditor { get; internal set; }

        public TextEditorPageViewModel()
        {
            HandleTextChanged = new RelayCommand((_) => TextChanged?.Invoke(this, EventArgs.Empty));
        }

        public string GetText()
        {
            return TextEditor.Text;
        }
        public void SetText(string text)
        {
            // HACK: the texteditor is null even if it has been set before
            if (TextEditor == null) return;

            TextEditor.Text = text;
        }

        public event EventHandler TextChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
