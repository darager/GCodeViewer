using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NinjectDIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            // in order to bind the dependencies this method calls the Bindings.Load method
            kernel.Load(Assembly.GetExecutingAssembly());

            // the kernel automatically looks up what dependencies are required for the Fighter
            // class and passes the according to the dependencies that have been bound in Bindings.Load()
            var fighter = kernel.Get<Fighter>();
            fighter.Fight();

            Console.ReadKey();
        }
    }
}
