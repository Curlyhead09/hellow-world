using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static test_GlobalHook.GlobalMouseHook;

namespace test_GlobalHook
{
    //Mouse Hook

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GlobalMouseHook m_MouseHoook;

        Thread thread;
        private void Form1_Load(object sender, EventArgs e)
        {
            m_MouseHoook = new GlobalMouseHook();

            m_MouseHoook.OnMouseMove += M_MouseHoook_OnMouseMove;
            m_sw.Start();


            thread = new Thread(new ThreadStart(Worker));
            thread.Start();

            m_MouseHoook.Begin();
          
        }

        private void Worker()
        {
            while (m_sw.IsRunning)
            {
                if (m_sw.ElapsedMilliseconds > 1000 * 58)
                {
                    //MousePoint point = m_MouseHoook.GetCursorPosition();
                    //m_MouseHoook.ForceMoveCursorLocal(point.x, point.y);
                    //m_MouseHoook.ForceWheelDown(120);
                    m_MouseHoook.ForceLeftClick();
                    listBox1.Invoke(new Action(() => listBox1.Items.Add("60초 :" + m_sw.ElapsedMilliseconds)));
                    Thread.Sleep(1000);
                    m_sw.Restart();
                }
            }
        }

        private void M_MouseHoook_OnMouseMove(GlobalMouseHook.MouseHookInfo mouseStruct)
        {
            addItemToListBox(mouseStruct);
        }

        Stopwatch m_sw = new Stopwatch();

        private void addItemToListBox(GlobalMouseHook.MouseHookInfo mouseStruct)
        {
            if (listBox1.Items.Count > 1000)
            {
                listBox1.Items.Clear();

                //    MousePoint point = m_MouseHoook.GetCursorPosition();

                //    m_MouseHoook.ForceSetCursor(point.x, point.y);
                //    m_MouseHoook.ForceWheelUp(5);
            }

            if (m_sw.IsRunning)
                m_sw.Restart();

            listBox1.Items.Add(mouseStruct.pt.x + " :" + mouseStruct.pt.x);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_MouseHoook.Stop();
            m_sw.Stop();
            thread.Abort();
        }
    }
}
