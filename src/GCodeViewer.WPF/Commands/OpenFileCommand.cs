using GCodeViewer.WPF.Abstractions.FileAccess;
using GCodeViewer.WPF.Abstractions.ViewModels;
using GCodeViewer.WPF.Components.FileAccess;
using GCodeViewer.WPF.Resources;
using GCodeViewer.WPF.Views.ViewModels;
using Microsoft.Win32;
using Ninject;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GCodeViewer.WPF.Commands
{
    public class OpenFileCommand : ICommand
    {
        public ITextViewModel textViewModel { get; set; }
        public IFileChooser fileChooser { get; set; }
        public ITextBuffer textBuffer { get; set; }
        [Inject]
        public IPageLocator pageLocator { get; set; }

        public OpenFileCommand(ITextViewModel textViewModel, IFileChooser fileChooser, ITextBuffer textBuffer)
        {
            this.textViewModel = textViewModel;
            this.fileChooser = fileChooser;
            this.textBuffer = textBuffer;
        }

        public bool CanExecute(object parameter)
        {
            // you can always open a file
            return true;
        }
        public void Execute(object parameter)
        {
            if (ThereAreUnsavedChanges())
            {
                bool UserWantsToDiscardChanges = AskUserWhetherToDiscardChanges();

                if (UserWantsToDiscardChanges)
                    return;
            }

            var ofd = new OpenFileDialog();
            Nullable<bool> dialogResult = GetDialogResult(ofd);

            if (dialogResult == false) return;

            OpenTextFile(ofd);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private bool ThereAreUnsavedChanges()
        {
            bool result = (!textViewModel.IsCurrentStateSaved() && textViewModel.IsFileLoaded());

           return result;
        }
        private bool AskUserWhetherToDiscardChanges()
        {
            var result =
                MessageBox.Show(
                  "The current changes have not been saved! \nDo you wish to proceed without saving"
                , "Unsaved Changes!"
                , MessageBoxButton.YesNo);

            return (result == MessageBoxResult.No);
        }
        private Nullable<bool> GetDialogResult(OpenFileDialog ofd)
        {
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.DefaultExt = GCodeFileExtensionFilter.StandardFileExtension;
            ofd.Filter = GCodeFileExtensionFilter.Filter;
            Nullable<bool> dialogResult = ofd.ShowDialog();

            return dialogResult;
        }
        private void OpenTextFile(OpenFileDialog ofd)
        {
            fileChooser.SwapFile(new TextFile(ofd.FileName));
            textBuffer.LoadFileContent();
            textViewModel.LoadFileContent();
            pageLocator.SwapPage(FramePage.LiveEditor);
        }
    }
}