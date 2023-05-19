using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Firewall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyFirewallManager firewallMng = new MyFirewallManager();
            FirewallAppInfo appInfo = firewallMng.getAppInfo(Application.ExecutablePath);
            // 방화벽 앱 목록에 없음 - 프로그램을 최초로 실행시키는 경우
            if (appInfo.mListAdded == false)
            {
                // TODO : 필요한 처리 추가
            }
            // 방화벽에 허용된 앱으로 등록되지 않았음 - "액세스" 허용 하지 않은 경우
            else if (appInfo.mEnabled == false)
            {    // TODO : 필요한 처리 추가}

            }
        }
    }
}
