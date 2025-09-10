using System;
using System.ComponentModel.Design;
using System.Timers;

class Program
{
    static Timer timer;

    static void Main()
    {
        bool a = true;
        int MiliSecond = 0;
        int Second = 0;

        while (a)
        {
            Console.Write($"{Second}.{MiliSecond}");
            Console.Clear();
            MiliSecond++;
            if (MiliSecond == 1000)
            {
                Second++;
                MiliSecond = 0;
            }


        }
    }

    
}
