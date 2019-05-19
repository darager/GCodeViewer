using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectDIExample
{
    public class Fighter
    {
        private IWeapon weapon;
        public Fighter(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public void Fight()
        {
            Console.WriteLine(weapon.UseWeapon() + "stab his oponent cold blooded.");
        }
    }
}
