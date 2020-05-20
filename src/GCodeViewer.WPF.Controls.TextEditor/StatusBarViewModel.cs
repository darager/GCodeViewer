using System;
using System.ComponentModel;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    internal class StatusBarViewModel : INotifyPropertyChanged
    {
        public int CurrentLine
        {
            get => _currentLine;
            set
            {
                if (_currentLine == value) return;

                _currentLine = value;
                OnPropertyChanged("CurrentLine");
            }
        }
        private int _currentLine;

        public int LineCount
        {
            get => _lineCount;
            set
            {
                if (_lineCount == value) return;

                _lineCount = value;
                OnPropertyChanged("LineCount");
            }
        }
        private int _lineCount;

        private readonly ICSharpCode.AvalonEdit.TextEditor _editor;

        public StatusBarViewModel(ICSharpCode.AvalonEdit.TextEditor editor)
        {
            _editor = editor;

            BindCurrentLineUpdates();
        }

        private void BindCurrentLineUpdates()
        {
            var updateLineStatistics = ((Action)UpdateLineStatistics).Throttle(10);

            _editor.TextChanged += (s, e) => updateLineStatistics();
            _editor.PreviewKeyDown += (s, e) => updateLineStatistics();
            _editor.GotMouseCapture += (s, e) => updateLineStatistics();
        }
        private void UpdateLineStatistics()
        {
            LineCount = _editor.Document.LineCount;

            int offset = _editor.CaretOffset;
            var textlocation = _editor.Document.GetLocation(offset);
            CurrentLine = textlocation.Line;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
