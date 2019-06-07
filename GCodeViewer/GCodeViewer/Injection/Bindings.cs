using GCodeViewer.Abstractions;
using GCodeViewer.Abstractions.FileAccess;
using GCodeViewer.Abstractions.ViewModels;
using GCodeViewer.Commands;
using GCodeViewer.Components.FileAccess;
using GCodeViewer.ViewModels;
using GCodeViewer.Views;
using Ninject.Modules;
using System.Windows.Controls;
using System.Windows.Input;

namespace GCodeViewer.Injection
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            // WPF Viewmodels
            Bind<IToolbarViewModel>().To<ToolbarBase>().InSingletonScope();
            Bind<ITextViewModel>().To<TextEditorBase>().InSingletonScope();
            Bind<IPageLocator>().To<FramePageSwapperBase>().InSingletonScope();

            // Pages and Pagelocator
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
