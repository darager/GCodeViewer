using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new DataParser();

            parser.SendData(10);
            parser.SendData(20);


            string message = "lkasjdföklajsdlkfjljk";
            Console.WriteLine(message);


            Console.ReadKey();
        }
    }
}
