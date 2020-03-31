using GCodeViewer.WPF.Abstractions.FileAccess;
using GCodeViewer.WPF.Abstractions.ViewModels;
using GCodeViewer.WPF.Components.FileAccess;
using GCodeViewer.WPF.Resources;
using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace GCodeViewer.WPF.Commands
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
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = GCodeFileExtensionFilter.Filter;
            saveFileDialog.DefaultExt = GCodeFileExtensionFilter.StandardFileExtension;
            Nullable<bool> dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == false) return;

            IFile newFile = new TextFile(saveFileDialog.FileName);
            SaveFile(newFile);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void SaveFile(IFile file)
        {
            fileSaver.SaveToFile(file);
            fileChooser.SwapFile(file);
            textBuffer.LoadFileContent();
            textViewModel.GetCurrentContent();
        }
    }
}
