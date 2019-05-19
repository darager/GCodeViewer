using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectDIExample.Ninject
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeapon>().To<Sword>();
        }
    }
}
