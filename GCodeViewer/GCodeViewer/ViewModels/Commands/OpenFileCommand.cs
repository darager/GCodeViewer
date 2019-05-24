using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GCodeViewer.Commands
{
    public class OpenFileCommand : ICommand
    {
        IFileChooser FileChooser;

        public OpenFileCommand(IFileChooser fileChooser)
        {
            this.FileChooser = fileChooser;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.DefaultExt = ".gcode";
            openFileDialog.Filter = "gcode files (*.gcode)|*.gcode|text files (*.txt)|*.txt|all files (*.*)|*.*";
            Nullable<bool> dialogOk = openFileDialog.ShowDialog();

            if(dialogOk == true)
            {
                var file = new TextFile(openFileDialog.FileName);
                FileChooser.SwapFile(file);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}