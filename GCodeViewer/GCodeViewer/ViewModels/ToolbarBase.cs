using GCodeViewer.Interfaces.ViewModels;
using Ninject;
using System.Windows.Input;

namespace GCodeViewer.ViewModels
{
    public class ToolbarBase : IToolbarViewModel
    {
        [Inject, Named("SaveAsFileCommand")]
        public ICommand SaveAsCommand { get; set; }
        [Inject, Named("OpenFileCommand")]
        public ICommand OpenFileCommand { get; set; }
        [Inject, Named("SaveFileCommand")]
        public ICommand SaveFileCommand { get; set; }
    }
}



