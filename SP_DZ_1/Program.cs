using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SP_DZ_1
{
    public class AboutMe
    {
        public string Title { get; set; }
        public string Info { get; set; }
    }
    public class MyClass
    {
        [DllImport("User32.dll", ExactSpelling = true)]
        public static extern int MessageBoxA(IntPtr intPtr, string text, string captions, uint type);
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title ="SP_DZ_1";
            AboutMe[] infoAll =
            {
                new AboutMe {Title="ФИО", Info="Юшинова Татьяна Александровна"},
                new AboutMe {Title="Дата рождения", Info="11.04.1984"},
                new AboutMe {Title="Место рождения", Info="СССР, г. Каменец-Подольский, Хмельницкая обл."},
                new AboutMe {Title="Профессия", Info="Графический дизайнер"}

            };
            infoAll.AsParallel().ForAll(info => MyClass.MessageBoxA(IntPtr.Zero, info.Info, info.Title,  0));
        }
    }
}
