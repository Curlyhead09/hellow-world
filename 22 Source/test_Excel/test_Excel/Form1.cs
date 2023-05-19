using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace test_Excel
{
    public partial class Form1 : Form
    {
        Excel_Programming excel = new Excel_Programming();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            excel.autoMationRun1();
        }    
        private void button1_Click(object sender, EventArgs e)
        {
            excel.autoMationRun2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            excel.oledbRun(); // 설리 필요
        }

        private void button4_Click(object sender, EventArgs e)
        {
            excel.autoMationRun3();
        }
    }
}
