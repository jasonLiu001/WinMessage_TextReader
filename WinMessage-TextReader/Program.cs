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
            var captionTitle = string.Empty;
            IntPtr maindHwnd = Win32.FindWindow("WTWindow", null); //获取窗口句柄
            if (maindHwnd == IntPtr.Zero)
            {
                var msg = "未找到对应的窗口";
                Console.WriteLine(msg);
                return captionTitle;
            }

            //第一个子窗口
            IntPtr firstChildWin = Win32.FindWindowEx(maindHwnd, IntPtr.Zero, "Edit", null);  //第一个子窗口
            //计划窗口 maindHwnd为主窗口 表示在这里面查找 如果替换成子窗口，说明在子窗口中查找
            IntPtr planWin = Win32.FindWindowEx(maindHwnd, firstChildWin, "Edit", null);  //计划窗口
            //获取文本长度
            var txtLength = Win32.SendMessageW2(planWin, Constant.WM_GETTEXTLENGTH, 0, 0);

            Byte[] byt = new Byte[txtLength * 2];
            Win32.SendMessageW2(planWin, Constant.WM_GETTEXT, txtLength * 2 + 1, byt);
            captionTitle = Encoding.Unicode.GetString(byt);
            return captionTitle;
        }
    }
}
