using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp1
{
    public class DataParser
    {
        //System.Timers.Timer timer;
        //List<int> items;

        //public DataParser()
        //{
        //    items = new List<int>();
        //    timer = new System.Timers.Timer();
        //    timer.Interval = 100;
        //    timer.AutoReset = false;
        //    timer.Elapsed += ParseData;
        //}

        //public void SendData(int data)
        //{
        //    if (!timer.Enabled) StartNewTimer();

        //    if (items.Count >= 2) return;

        //    items.Add(data);
        //}
        //private void StartNewTimer()
        //{
        //    timer.Start();
        //    items = new List<int>();
        //}

        //private void ParseData(object sender, ElapsedEventArgs e)
        //{
        //    var builder = new StringBuilder();
        //    foreach (var item in items)
        //        builder.Append(item);

        //    int number = Convert.ToInt16(builder.ToString());
        //    Console.WriteLine("The number is: " + number);
        //}
    }
}
