using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_CPU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Thread t1 = new Thread(threadFunc);
                t1.IsBackground = true;
                t1.Start();
            }

            Console.WriteLine("Press ENTER key to exit...");
            Console.ReadLine();
        }
        private static void threadFunc()
        {
            while (true)
            {
            }
        }


        void testMemory()
        {
            //초기 메모리
            long mem = GC.GetTotalMemory(false);
            Console.WriteLine("Initial Memoty : {0}", mem);
            
            run();

            //함수 호출 후 메모리
            mem = GC.GetTotalMemory(false);
            Console.WriteLine("Current Memory : {0}",mem);

            //메모리 Clean Up
            GC.Collect();
            Thread.Sleep(5000);

            //메모리 Clean up 후 
            mem = GC.GetTotalMemory(false);
            Console.WriteLine("After GC Memory : {0}", mem);
        }

        private void run()
        {
            var obj = new LargeDataClass();
            obj.set(1, 10);
        }
    }

    class LargeDataClass
    {
        private int[] data = new int[1000000];

        public void set(int index, int value)
        {
            data[index] = value
                    ;
        }
    }

}
