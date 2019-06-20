using GCodeViewer.Abstractions.FileAccess;
using GCodeViewer.Abstractions.ViewModels;
using GCodeViewer.Components.FileAccess;
using GCodeViewer.Resources;
using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace GCodeViewer.Commands
{
    public class SaveAsCommand : ICommand
    {
        private ITextViewModel textViewModel;
        private IFileChooser fileChooser;
        private ITextBuffer textBuffer;
        private IFileSaver fileSaver;

        public SaveAsCommand(ITextViewModel textViewModel, IFileChooser fileChooser, ITextBuffer textBuffer, IFileSaver fileSaver)
        {
            this.textViewModel = textViewModel;
            this.fileChooser = fileChooser;
            this.textBuffer = textBuffer;
            this.fileSaver = fileSaver;
        }

        public bool CanExecute(object parameter)
        {
            return textViewModel.IsFileLoaded();
        }
        public void Execute(object parameter)
        {
            var sad = new SaveFileDialog();
            sad.Filter = GCodeFileExtensionFilter.Filter;
            sad.DefaultExt = GCodeFileExtensionFilter.StandardFileExtension;
            Nullable<bool> dialogResult = sad.ShowDialog();

            if (dialogResult == false)
            {
                return;
            }

            IFile newFile = new TextFile(sad.FileName);
            fileSaver.SaveToFile(newFile);
            fileChooser.SwapFile(newFile);
            textBuffer.LoadFileContent();
            textViewModel.GetCurrentContent();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
