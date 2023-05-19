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

namespace test_Event
{
    //List Of Windows Messages
    //https://wiki.winehq.org/List_Of_Windows_Messages

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/a777c2d1-177a-4789-ab03-0b3e0110f744/how-to-get-events-at-the-title-bar?forum=csharpgeneral
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int WM_NCLBUTTONDBLCLK = 0x00A3;
        protected override void WndProc(ref Message m)
        {
            Console.WriteLine(m.Msg);

            //if (listBox1.Items.Count > 100)
            //    listBox1.Items.Clear();




            //listBox1.Items.Add(m.Msg);



            //listBox1.SelectedIndex = listBox1.Items.Count - 1;
            //listBox1.SelectedItem = listBox1.Items[listBox1.Items.Count - 1];

            //if (m_popup != null)
            //{
            //    if (m.Msg == WM_NCLBUTTONDOWN)
            //    {
            //        //select tittle area only If (){
            //        //this.Cursor = new Cursor(Cursor.Current.Handle);
            //        //MessageBox.Show("Click");
            //        //}
            //        return;
            //    }

            //    if (m.Msg == WM_NCLBUTTONDBLCLK)
            //    {
            //        return;
            //    }
            //}

            base.WndProc(ref m);
        }



        Thread runThread;

        private void button1_Click(object sender, EventArgs e)
        {
            runThread = null;
            try
            {
                runThread = new Thread(worker)
                {
                    IsBackground = true
                };
                runThread.SetApartmentState(ApartmentState.STA);

                runThread.Start();


            }
            catch (Exception ex)
            {
                //CNotice.noticeError("해석에 실패 하였습니다");
            }
        }

        FormProgress m_popup;

        private void worker()
        {
            m_popup = null;

            m_popup = new FormProgress(this)
            {
                StartPosition = FormStartPosition.Manual,
                TopLevel = true,
                ControlBox = false
            };

            m_popup.MinimumSize = m_popup.Size;
            m_popup.MaximumSize = m_popup.Size;


            Thread thread = null;
            thread = new Thread(new ParameterizedThreadStart(threadWorker))
            {
                IsBackground = true
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(m_popup);


            MessageBox.Show("");


            if (thread != null)
            {
                if (m_popup != null)
                {
                    m_popup.Invoke(new Action(() =>
                    {
                        m_popup.useClose = true;
                        m_popup.Close();
                        m_popup = null;
                    }
                    ));

                    thread.Interrupt();
                    thread.Join();
                }
            }

            if (runThread != null)
            {
                thread.Interrupt();
                thread.Join();
            }


        }

        void threadWorker(object popup)
        {
            try
            {

                FormProgress formProgress = (FormProgress)popup;

                formProgress.ShowDialog();


            }
            catch (System.Threading.ThreadAbortException) // ThreadAbortException을 처리해 주지 않으면 강제 종료 됨
            {
            }
            catch (System.Threading.ThreadInterruptedException) //스레드가 동작 중인 상태를 피해 예외를 던져 스레드를 중지
            {
                //
            }
            catch (Exception ex)
            {
                //CNotice.writeCatchMessage(ex);
            }
        }


        private void Form1_Activated_1(object sender, EventArgs e)
        {

            if (m_popup != null)
            {
                if (m_popup.InvokeRequired)
                {
                    m_popup.BeginInvoke(
                        new Action(() =>
                        {
                            m_popup.Activate();
                        }
                                 ));
                }
            }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //내부 컨트롤
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //내부 컨트롤
            //Form1_Click 먼저 발생
        }
    }
}
