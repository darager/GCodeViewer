using GCodeViewer.Abstractions;
using GCodeViewer.Abstractions.FileAccess;
using GCodeViewer.Abstractions.ViewModels;
using GCodeViewer.Components.FileAccess;
using GCodeViewer.Resources;
using Microsoft.Win32;
using Ninject;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GCodeViewer.Commands
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
            return true;
        }
        public void Execute(object parameter)
        {
            if (!textViewModel.IsCurrentStateSaved() && textViewModel.IsFileLoaded())
            {
                var result =
                    MessageBox.Show(
                      "The current changes have not been saved! \nDo you wish to proceed without saving"
                    , "Unsaved Changes!"
                    , MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.DefaultExt = GCodeFileFilter.StandardFileExtension;
            ofd.Filter = GCodeFileFilter.Filter;
            Nullable<bool> dialogOk = ofd.ShowDialog();

            if (dialogOk == false)
            {
                return;
            }

            fileChooser.SwapFile(new TextFile(ofd.FileName));
            textBuffer.LoadFileContent();
            textViewModel.LoadFileContent();
            pageLocator.SwapToLiveEditorPage();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}