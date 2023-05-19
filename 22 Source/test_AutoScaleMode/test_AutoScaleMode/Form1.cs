using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_AutoScaleMode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    this.AutoScaleMode = AutoScaleMode.None;
                    break;
                case 1:
                    this.AutoScaleMode = AutoScaleMode.Dpi;
                    break;
                case 2:
                    this.AutoScaleMode = AutoScaleMode.Font;
                    break;
                case 3:
                    this.AutoScaleMode = AutoScaleMode.Inherit;
                    break;
            }
        }
    }
}
