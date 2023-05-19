using PInvokeLibrary;
using System;
using System.Windows.Forms;

namespace test_Win32Library
{
    class Program
    {
        static CMouseHook m_mouseHook;

        [STAThread]
        static void Main(string[] args)
        {

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);


            m_mouseHook = new CMouseHook();

            m_mouseHook.OnMouseMove += M_mouseHook_OnMouseMove;

            m_mouseHook.Begin();

            Application.Run();

            m_mouseHook.Stop();
        }

        private static void M_mouseHook_OnMouseMove(CMouseHook.MouseHookInfo mouseStruct)
        {
            Console.WriteLine(mouseStruct.ToString());
        }
    }
}
