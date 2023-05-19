using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_MuliLanguage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex ==0)            
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
             
            
            else
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ko-KR");




            setTextLanguage();

        }

        private void setTextLanguage()
        {
            label2.Text = Str.Connect;
            label3.Text = Str.Disconnect;
            button1.Text = Str.Open;
            textBox1.Text = Str.Close;
        }
    }
}
