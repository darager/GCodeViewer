using GCodeViewer.Abstractions;
using GCodeViewer.Abstractions.ViewModels;
using Ninject;
using System.Reflection;
using System.Windows;

namespace GCodeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitIocContainer();
        }

        void InitIocContainer()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            toolBar.DataContext = kernel.Get<IToolbarViewModel>();

            var pageSwapper = kernel.Get<IPageLocator>();
            pageSwapper.SetStartupPage();
            mainFrame.DataContext = pageSwapper;
        }
    }
}
