using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using Microsoft.Win32;

namespace OpenTkTest.ViewModels
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        public ICommand OpenFile { get; private set; }

        private readonly TextEditorViewModel _editorVM;

        public StatusBarViewModel(TextEditorViewModel editorVM)
        {
            _editorVM = editorVM;

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
                string content = File.ReadAllText(filePath);

                _editorVM.Text = content;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
