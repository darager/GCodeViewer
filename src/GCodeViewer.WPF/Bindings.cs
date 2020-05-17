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
            Bind<IRenderService>().To<Viewer3DViewModel>().InSingletonScope();

            Bind<ITextEditor>().To<TextEditorViewModel>().InSingletonScope();
            Bind<TextEditorViewModel>().To<TextEditorViewModel>().InSingletonScope();

            Bind<SettingsService>().To<SettingsService>().InSingletonScope();
            Bind<PageNavigationService>().To<PageNavigationService>().InSingletonScope();

            Bind<SettingsPage>().To<SettingsPage>().InSingletonScope();
            Bind<StartingPage>().To<StartingPage>().InSingletonScope();
            Bind<TextEditorPage>().To<TextEditorPage>().InSingletonScope();
        }
    }
}
