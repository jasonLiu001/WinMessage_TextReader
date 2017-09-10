using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WinMessage_TextReader
{
    class Program
    {

        static void Main(string[] args)
        {
            var captionTitle = GetWindowCaptionTitle();
            Console.WriteLine(captionTitle);
            Console.ReadLine();
        }



        /// <summary>
        /// 获取窗口标题文字
        /// </summary>
        /// <returns></returns>
        private static string GetWindowCaptionTitle()
        {
            IntPtr maindHwnd = Win32.FindWindow("WTWindow", null); //获取窗口句柄
            if (maindHwnd == IntPtr.Zero)
            {
                Console.WriteLine("未找到对应的窗口");
                return string.Empty;
            }

            //第一个子窗口
            IntPtr firstChildWin = Win32.FindWindowEx(maindHwnd, IntPtr.Zero, "Edit", null);  //第一个子窗口
            //计划窗口 maindHwnd为主窗口 表示在这里面查找 如果替换成子窗口，说明在子窗口中查找
            IntPtr planWin = Win32.FindWindowEx(maindHwnd, firstChildWin, "Edit", null);  //计划窗口
            int maxLength = 1000000;

            IntPtr buffer = Marshal.AllocHGlobal((maxLength + 1) * 2);
            Win32.SendMessageW2(planWin, Constant.WM_GETTEXT, (uint)maxLength, buffer);
            string windowCaptionTitle = Marshal.PtrToStringUni(buffer);
            return windowCaptionTitle;
        }
    }
}
