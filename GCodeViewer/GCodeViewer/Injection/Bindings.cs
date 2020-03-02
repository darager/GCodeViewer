using GCodeViewer.WPF.Abstractions.FileAccess;
using GCodeViewer.WPF.Abstractions.ViewModels;
using GCodeViewer.WPF.Commands;
using GCodeViewer.WPF.Components.FileAccess;
using GCodeViewer.WPF.Views.Pages;
using GCodeViewer.WPF.Views.ViewModels;
using Ninject.Modules;
using System.Windows.Controls;
using System.Windows.Input;

namespace GCodeViewer.WPF.Injection
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            // WPF Viewmodels
            Bind<IToolbarViewModel>().To<ToolbarBase>().InSingletonScope();
            Bind<ITextViewModel>().To<TextEditorBase>().InSingletonScope();
            Bind<IPageLocator>().To<PageSwapperBase>().InSingletonScope();

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
