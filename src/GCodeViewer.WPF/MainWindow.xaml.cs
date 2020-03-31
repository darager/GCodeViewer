using GCodeViewer.WPF.Abstractions.ViewModels;
using Ninject;
using System.Reflection;
using System.Windows;

namespace GCodeViewer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Init ioc container
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            // get pageswapper from kernel
            var pageSwapper = kernel.Get<IPageLocator>();
            pageSwapper.SetStartupPage();

            // set datacontexts
            toolBar.DataContext = kernel.Get<IToolbarViewModel>();
            mainFrame.DataContext = pageSwapper;
        }
    }
}
