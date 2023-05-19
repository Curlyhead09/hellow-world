using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace test_Process
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                //Process.Start(Path.Combine(Environment.CurrentDirectory, "test_Process.exe"), @"c:\AD_Work\Actuator-CAT\Actuator-CAT_Solenoid_2D Tutorial rev(09).pdf");


                string userName = Environment.UserName;
                string domein = Environment.UserDomainName;

                Console.WriteLine("UserName: {0}", userName);
                Console.WriteLine("UserDomein: {0}", domein);

                string m_strADExeFileFullName = args[0];

                Console.WriteLine(m_strADExeFileFullName);

                //m_strADExeFileFullName = @"C:\AD_Work\Actuator-CAT\LV_20211125\current\MagneticForceCurrent.csv";


                Process m_process = new Process();

                m_process.StartInfo.FileName = m_strADExeFileFullName;

                SecureString securePwd = new SecureString();
                m_process.StartInfo.UserName = args[1];
                ////m_process.StartInfo.Domain = domein;

                //string m_strPW1 = "gtk_";
                //string m_strPW2 = "jrm_";
                //string m_strPW3 = "2020";
                //string m_strPW4 = "#@!";


                //string strTemp = m_strPW1 + m_strPW2 + m_strPW3 + m_strPW4;

                //string strTemp = "!QAZ2wsx";


                //for (int i = 0; i < strTemp.Length; i++)
                //    securePwd.AppendChar(strTemp[i]);

                //m_process.StartInfo.Password = securePwd;

                m_process.StartInfo.UseShellExecute = false;
                //m_process.StartInfo.LoadUserProfile = true;

                m_process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
