using Actuator_CAD;
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

namespace Test_Thread_sleep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool bOpened = false;
            int nCount = 0;
            const int iWaitingTime = 60;

            popupProgress progress = new popupProgress();

            if (isOpenedReceiverForm() == false)
            {
                progress.changeLabel("AEDT 가 실행 중에 있습니다.");
                progress.Show();

                //loadAEDT();

                do
                {
                    //bOpened = isOpenedReceiverForm();
                    Thread.Sleep(1000);

                    nCount++;
                }
                while (bOpened == false && nCount < iWaitingTime);

                if (nCount >= iWaitingTime)
                {
                    //CNotice.noticeWarning("AEDT 실행에 문제가 발생하여 해석이 취소 되었습니다.");
                    return;
                }

                progress.Hide();         // 다음에도 사용하기 때문에 객체를 소멸시키는 Close() 대신 Hide() 를 사용한다.
            }
        }


        bool isOpenedReceiverForm()
        {
            return false;
        }
    }
}
