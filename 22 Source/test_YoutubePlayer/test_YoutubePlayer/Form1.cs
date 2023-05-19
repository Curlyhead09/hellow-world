using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_YoutubePlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += "<iframe id='video' src= 'https://www.youtube.com/embed/{0}' width='{1}' height='{2}' frameborder='0' allowfullscreen></iframe>";
            html += "</body></html>";
            this.webBrowser1.DocumentText = string.Format(html, textBox1.Text.Split('=')[1], webBrowser1.Width, webBrowser1.Height);
        }
    }
}
