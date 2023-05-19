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

namespace Actuator_CAD
{
    public partial class popupProgress : Form
    {

        private Thread m_timerThread;

        public popupProgress()
        {
            InitializeComponent();
        }

        public void changeLabel(string str)
        {
            labelText.Text = str;
        }
    }
}
