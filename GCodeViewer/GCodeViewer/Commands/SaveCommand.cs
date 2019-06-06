using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;
using System;
using System.Windows.Input;

namespace GCodeViewer.ViewModels.Commands
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
            return !textViewModel.IsCurrentStateSaved() && textViewModel.IsFileLoaded();
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
