using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Settings;
using GCodeViewer.WPF.ViewModels;
using Ninject.Modules;

namespace GCodeViewer.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IRenderService>().To<Viewer3DViewModel>().InSingletonScope();

            Bind<SettingsService>().To<SettingsService>().InSingletonScope();
            Bind<PageNavigationService>().To<PageNavigationService>().InSingletonScope();
        }
    }
}
