using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GCodeViewer.ViewModels.Commands
{
    public class SaveAsCommand : ICommand
    {
        private ITextViewModel textViewModel;
        private IFileChooser fileChooser;
        private ITextBuffer textBuffer;
        private IFileSaver fileSaver;

        public event EventHandler CanExecuteChanged;

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
            sad.Filter = GCodeFileFilter.Filter;
            sad.DefaultExt = GCodeFileFilter.StandardFileExtension;
            Nullable<bool> dialogResult = sad.ShowDialog();

            if (dialogResult == false) return;

            IFile newFile = new TextFile(sad.FileName);
            fileSaver.SaveToFile(newFile);
            fileChooser.SwapFile(newFile);
            textBuffer.LoadFileContent();
            textViewModel.GetCurrentContent();
        }
    }
}
