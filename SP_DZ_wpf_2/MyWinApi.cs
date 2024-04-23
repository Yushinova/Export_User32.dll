using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SP_DZ_wpf_2
{
    public class MyWinApi
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        //[DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg,IntPtr wParam, string lParam);

        public static uint WM_SETTEXT = 0X000C;
        public static uint WM_CLOSE = 0x0010;

    }
}
