using GCodeViewer.WPF.Abstractions.FileAccess;
using GCodeViewer.WPF.Abstractions.ViewModels;
using System;
using System.Windows.Input;

namespace GCodeViewer.WPF.Commands
{
    public class SaveCommand : ICommand
    {
        private ITextViewModel textViewModel;
        private IFileSaver fileSaver;

        public SaveCommand(ITextViewModel textViewModel, IFileSaver fileSaver)
        {
            this.textViewModel = textViewModel;
            this.fileSaver = fileSaver;
        }

        public bool CanExecute(object parameter)
        {
            var CanChangesBeSaved = !textViewModel.IsCurrentStateSaved() && textViewModel.IsFileLoaded();

            return CanChangesBeSaved;
        }
        public void Execute(object parameter)
        {
            fileSaver.SaveCurrentFile();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
