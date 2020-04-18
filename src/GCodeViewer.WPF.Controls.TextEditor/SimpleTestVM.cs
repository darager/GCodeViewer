using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GCodeViewer.WPF.Controls.TextEditor
{
    internal class SimpleTestVM : INotifyPropertyChanged
    {
        public string CurrentLine
        {
            get => _currentLine;
            set
            {
                //if (value == _currentLine) return;
                if (Regex.IsMatch(value, "[0-9]+"))
                {
                    _currentLine = value;
                    OnPropertyChanged("CurrentLine");
                }
            }
        }
        private string _currentLine;

        public SimpleTestVM()
        {

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged; // should be in viewmodel instead
    }
}
