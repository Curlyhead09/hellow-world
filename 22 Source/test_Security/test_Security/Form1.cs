using System;
using System.Windows.Forms;

namespace test_Security
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string mac1 = MacAddressHelper.MacAddress;
            string mac2 = MacAddressHelper.NetworkMacAddress;
            string mac3 = MacAddressHelper.MostUsedNetworkMacAddress;
            string mac4 = MacAddressHelper.ManagementObjectMacAddress;

            string mac5 = MacAddressHelper.MacUsingARP;

            //TimeCheckHelper
            DateTime time = TimeCheckHelper.GoogleDateTime;

            DateTime time1 = TimeCheckHelper.TimeNistGov;

            DateTime time2 = TimeCheckHelper.WindowsDateTime        ;

        }

    }
}
