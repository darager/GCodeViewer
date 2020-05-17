using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorViewModel : INotifyPropertyChanged, ITextEditor
    {
        public string GetText()
        {
            throw new NotImplementedException();
        }
        public void SetText(string text)
        {
            throw new NotImplementedException();
        }

        public event EventHandler TextChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
