using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvokeLibrary //Platform Invocation Service 관리화 코드에서 비관리화 코드를 호출할 방법을 제공
{
    //WIN32 데이터형의 치환
    //  WIN32 비관리C데이타타입   C#
    //  HANDLE  int int
    //  BYTE    unsigned char byte
    //  SHORT   short short
    //  WORD    unsigned short ushort
    //  INT int int
    //  UINT    unsigned int uint 또는 int
    //  LONG    long int
    //  BOOL    long int
    //  DWORD   unsigned long uint
    //  ULONG   unsigned long uint
    //  CHAR    char char
    //  LPSTR   char* string 또는 StringBuilder
    //  LPCSTR  const char* string
    //  LPWSTR  wchar_t* string 또는 StringBuilder
    //  LPCWSTR const wchar_t*  string
    //  FLOAT   float float
    //  DOUBLE  double double


    public abstract class IWin32
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        public IntPtr getModuleHandle()
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                return GetModuleHandle(module.ModuleName);
        }

    }

    class CFindWindow : IWin32
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string a, string b);

        public object excute(string a, string b)
        {
            return FindWindow(a, b);
        }
    }
    class CMessageBox : IWin32
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        public object excute()
        {
            return MessageBox(new IntPtr(0), "Hello World!", "Hello Dialog", 0);
        }
    }



    public class CShowWindowP : IWin32
    {
        public enum emWindowType
        {
            HIDE = 0,
            MAXIMIZE = 3,
            SHOW = 5,
            MINIMIZE = 6,
            RESTORE = 9,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public object excute(emWindowType windowType)
        {
            return ShowWindow(getModuleHandle(), (int)windowType);
        }

    }

    public class CSetThreadExecutionState //화면 보호기
    {
        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,          // 종료 모드를 활성화 합니다
            ES_CONTINUOUS = 0x80000000,                 // 설정 중인 상태가 ES_CONTINUOUS를 사용하고 다른 상태 플래그 중 하나를 제거할 때까지 계속 유호함을 시스템에 알립니다
            ES_DISPLAY_REQUIRED = 0x00000002,           // 디스플레이 유휴 타이머를 재 설정하여 강제로 디스플레이를 켭니다.
            ES_SYSTEM_REQUIRED = 0x00000001             // 시스템 유휴 타이머를 재설정하여 시스템이 작동 상태가 되도록 합니다.
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        public object excute_PreventSleepMode()
        {
            return SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
        }
    }


    public class CMouseHook : IWin32
    {
        /// <summary>
        /// 훅 프로시저를 설치
        /// </summary>
        /// <param name="idHook">설치하고자 하는 훅의 타입을 지정</param>
        /// <param name="lpfn">훅 프로시저의 번지</param>
        /// <param name="hMod">훅 프로시저를 가진 인스턴트 핸들</param>
        /// <param name="dwThreadId">훅 프로시저가 감시할 스레드의 ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, MouseHookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// 훅 프로시저를 해지
        /// </summary>
        /// <param name="hhk">해지하고자 하는 훅 핸들</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// 다음 훅 프로시저에게 전해주는 함수
        /// </summary>
        /// <param name="hhk">해</param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private delegate IntPtr MouseHookProc(int code, IntPtr wParam, IntPtr lParam); //대리자 형식의 훅 프로시저 함수

        private MouseHookProc mouseHookProc;
        private IntPtr hookID = IntPtr.Zero;
        private const int WH_MOUSE_LL = 14;

        //private const uint LB_DOWN = 0x00000002; // 왼쪽 마우스 버튼 누름
        //private const uint LB_UP = 0x00000004; // 왼쪽 마우스 버튼 뗌

        //private const uint RB_DOWN = 0x00000008;  // 오른쪽 마우스 버튼 누름
        //private const uint RB_UP = 0x000000010; // 오른쪽 마우스 버튼 뗌

        //private const uint MB_DOWN = 0x00000020;  // 휠 버튼 누름
        //private const uint MB_UP = 0x000000040; // 휠 버튼 뗌
        //private const uint WHEEL = 0x00000800;  // 휠 스크롤

        //private const int WH_MOUSE_LL = 14;

        //const int TRUE = 1;
        //const int FALSE = 0;

        public void Begin()
        {

            mouseHookProc = HookProc;
            hookID = SetHook(mouseHookProc);
        }

        public void Stop()
        {
            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }


        private IntPtr SetHook(MouseHookProc proc)
        {
            return SetWindowsHookEx(WH_MOUSE_LL, proc, getModuleHandle(), 0);
        }

        private enum MouseEvent
        {
            MouseMove = 0x0200,

            LButtonDown = 0x0201,
            LButtonUp = 0x0202,
            //LButtonDoubleClick = 0x0203,

            RButtonDown = 0x0204,
            RButtonUp = 0x0205,
            //RButtonDoubleClick = 0x0206,

            MButtonDown = 0x0207,
            MButtonUp = 0x0208,

            MouseWheel = 0x020A,
        }

        public struct Point
        {
            public int x;
            public int y;
        }

        public struct MouseHookInfo
        {
            public Point pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;


            public override string ToString()
            {
                return time + " - " + pt.x + " : " + pt.y;
            }

        }

        public delegate void MouseEventHandler(MouseHookInfo mouseStruct);

        public event MouseEventHandler OnMouseMove;
        public event MouseEventHandler OnMouseWheel;

        public event MouseEventHandler OnLeftButtonDown;
        public event MouseEventHandler OnLeftButtonUp;

        public event MouseEventHandler OnRightButtonDown;
        public event MouseEventHandler OnRightButtonUp;

        public event MouseEventHandler OnMiddleButtonDown;
        public event MouseEventHandler OnMiddleButtonUp;

        private IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                MouseEvent mEvent = (MouseEvent)wParam;

                //PtrToStructure
                //  관리되지 않는 메모리 블록의 데이터를 관리되는 개체로 마샬링합니다.
                //  관리되지 않는 메모리 블록의 데이터를 지정된 형식의 새로 할당된 관리되는 개체로 마샬링합니다.
                //ptr
                //   관리되지 않는 메모리 블록에 대한 포인터입니다.
                //structureType
                //   만들 개체의 형식입니다.이 개체는 서식이 지정된 클래스나 구조체를 나타내야 합니다.

                switch (mEvent)
                {
                    case MouseEvent.LButtonDown:
                        OnLeftButtonDown?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    case MouseEvent.LButtonUp:
                        OnLeftButtonUp?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    //case MouseEvent.LButtonDoubleClick:
                    //    LeftDoubleClick?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                    //    break;

                    case MouseEvent.RButtonDown:
                        OnRightButtonDown?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    case MouseEvent.RButtonUp:
                        OnRightButtonUp?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    //case MouseEvent.RButtonDoubleClick:
                    //    RightDoubleClick?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                    //    break;

                    case MouseEvent.MouseMove:
                        OnMouseMove?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    case MouseEvent.MButtonDown:
                        OnMiddleButtonDown?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    case MouseEvent.MButtonUp:
                        OnMiddleButtonUp?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;

                    case MouseEvent.MouseWheel:
                        OnMouseWheel?.Invoke((MouseHookInfo)Marshal.PtrToStructure(lParam, typeof(MouseHookInfo)));
                        break;
                }
            }
            return CallNextHookEx(hookID, code, wParam, lParam);
        }


    }
}
