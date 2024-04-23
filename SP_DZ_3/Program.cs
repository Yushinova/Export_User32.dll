using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SP_DZ_3
{
    public class MyBeeper
    {
        [DllImport("Kernel32.dll")]
        public static extern void Beep(int frequency, int duration);
        //public static extern bool MessageBeep(int frequency, int duration);
    }
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Дя остановки нажмите любую клавишу!");
            Random _random = new Random();
            do
            {
               
                MyBeeper.Beep(_random.Next(100, 800), _random.Next(100, 800));
                Thread.Sleep(_random.Next(100, 800));

            } while(!Console.KeyAvailable);
        }
    }
}
