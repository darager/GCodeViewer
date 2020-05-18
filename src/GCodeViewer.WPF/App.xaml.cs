using System.Reflection;
using System.Windows;
using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.Navigation;
using Ninject;

namespace GCodeViewer.WPF
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var window = kernel.Get<MainWindow.MainWindow>();
            window.Show();

            var navService = kernel.Get<PageNavigationService>();
            navService.GoTo(Navigation.Navigation.StartingPage);
        }
    }
}
