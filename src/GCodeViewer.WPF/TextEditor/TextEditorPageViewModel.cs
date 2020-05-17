using System;
using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorPageViewModel : INotifyPropertyChanged
    {

        public GCodeTextEditor TextEditor { get; internal set; }

        public TextEditorPageViewModel()
        {
            HandleTextChanged = new RelayCommand((_) => TextChanged?.Invoke(this, EventArgs.Empty));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
