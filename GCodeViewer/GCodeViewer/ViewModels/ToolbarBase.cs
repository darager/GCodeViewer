using GCodeViewer.Commands;
using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.ViewModels.Commands;
using System;
using System.Windows.Input;

namespace GCodeViewer.ViewModels
{
    public class ToolbarBase : IToolbarViewModel
    {
        public ITextViewModel TextViewModel { get; set; }
        public IFileSaver FileSaver { get; set; }
        public IFileChooser FileChooser { get; set; }
        public ITextBuffer TextBuffer { get; set; }
        public ICommand SaveFileCommand
        {
            get { return _saveFileCommand; }
            set { _saveFileCommand = value; }
        }
        public ICommand OpenFileCommand
        {
            get { return _openFileCommand; }
            set { _openFileCommand = value; }
        }
        public ICommand SaveAsCommand
        {
            get { return _saveAsCommand; }
            set { _saveAsCommand = value; }
        }

        private ICommand _saveFileCommand;
        private ICommand _openFileCommand;
        private ICommand _saveAsCommand;

        public ToolbarBase(ITextViewModel textViewModel, IFileSaver fileSaver, IFileChooser fileChooser, ITextBuffer textBuffer)
        {
            TextViewModel = textViewModel;
            FileSaver = fileSaver;
            FileChooser = fileChooser;
            TextBuffer = textBuffer;

            _saveFileCommand = new SaveCommand(textViewModel, fileSaver);
            _openFileCommand = new OpenFileCommand(textViewModel, fileChooser, textBuffer);
            _saveAsCommand = new SaveAsCommand(textViewModel, fileChooser, textBuffer, fileSaver);
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
        public void SaveAs()
        {
            throw new NotImplementedException();
        }
    }
}



