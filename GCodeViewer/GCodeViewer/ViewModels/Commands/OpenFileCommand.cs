using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Objects;
using GCodeViewer.ViewModels.Commands;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace GCodeViewer.Commands
{
    public class OpenFileCommand : ICommand
    {
        private ITextViewModel textViewModel { get; set; }
        private IFileChooser fileChooser { get; set; }
        private ITextBuffer textBuffer { get; set; }

        public OpenFileCommand(ITextViewModel textViewModel, IFileChooser fileChooser, ITextBuffer textBuffer)
        {
            this.textViewModel = textViewModel;
            this.fileChooser = fileChooser;
            this.textBuffer = textBuffer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.DefaultExt = GCodeFileFilter.StandardFileExtension;
            ofd.Filter = GCodeFileFilter.Filter;
            Nullable<bool> dialogOk = ofd.ShowDialog();

            if (dialogOk == false)
                return;

            fileChooser.SwapFile(new TextFile(ofd.FileName));
            textBuffer.LoadFileContent();
            textViewModel.LoadBufferContent();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}