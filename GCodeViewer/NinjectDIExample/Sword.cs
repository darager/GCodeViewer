using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectDIExample
{
    public class Sword : IWeapon
    {
        public Sword()
        {
        }

        public string UseWeapon()
        {
            return "The Sword is used to ";
        }
    }
}
