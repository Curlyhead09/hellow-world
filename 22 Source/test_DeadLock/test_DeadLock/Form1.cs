using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_DeadLock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //https://www.sysnet.pe.kr/2/0/1738
        // CAS를 이용한 Lock 래퍼 클래스

        //test 1
        // dead-lock  연출
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(this.t1);
            Thread t2 = new Thread(this.t2);

            t1.Name = "lockAB";
            t2.Name = "lockBA";

            t1.Start();
            t2.Start();

            t1.Join(); // t1 스레드는 절대로 종료하지 않으므로 Join문은 반환하지 않음.
            t2.Join();
        }

        CASLock lockA = new CASLock();
        CASLock lockB = new CASLock();

        //test2
        //Thread.Abort 메서드로 특정 스레드를 강제 종료하는 것으로 우리가 만든 CASLock 클래스의 안정성 테스트를 해보겠습니다.
        private void button2_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(this.t1);
            Thread t2 = new Thread(this.t2);

            t1.Name = "lockAB";
            t2.Name = "lockBA";

            t1.Start();
            t2.Start();

            int retryCount = 5;
            while (retryCount-- > 0)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            t1.Abort(); // Thread1 강제 종료

            t1.Join();
            t2.Join();
        }

        //test3
        // 그런데, Native Win32 API인 TerminateThread를 호출하면

        [DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        static IntPtr _threadAHandle = IntPtr.Zero;

        private void button3_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(this.t3);
            Thread t2 = new Thread(this.t4);

            t1.Name = "lockAB";
            t2.Name = "lockBA";

            t1.Start();
            t2.Start();

            int retryCount = 5;
            while (retryCount-- > 0)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Terminating...: " + Process.GetCurrentProcess().Threads.Count);
            TerminateThread(_threadAHandle, 100);
            Console.WriteLine("Terminated.: " + Process.GetCurrentProcess().Threads.Count);

            t1.Join();
            t2.Join();

        }

        // Thread 1 
        void t3()
        {
            _threadAHandle = OpenThread(ThreadAccess.TERMINATE, false, AppDomain.GetCurrentThreadId());
            using (lockA.Lock())
            {
                Thread.Sleep(2000);
                using (lockB.Lock())
                {
                    Console.WriteLine("lockA -> lockB");
                }
            }
        }

        // Thread 2 
        void t4()
        {
            using (lockB.Lock())
            {
                Thread.Sleep(2000);
                using (lockA.Lock())
                {
                    Console.WriteLine("lockB -> lockA");
                }
            }
        }




        void t1()
        {
            using (lockA.Lock())
            {
                Thread.Sleep(2000);
                using (lockB.Lock()) // Thread2의 t2메서드에서 이미 lock을 소유하고 있으므로 block
                {
                    Console.WriteLine("lockA -> lockB");
                }
            }
        }

        // Thread 2 
        void t2()
        {
            using (lockB.Lock())
            {
                Thread.Sleep(2000);
                using (lockA.Lock()) // Thread1의 t1메서드에서 이미 lock을 소유하고 있으므로 block
                {
                    Console.WriteLine("lockB -> lockA");
                }
            }
        }


    }
}
