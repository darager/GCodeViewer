using System;
using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorViewModel : INotifyPropertyChanged, ITextEditor
    {
        public ICommand HandleTextChanged { get; private set; }

        public GCodeTextEditor TextEditor { get; internal set; }

        public TextEditorViewModel()
        {
            HandleTextChanged = new RelayCommand((_) => TextChanged?.Invoke(this, EventArgs.Empty));
        }

        public string GetText()
        {
            return TextEditor.Text;
        }
        public void SetText(string text)
        {
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
