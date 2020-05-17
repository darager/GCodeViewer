using System;
using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorPageViewModel : INotifyPropertyChanged, ITextEditor
    {
        public ICommand OpenOtherFile { get; private set; }
        public ICommand SaveFile { get; private set; }
        public ICommand SaveFileAs { get; private set; }
        public ICommand CloseFile { get; private set; }

        public ICommand HandleTextChanged { get; internal set; }

        internal GCodeTextEditor TextEditor { get; set; }

        public TextEditorPageViewModel()
        {
            HandleTextChanged = new RelayCommand((_) => TextChanged?.Invoke(this, EventArgs.Empty));
        }

        public void SetText(string text)
        {
            TextEditor.Text = text;
        }
        public string GetText()
        {
            return TextEditor.Text;
        }

        public event EventHandler TextChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
