using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCodeViewer.Interfaces;
using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.FileAccess.FileChooser;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Objects;
using GCodeViewer.ViewModels;
using Ninject.Modules;

namespace GCodeViewer.Dependency_Injection
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            // TODO: Add Bindings for all for all of the objects

            // WPF Viewmodels
            //Bind<ITextViewModel>().To<GCodeViewModel>().InSingletonScope();
            //Bind<IRenderWindowViewModel>().To<RenderWindowViewModel>().InSingletonScope();
            //Bind<IToolbarViewModel>().To<ToolbarViewModel>().InSingletonScope();

            // File Access objects
            Bind<IFileChooser>().To<FileChooser>().InSingletonScope();
            Bind<IFileSaver>().To<FileSaver>().InSingletonScope();
        }
    }
}
