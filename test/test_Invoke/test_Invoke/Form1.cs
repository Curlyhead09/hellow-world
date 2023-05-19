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

namespace test_Invoke
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Thread worker = new Thread(Run);
            worker.Start();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            UpdateTextBox(string.Empty);
        }



        private void Run()
        {
            Thread.Sleep(3000);
            string dbData = "Query Result";

            UpdateTextBox(dbData);
        }

        private void UpdateTextBox(string data)
        {
            //하나의 Form을 다른 thread에서 접근하게 될 경우에 기존의 Form과 충돌이 날 수 있다.
            //이 때 invoke 를 사용하여 실행하려고 하는 메소드의 대리자(delegate)를 실행시키면 된다.

            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new Action(() => textBox1.Text = data));

            }
            else
            {
                textBox1.Text = data;
            }
        }
    }
}
