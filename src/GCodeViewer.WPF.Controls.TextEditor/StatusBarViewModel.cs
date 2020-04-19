﻿using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;

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

        public int LineCount
        {
            get => _lineCount;
            set
            {
                if (value == LineCount)
                    return;

                _lineCount = value;
                OnPropertyChanged("LineCount");
            }
        }
        private int _lineCount;

        public ICommand IncreaseFontSize { get; private set; }
        public ICommand DecreaseFontSize { get; private set; }
        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (value == _fontSize)
                    return;

                _fontSize = (value >= 1) ? value : 1;

                OnPropertyChanged("FontSize");
            }
        }
        private int _fontSize;

        private readonly ICSharpCode.AvalonEdit.TextEditor _editor;

        public StatusBarViewModel(ICSharpCode.AvalonEdit.TextEditor editor)
        {
            _editor = editor;

            var updateCurrentLine = (Action)UpdateCurrentLine;
            _editor.GotMouseCapture += (s, e) => updateCurrentLine.Throttle(10);
            _editor.TextChanged += (s, e) => updateCurrentLine.Throttle(10);

            FontSize = 12;
            IncreaseFontSize = new RelayCommand((_) => FontSize++);
            DecreaseFontSize = new RelayCommand((_) => FontSize--);
        }

        private void UpdateCurrentLine()
        {
            var offset = _editor.CaretOffset;
            var textlocation = _editor.Document.GetLocation(offset);

            LineCount = _editor.Document.LineCount;
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}