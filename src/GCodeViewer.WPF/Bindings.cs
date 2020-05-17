using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Pages;
using GCodeViewer.WPF.Settings;
using GCodeViewer.WPF.TextEditor;
using GCodeViewer.WPF.ViewModels;
using Ninject.Modules;

namespace GCodeViewer.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow>().To<MainWindow>().InSingletonScope();

            Bind<PageNavigationService>().To<PageNavigationService>().InSingletonScope();

            Bind<StartingPage>().To<StartingPage>().InSingletonScope();
            Bind<StartingPageViewModel>().To<StartingPageViewModel>().InSingletonScope();

            Bind<SettingsService>().To<SettingsService>().InSingletonScope();
            Bind<SettingsPage>().To<SettingsPage>().InSingletonScope();
            Bind<SettingsPageViewModel>().To<SettingsPageViewModel>().InSingletonScope();

            Bind<ITextEditor>().To<TextEditorPageViewModel>().InSingletonScope();
            Bind<TextEditorPage>().To<TextEditorPage>().InSingletonScope();
            Bind<TextEditorPageViewModel>().To<TextEditorPageViewModel>().InSingletonScope();

            Bind<PagingViewModel>().To<PagingViewModel>().InSingletonScope();

            Bind<IRenderService>().To<Viewer3DViewModel>().InSingletonScope();
        }
    }
}
