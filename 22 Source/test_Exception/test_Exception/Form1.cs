using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Exception
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormTest form = new FormTest();
            FormTest usingForm;
            using (usingForm = new FormTest())
            {

            }
            //usingForm.IsDisposed : True
            Debug.WriteLine($"usingForm.IsDisposed : {usingForm.IsDisposed}");
            //form.IsDisposed : False
            Debug.WriteLine($"form.IsDisposed : {form.IsDisposed}");

            form.Close();
            //삭제된 개체에 액세스할 수 없습니다.
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Timer t = new System.Threading.Timer(TimerNotification, null,
                         100, Timeout.Infinite);
            Thread.Sleep(2000);
            t.Dispose();

            t.Change(200, 1000);
            Thread.Sleep(3000);
        }

        private static void TimerNotification(Object obj)
        {
            Console.WriteLine("Timer event fired at {0:F}", DateTime.Now);
        }

        Thread m_thread;
        UserControl1 form;

        void initThread()
        {
            form = new UserControl1()
            {
                StartPosition = FormStartPosition.CenterParent,
                TopLevel = true,
                ControlBox = false,
                TopMost = true
            };

            m_thread = new Thread(new ParameterizedThreadStart(threadWorker))
            {
                IsBackground = true
            };
            m_thread.SetApartmentState(ApartmentState.STA);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            initThread();

            m_thread.Start(form);

            //Thread.Sleep(5000);

            //if (m_thread != null)
            //{

            //    try
            //    {
            //        m_thread.Interrupt(); //쓰레드가 동작중인 상태를 피해서 WaitSleepJoin 스레드 상태에 들어갔을 때  ThreadInterruptedException을 던져 예외처리부에서 쓰레드를 중지시                                  킴
            //        m_thread.Join(); // 
            //    }
            //    catch (Exception)
            //    {
            //    }

            //    if (form != null)
            //    {
            //        form.Invoke(new Action(() =>
            //        {
            //            form.Close();
            //            form.Dispose();
            //            form = null;
            //        }
            //        ));

            //    }
            //}
        }

        static void threadWorker(object popup)
        {
            try
            {
                UserControl1 formProgress = (UserControl1)popup;

                if (formProgress.InvokeRequired)
                {
                    formProgress.BeginInvoke(new Action(() =>
                    {
                        //if (!formProgress.IsDisposed)
                      DialogResult result=  formProgress.ShowDialog();
                        //formProgress.Show();

                    }
                          ));
                }
                else
                {
                    DialogResult result = formProgress.ShowDialog();
                }

            }
            catch (System.Threading.ThreadAbortException) // ThreadAbortException을 처리해 주지 않으면 강제 종료 됨
            {
            }
            catch (System.Threading.ThreadInterruptedException) //스레드가 동작 중인 상태를 피해 예외를 던져 스레드를 중지
            {
                //
            }
            catch (System.ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                //CNotice.writeCatchMessage(ex);
            }
        }
    }

    class FormTest : Form
    {
        internal FormTest()
        {
            FormClosed += AFormClosed;
            FormClosing += AFormClosing;
        }
        void AFormClosed(object sender, FormClosedEventArgs e)
        {
            Debug.WriteLine("Form Closed");
        }
        void AFormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("Form Closing");
        }
    }

}
