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

namespace test_SplashScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Normal;
            this.Focus(); this.Show();
        }

        SplashForm splashForm;

        private void button1_Click(object sender, EventArgs e)
        {

            //Application.Run(splashForm);

            SplashForm.ShowSplashScreen();
        }

        private void thread()
        {
            SplashForm splashForm = new SplashForm();
            //splashForm.Show();            
            Application.Run(splashForm);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SplashForm.FormClose();
        }
    }
}
