using GCodeViewer.Abstractions.ViewModels;
using Ninject;
using System.Windows.Input;

namespace GCodeViewer.ViewModels
{
    public class ToolbarBase : IToolbarViewModel
    {
        public ICommand SaveAsCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }

        public ToolbarBase([Named("SaveAsFileCommand")]ICommand saveAsCommand, [Named("OpenFileCommand")]ICommand openFileCommand, [Named("SaveFileCommand")]ICommand saveFileCommand)
        {
            this.SaveAsCommand = saveAsCommand;
            this.OpenFileCommand = openFileCommand;
            this.SaveFileCommand = saveFileCommand;
        }
    }
}



