using System;
using System.Threading;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Поточний час:");
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));

            Thread.Sleep(1000);
        }
    }
}