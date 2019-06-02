using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GCodeViewer.Commands;
using GCodeViewer.Interfaces;
using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Objects;
using GCodeViewer.ViewModels;
using GCodeViewer.ViewModels.Commands;
using GCodeViewer.Views;
using Ninject.Modules;

namespace GCodeViewer.Dependency_Injection
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            // TODO: Add Bindings for all for all of the objects

            // WPF Viewmodels
            Bind<IToolbarViewModel>().To<ToolbarBase>().InSingletonScope();
            Bind<ITextViewModel>().To<TextEditorBase>().InSingletonScope();

            // Page Factory for navigation
            Bind<Page>().To<OpenFilePage>().InSingletonScope().Named("OpenFilePage");
            Bind<Page>().To<LiveEditorPage>().InSingletonScope().Named("LiveEditorPage");

            // Commands
            Bind<ICommand>().To<OpenFileCommand>().Named("OpenFileCommand");
            Bind<ICommand>().To<SaveAsCommand>().Named("SaveAsFileCommand");
            Bind<ICommand>().To<SaveCommand>().Named("SaveFileCommand");

            // File Access objects
            Bind<IFileChooser>().To<FileChooser>().InSingletonScope();
            Bind<IFileSaver>().To<FileSaver>().InSingletonScope();
            Bind<ITextBuffer>().To<TextBuffer>().InSingletonScope();
        }
    }
}
