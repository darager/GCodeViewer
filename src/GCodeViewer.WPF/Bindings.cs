using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.MainWindow;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Settings;
using GCodeViewer.WPF.Starting;
using GCodeViewer.WPF.StlPositioning;
using GCodeViewer.WPF.TextEditor;
using Ninject.Modules;

namespace GCodeViewer.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow.MainWindow>().ToSelf().InSingletonScope();

            // Rendering
            // when binding a class to an interface and to itself the interface has to be bound to a provider to ensure that the class is not created twice
            Bind<IRenderService>().ToProvider<Viewer3DViewModel>();
            Bind<Viewer3DViewModel>().ToSelf().InSingletonScope();
            Bind<IViewerScene>().To<BasicScene>().InSingletonScope();

            // Starting Page
            Bind<StartingPage>().ToSelf().InSingletonScope();
            Bind<StartingPageViewModel>().ToSelf().InSingletonScope();

            // STL positioning Page
            Bind<STLPositioningPage>().ToSelf().InSingletonScope();
            Bind<STLPositioningPageViewModel>().ToSelf().InSingletonScope();

            // Settings
            Bind<SettingsService>().ToSelf().InSingletonScope();
            Bind<SettingsPage>().ToSelf().InSingletonScope();
            Bind<SettingsPageViewModel>().ToSelf().InSingletonScope();

            // Text Editor
            Bind<TextEditorPage>().ToSelf().InSingletonScope();
            Bind<ITextEditor>().ToProvider<TextEditorPageViewModel>();
            Bind<TextEditorPageViewModel>().ToSelf().InSingletonScope();

            // Page Navigation
            Bind<PagingViewModel>().ToSelf().InSingletonScope();
            Bind<PageNavigationService>().ToSelf().InSingletonScope();
        }
    }
}
