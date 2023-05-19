using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace test_ScreenSaver
{
    public partial class Form1 : Form
    {      
        //tray 아이콘
        // http://www.toughman.pe.kr/2018/05/c-%ED%8A%B8%EB%A0%88%EC%9D%B4tray-%EC%95%84%EC%9D%B4%EC%BD%98-%ED%91%9C%EC%8B%9C-%ED%94%84%EB%A1%9C%EA%B7%B8%EB%9E%A8-%EB%A7%8C%EB%93%A4%EA%B8%B0/
        // 절전 및 모니터 끄기 방지
        // https://newstory-of-dev.tistory.com/entry/C-%EC%A0%88%EC%A0%84-%EB%B0%8F-%EB%AA%A8%EB%8B%88%ED%84%B0%EB%81%84%EA%B8%B0-%EB%B0%A9%EC%A7%80


        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        //SetThreadExecutionState function
        //https://docs.microsoft.com/ko-kr/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate?redirectedfrom=MSDN
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,          // 종료 모드를 활성화 합니다
            ES_CONTINUOUS = 0x80000000,                 // 설정 중인 상태가 ES_CONTINUOUS를 사용하고 다른 상태 플래그 중 하나를 제거할 때까지 계속 유호함을 시스템에 알립니다
            ES_DISPLAY_REQUIRED = 0x00000002,           // 디스플레이 유휴 타이머를 재 설정하여 강제로 디스플레이를 켭니다.
            ES_SYSTEM_REQUIRED = 0x00000001             // 시스템 유휴 타이머를 재설정하여 시스템이 작동 상태가 되도록 합니다.
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        Thread interrupt_thread;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            return;

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Opacity = 0;

   
            InterruptIdle();
        }


        private void InterruptIdle()
        {
            interrupt_thread = new Thread(new ThreadStart(Interrupt));
            interrupt_thread.Start();          
        }

        void Interrupt()
        {
            while (true)
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);

                Thread.Sleep(60000);
            }
        } 
 
     


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                interrupt_thread.Abort();
            }
            catch (Exception)
            {
            }        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("종료 하시겠습니까?");
            this.Close();
        }

        //Win32 DLL export 함수의 호출 규약
        //  https://www.sysnet.pe.kr/2/0/11132

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]      

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int SHOW = 5;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;


        IntPtr intPtr;
        private void button2_Click(object sender, EventArgs e)
        {

            intPtr = this.Handle;

            ShowWindow(intPtr, HIDE);
        }

 
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow(intPtr, SHOW);
        }
    }
}
