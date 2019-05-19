using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectDIExample
{
    public class Gun : IWeapon
    {
        public string UseWeapon()
        {
            return "The gun is used to ";
        }
    }
}
