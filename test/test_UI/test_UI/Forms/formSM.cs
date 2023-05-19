using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_UI.Forms
{
    public partial class formSM : Form
    {
        const int MAX_SLIDING_WIDTH = 200;
        const int MIN_SLIDING_WIDTH = 50;
        //슬라이딩 메뉴가 보이는/접히는 속도 조절
        const int STEP_SLIDING = 10;
        //최초 슬라이딩 메뉴 크기
        int _posSliding = 200;

        public formSM()
        {
            InitializeComponent();
        }

        //https://luckygg.tistory.com/340

        private void timerSliding_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //슬라이딩 메뉴를 숨기는 동작
                _posSliding -= STEP_SLIDING;
                if (_posSliding <= MIN_SLIDING_WIDTH)
                    timerSliding.Stop();
            }
            else
            {
                _posSliding += STEP_SLIDING;
                if (_posSliding >= MAX_SLIDING_WIDTH)
                    timerSliding.Stop();
            }

            panel1.Width = _posSliding;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Text = "1";
                button2.Text = "2";
                checkBox1.Text = ">";
            }
            else
            {
                button1.Text = "버튼 1";
                button2.Text = "버튼 2";
                checkBox1.Text = "<";
            }

            timerSliding.Start();

        }
    }
}
