using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    internal class StatusBarViewModel : INotifyPropertyChanged
    {
        public string CurrentLine
        {
            get => _currentLine.ToString();
            set
            {
                if (value == _currentLine.ToString())
                    return;

                if (IsNotNumberOrEmpty(value))
                {
                    CurrentLine = _currentLine.ToString();
                    return;
                }

                int.TryParse(value, out int line);

                _currentLine = line;
                OnPropertyChanged("CurrentLine");
            }
        }
        private int _currentLine;

        public string LineCount { get; set; }
        private int _lineCount;

        public string FontSizeFactor { get; set; }
        private float _fontSizeFactor = 1.0f;


        private ICSharpCode.AvalonEdit.TextEditor _editor;

        public StatusBarViewModel(ICSharpCode.AvalonEdit.TextEditor editor)
        {
            _editor = editor;
            _editor.GotMouseCapture += UpdateCurrentLine;
            _editor.TextChanged += UpdateCurrentLine;
        }

        private void UpdateCurrentLine(object sender, EventArgs e)
        {
            var offset = _editor.CaretOffset;

            var textlocation = _editor.Document.GetLocation(offset);

            // TextEditor.SelectionStart

            //var newloc = new TextLocation(300, 0);
            //TextEditor.CaretOffset = _doc.GetOffset(newloc);

            CurrentLine = textlocation.Line.ToString();
        }
        private bool IsNotNumberOrEmpty(string str)
        {
            return !Regex.IsMatch(str, "^[0-9]*$");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged; // should be in viewmodel instead
    }
}
