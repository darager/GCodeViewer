﻿using GCodeViewer.Library.Renderables;
using GCodeViewer.WPF.ViewModels;
using Ninject.Modules;

namespace GCodeViewer.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IRenderService>().To<Viewer3DViewModel>().InSingletonScope();
        }
    }
}
