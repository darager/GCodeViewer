using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Starting;
using GCodeViewer.WPF.Settings;
using GCodeViewer.WPF.TextEditor;
using GCodeViewer.WPF.MainWindow;
using Ninject.Modules;

namespace GCodeViewer.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow.MainWindow>().ToSelf().InSingletonScope();

            // Rendering
            Bind<IRenderService>().To<Viewer3DViewModel>().InSingletonScope();
            Bind<Viewer3DViewModel>().ToSelf().InSingletonScope();
            Bind<PrinterScene>().ToSelf().InSingletonScope();

            // Starting Page
            Bind<StartingPage>().ToSelf().InSingletonScope();
            Bind<StartingPageViewModel>().ToSelf().InSingletonScope();

            // Settings
            Bind<SettingsService>().ToSelf().InSingletonScope();
            Bind<SettingsPage>().ToSelf().InSingletonScope();
            Bind<SettingsPageViewModel>().ToSelf().InSingletonScope();

            // Text Editor
            Bind<TextEditorPage>().ToSelf().InSingletonScope();
            Bind<TextEditorPageViewModel>().ToSelf().InSingletonScope();

            // Page Navigation
            Bind<PagingViewModel>().ToSelf().InSingletonScope();
            Bind<PageNavigationService>().ToSelf().InSingletonScope();
        }
    }
}
