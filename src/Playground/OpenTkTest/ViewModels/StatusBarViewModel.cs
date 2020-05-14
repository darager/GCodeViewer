using System;
using System.IO;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using Microsoft.Win32;

namespace OpenTkTest.ViewModels
{
    public class StatusBarViewModel
    {
        public ICommand OpenFile { get; private set; }

        private readonly TextEditorViewModel _editorVM;

        public StatusBarViewModel(TextEditorViewModel editorVM)
        {
            _editorVM = editorVM;

            OpenFile = new RelayCommand((_) => StartOpenFileDialogue());
        }

        private async void StartOpenFileDialogue()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = "gcode files (*.gcode)|*.gcode";
            ofd.FilterIndex = 2;

            if (ofd.ShowDialog() == true)
            {
                string filePath = ofd.FileName;

                using var reader = File.OpenText(filePath);
                string content = await reader.ReadToEndAsync();

                _editorVM.Text = content;
            }
        }
    }
}
