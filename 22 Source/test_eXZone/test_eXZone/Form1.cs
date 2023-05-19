using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_eXZone
{
    public partial class Form1 : Form
    {
        //.NET의 타이머는 크게
        //멀티쓰레딩을 지원하는 System.Threading.Timer클래스, System.Timers.Timer
        //클래스와 싱글쓰레드만을 지원하는 System.Windows.Forms.Timer클래스, System.Windows.Threading.DispatcherTimer 클래스가 있다



        //버튼 이벤트가 바로 처리 안 됨
        //싱글쓰레드만을 지원하는 System.Windows.Forms.Timer클래스
        System.Windows.Forms.Timer m_timerAEDT = new Timer();
        const int TIMER_INTERVAL_FOR_MONITORING_AEDT = 1000;

        System.Windows.Forms.Timer m_timerAnalysis = new Timer();
        const int TIMER_INTERVAL_FOR_MONITORING_ANALYSIS = 1000;


        System.Timers.Timer m_timerAEDT_Threading = new System.Timers.Timer();
        System.Timers.Timer m_timerAnalysis_Threading = new System.Timers.Timer();

        enum timerType
        {
            none,
            Forms, Timers
        }

        timerType m_timerType;

        public Form1()
        {
            InitializeComponent();

            //Form timer
            //버튼 이벤트가 바로 처리 안 됨
            //싱글쓰레드만을 지원하는 System.Windows.Forms.Timer클래스
            m_timerType = timerType.Forms;

            m_timerAEDT.Interval = TIMER_INTERVAL_FOR_MONITORING_AEDT;
            m_timerAEDT.Tick += M_timerAEDT_Tick;
            m_timerAnalysis.Interval = TIMER_INTERVAL_FOR_MONITORING_ANALYSIS;
            m_timerAnalysis.Tick += M_timerAnalysis_Tick;
            //

            //            System.Timers.Timer
            // 또한 만약 이벤트 핸들러가 다음 Interval 보다 오래 실행된다면, 다른 작업쓰레드가 핸들러를 실행하게 되기 때문에, Thread Safe하게 작성해야 한다
            //m_timerType = timerType.Timers;

            //m_timerAEDT_Threading.Interval = TIMER_INTERVAL_FOR_MONITORING_AEDT;
            //m_timerAEDT_Threading.Elapsed += M_timerAEDT_Threading_Elapsed;
            //m_timerAnalysis_Threading.Interval = TIMER_INTERVAL_FOR_MONITORING_ANALYSIS;
            //m_timerAnalysis_Threading.Elapsed += M_timerAnalysis_Threading_Elapsed;


            label1.Text = m_timerType.ToString();

            toolStripProgressBar1.Maximum = 100;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Value = 0;
        }

        private void M_timerAnalysis_Threading_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            doCalc("timerAnalysis");

            toolStripProgressBar1.Value = toolStripProgressBar1.Value +1;
        }

        private void M_timerAEDT_Threading_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            doCalc("timerAEDT");
        }

        void doCalc(string name)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Start : {0}", name);
            stopWatch.Start();
            //Milliseconds
            //10000   5
            //20000   18
            //1000000 403
            //=================
            //second
            //5000000 1
            //10000000 3 느림
            //15000000 5 느림느림 
            for (int i = 0; i < 20000; i++)
            {
                //Console.WriteLine(name + " {0}", i);
                string v = string.Format(name + " {0}", i);

            }
            stopWatch.Stop();
            //Console.WriteLine("소요시간 : " + name + " {0}", stopWatch.ElapsedMilliseconds/1000);
            Console.WriteLine("소요시간 : " + name + " {0}", stopWatch.ElapsedMilliseconds );

        }


        private void M_timerAnalysis_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < 1000000; i++)
            //{
            //    Console.WriteLine("timerAnalysis {0}", i);
            //}
            doCalc("timerAnalysis");

            if (toolStripProgressBar1.Value == toolStripProgressBar1.Maximum)
            {
                toolStripProgressBar1.Value = 0;
            }
            toolStripProgressBar1.Value = toolStripProgressBar1.Value + 1;
        }

        private void M_timerAEDT_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < 1000000; i++)
            //{
            //    Console.WriteLine("timerAEDT {0}", i);
            //}

            doCalc("timerAEDT");
        }

        PointF mousepointDown;
        PointF mousepointUp;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mousepointDown = new PointF(e.X, e.Y);
        }



        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mousepointUp = new PointF(e.X, e.Y);

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            for (int i = 0; i < 10000; i++)
            {
                mousepointUp.Y = mousepointUp.Y + (0.01f * i);
                mousepointDown.Y = mousepointDown.Y + (0.01f * i);
                try
                {
                    e.Graphics.DrawLine(Pens.Red, mousepointDown, mousepointUp);
                }
                catch (Exception)
                {
                }
            }

            label2.Text = (int.Parse(label2.Text) +1).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            switch (m_timerType)
            {
                case timerType.none:
                    break;
                case timerType.Forms:
                    if (m_timerAnalysis.Enabled)
                    {
                        m_timerAnalysis.Stop();
                        toolStripProgressBar1.Value = 0;
                    }
                    else
                    {
                        m_timerAnalysis.Start();
                    }
                    break;
                case timerType.Timers:
                    if (m_timerAnalysis_Threading.Enabled)
                    {
                        m_timerAnalysis_Threading.Stop();
                        toolStripProgressBar1.Value = 0;
                    }
                    else
                    {
                        m_timerAnalysis_Threading.Start();
                    }
                    break;
                default:
                    break;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (m_timerType)
            {
                case timerType.none:
                    break;
                case timerType.Forms:
                    if (m_timerAEDT.Enabled)
                    {
                        m_timerAEDT.Stop();
                    }
                    else
                    {
                        m_timerAEDT.Start();
                    }
                    break;
                case timerType.Timers:
                    if (m_timerAEDT_Threading.Enabled)
                    {
                        m_timerAEDT_Threading.Stop();
                    }
                    else
                    {
                        m_timerAEDT_Threading.Start();
                    }
                    break;
                default:
                    break;
            }


        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {           

            //for (int i = 0; i < 500000; i++)
            //{
                Console.WriteLine("test Move : " +DateTime.Now.ToString());
            //}
        }
    }
}
