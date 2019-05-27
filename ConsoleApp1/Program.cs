using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var kahoot = new Dog();
            kahoot.Woof();
        }
    }

    class Dog
    {
        public Dog()
        {
        }

        public void Woof()
        {
            Console.WriteLine("Woof");
        }
    }
}

