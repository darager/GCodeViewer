using System;
using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using Microsoft.Win32;

namespace OpenTkTest
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        public ICommand OpenFile { get; private set; }

        public StatusBarViewModel()
        {
            OpenFile = new RelayCommand((_) => StartOpenFileDialogue());
        }

        private void StartOpenFileDialogue()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = "gcode files (*.gcode)|*.gcode|All files (*.*)|*.*";
            ofd.FilterIndex = 2;

            if (ofd.ShowDialog() == true)
            {
                string filePath = ofd.FileName;

            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
