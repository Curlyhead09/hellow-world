using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace test_Security
{
    class TimeCheckHelper
    {

        public static DateTime GoogleDateTime { get { return getGoogleDateTime(); } }

        public static DateTime TimeNistGov { get { return getTimeNistGov(); } }

        public static DateTime WindowsDateTime { get { return getWindowsDateTime(); } }

        static DateTime getGoogleDateTime()
        {
            DateTime dateTime = DateTime.MinValue;

            try
            {
                //using
                //개체의 범위를 정의할때 사용한다. 그 범위를 벗어나면 자동으로 Dispose 된다.
                //File이나 Font, DB Connection 관련 클래스들은 관리되지 않는 리소스에 액세스 합니다.
                //사용 후 적절하게 Dispose해서 자원을반납해야 합니다.
                //하지만 종종 Dispose를 하지 않아서 리소스가 낭비되거나 DB Connection 같은 것을 Open만하고 Close하지 않으면문제가 발생합니다.
                //이때 일일이 Close하지 않고 Using을 이용하면 그 범위를 벗어나면 자동으로 Dispose 되서 관리가 쉬워집니다.

                using (var response = WebRequest.Create("http://www.google.com").GetResponse())
                {
                    dateTime = DateTime.ParseExact(response.Headers["date"], "dddd.dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                }
            }
            catch (Exception)
            {
                dateTime = DateTime.Now;
            }

            return dateTime;
        }

        static DateTime getWindowsDateTime()
        {
            string ntpServer = "time.windows.com";

            IPAddress[] address = Dns.GetHostEntry(ntpServer).AddressList;

            if (address == null || address.Length == 0)
                throw new ArgumentException("Could not resolve ip address from '" + ntpServer + "'.", "ntpServer");

            IPEndPoint ep = new IPEndPoint(address[0], 123);

            return GetNetworkTime(ep);
        }

        static DateTime GetNetworkTime(IPEndPoint ep)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); 
            s.Connect(ep); 
            byte[] ntpData = new byte[48]; // RFC 2030    
                                           //        
            ntpData[0] = 0x1B; 
            
            for (int i = 1; i < 48; i++) 
                ntpData[i] = 0; 
            
            s.Send(ntpData); 
            s.Receive(ntpData); 
            byte offsetTransmitTime = 40; 
            ulong intpart = 0; 
            ulong fractpart = 0; 
            
            for (int i = 0; i <= 3; i++) 
                intpart = 256 * intpart + ntpData[offsetTransmitTime + i]; 
            
            for (int i = 4; i <= 7; i++) 
                fractpart = 256 * fractpart + ntpData[offsetTransmitTime + i];
            
            ulong milliseconds = (intpart * 1000 + (fractpart * 1000) / 0x100000000L); 
            s.Close(); 
            TimeSpan timeSpan = TimeSpan.FromTicks((long)milliseconds * TimeSpan.TicksPerMillisecond); 
            
            DateTime dateTime = new DateTime(1900, 1, 1);
            dateTime += timeSpan; 
            TimeSpan offsetAmount = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime); 
            DateTime networkDateTime = (dateTime + offsetAmount); 
            
            return networkDateTime;
        }

        static DateTime getTimeNistGov()
        {
            string responseText = null;
            DateTime utcdatetime = DateTime.MinValue;

            try
            {
                using (var client = new TcpClient("time.nist.gov", 13))
                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    //Thread.Sleep(5000);

                    responseText = streamReader.ReadToEnd();
                    var utcDateTimeString = responseText.Substring(7, 17);

                    DateTime.TryParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out utcdatetime);

                    //continue;여기서 반복? 대기를 해야 한는 걸로 보임


                    //var localDateTime = utcdatetime.ToLocalTime();

                }
            }
            catch (Exception)
            {
            }
            return utcdatetime;
        }

    }
}
