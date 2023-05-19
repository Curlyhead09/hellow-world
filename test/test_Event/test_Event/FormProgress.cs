using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_Event;

namespace test_Event
{
    public partial class FormProgress : Form
    {
        private Form1 formMain;
        public bool useClose = false;
        public FormProgress(Form1 formMain)
        {
            InitializeComponent();


            this.formMain = formMain;
        }

        public void changeLabel(string str)
        {
            labelText.Text = str;
        }


        //protected override void WndProc(ref Message message)
        //{
        //    const int WM_SYSCOMMAND = 0x0112;
        //    const int SC_MOVE = 0xF010;

        //    switch (message.Msg)
        //    {
        //        case WM_SYSCOMMAND:
        //            int command = message.WParam.ToInt32() & 0xfff0;
        //            if (command == SC_MOVE) ////https://stackoverflow.com/questions/907830/how-do-you-prevent-a-windows-from-being-moved
        //                return;
        //            break;
        //    }

        //    base.WndProc(ref message);
        //}


        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;

        private void FormProgress_Resize(object sender, EventArgs e)
        {
            //if (WindowState == FormWindowState.Minimized)
            //{

            //    if (formMain.InvokeRequired)
            //    {
            //        formMain.BeginInvoke(
            //            new Action(() =>
            //            {
            //                ShowWindow(formMain.Handle, SW_MINIMIZE);
            //            }
            //                     ));
            //    }


            //}
            //else if (WindowState == FormWindowState.Normal)
            //{
            //    if (formMain.InvokeRequired)
            //    {
            //        formMain.BeginInvoke(
            //            new Action(() =>
            //            {
            //                ShowWindow(formMain.Handle, SW_RESTORE);
            //            }
            //                     ));
            //    }
            //}



            //해석 진행 중에 멈춰 있기 때문에 의미 없음
            if (WindowState == FormWindowState.Minimized)
            {
                if (formMain.InvokeRequired)
                {
                    formMain.BeginInvoke(
                        new Action(() =>
                        {
                            formMain.WindowState = FormWindowState.Minimized;

                            formMain.Update();
                        }
                                 ));
                }
            }
            else if (WindowState == FormWindowState.Normal)
            {
                if (formMain.InvokeRequired)
                {
                    formMain.BeginInvoke(
                        new Action(() =>
                        {
                            formMain.WindowState = FormWindowState.Normal;

                            formMain.Update();
                        }
                                 ));

                    this.Activate();

                }
            }
        }

        private void FormProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!useClose)
                e.Cancel = true;
        }
    }
}
