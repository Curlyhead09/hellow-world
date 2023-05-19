using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace test_Security
{
     class MacAddressHelper
    {
        //.net 2.0 방식

        public static string MacAddress
        {
            get
            { return getMacAddress(); }
        }

        public static string NetworkMacAddress
        {
            get
            { return getMacAddress2(); }
        }

        public static string MostUsedNetworkMacAddress
        {
            get
            { return getMacAddress3(); }
        }

        public static string ManagementObjectMacAddress { get { return getMacAddress4(); }  }

        public static string MacUsingARP { get { return getMacUsingARP(getIPAddress()); } }



        //가상 환경이 사용중인 경우 값이 다르게 읽어지는 경우가 발생 (값이 변경)
        private static string getMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();

            //NetworkInterface
            //  네트워크 인터페이스에 대한 구성 및 통계 정보를 제공합니다.

            //GetAllNetworkInterfaces
            //  로컬 컴퓨터의 네트워크 인터페이스를 설명하는 개체를 반환합니다. (랜카드 리스트)

            //GetPhysicalAddress
            //  이 어댑터에 대한 MAC(Media Access Control) 또는 실제 주소를 반환합니다.
        }




        //네트워크 어댑터의 MAC 주서를 가져오는 방식
        //현재 사용중인 네트워크 어댑터의 MAC 주소 
        //동시 사용중인 경우 문제가 발생
        private static string getMacAddress2()
        {
            var macAddr = from nic in NetworkInterface.GetAllNetworkInterfaces()
                          where nic.OperationalStatus == OperationalStatus.Up //사용중인 랜카드 구분
                          select nic.GetPhysicalAddress().ToString();


            if (macAddr != null && macAddr.Count() == 0) return null;

            return macAddr.FirstOrDefault();
        }


        //가장 많이 사용되고 있는 네트워크 어탭터의 MAC 주소를 가져오는 방식
        //송수신량이 가장 많은 MAC 주소를 가져오는 로직
        private static string getMacAddress3()
        {
            Dictionary<string, long> ltmacAddress = new Dictionary<string, long>();

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    ltmacAddress[nic.GetPhysicalAddress().ToString()] = nic.GetIPStatistics().BytesSent + nic.GetIPStatistics().BytesReceived;
                }
            }

            long maxValue = 0;
            string mac = string.Empty;

            foreach (KeyValuePair<string, long> pair in ltmacAddress)
            {
                if (pair.Value > maxValue)
                {
                    mac = pair.Key;
                    maxValue = pair.Value;
                }
            }

            return mac;
        }



        private static string getMacAddress4()
        {
            string macAddress = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

            using (ManagementObjectCollection moc = mc.GetInstances())
            {
                foreach (ManagementObject mo in moc)
                {
                    if (mo["MacAddress"]!=null)
                    {
                        if ((bool)mo["IPEnabled"]==true)
                        {
                            macAddress = mo["MacAddress"].ToString();
                        }
                        mo.Dispose();
                    }
                }
            }

            return macAddress;
        }

        // WMI(Windows Management Instrumentation)
        //  WMI가 실행 가능한 솔루션이 아닌 경우 이것이 가장 좋은 옵션

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        private static string getMacUsingARP(string IPAddr)
        {
            IPAddress IP = IPAddress.Parse(IPAddr);
            byte[] macAddr = new byte[6];
            uint macAddrLen = (uint)macAddr.Length;

            if (SendARP((int)IP.Address, 0, macAddr, ref macAddrLen) != 0)
                throw new System.Exception("ARP command failed");

            string[] str = new string[(int)macAddrLen];
            for (int i = 0; i < macAddrLen; i++)            
                str[i] = macAddr[i].ToString("x2");

            return string.Join(":", str);
        }


        //--------------------------------------- IP ---------------------------------------


        public string IP { get { return getIPAddress(); } }
        private static string getIPAddress()
        {
            string ip = string.Empty;
            IPAddress[] host = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (var item in host)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = item.ToString();
                }
            }

            return ip;
        }

        private static string getIPAddress2()
        {
            string ipAddress = string.Empty;

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8,8,8,8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;

                ipAddress = endPoint.Address.ToString();
            }
            return ipAddress;
        }
    }
}
