using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace test_msChart
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();      

        }            




        Form1 form1 ;
        Form2 form2 ;
        Form3 form3 ;
        Form4 form4;



        private void button1_Click_1(object sender, EventArgs e)
        {
            form1 = new Form1();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form4 = new Form4();
            form4.Show();

        }
    }
}
