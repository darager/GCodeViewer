using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCodeViewer.Interfaces;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.ViewModels;
using Ninject.Modules;

namespace GCodeViewer.Dependency_Injection
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITextViewModel>().To<GCodeViewModel>().InSingletonScope();
            Bind<IRenderWindowViewModel>().To<RenderWindowViewModel>().InSingletonScope();
            Bind<IToolbarViewModel>().To<ToolbarViewModel>().InSingletonScope();
        }
    }
}
