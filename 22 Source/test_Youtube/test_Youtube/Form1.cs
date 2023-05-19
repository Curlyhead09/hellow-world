using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Youtube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //AIzaSyBAWFYj0MH97Hlzi57gp2mW8nHq9LUS44w
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // YouTubeService 객체 생성
            var youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBAWFYj0MH97Hlzi57gp2mW8nHq9LUS44w", // 키 지정
                ApplicationName = "My YouTube Search"
            });

            // Search용 Request 생성
            var request = youtube.Search.List("snippet");
            request.Q = txtSearch.Text;  //ex: "양희은"
            request.MaxResults = 25;

            // Search용 Request 실행
            var result = await request.ExecuteAsync();

            // Search 결과를 리스트뷰에 담기
            foreach (var item in result.Items)
            {
                if (item.Id.Kind == "youtube#video")
                {
                    listView1.Items.Add(item.Id.VideoId.ToString(), item.Snippet.Title, 0);
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // YouTube 비디오 Play를 위한 URL 생성 
                string videoId = listView1.SelectedItems[0].Name;
                string youtubeUrl = "http://youtube.com/watch?v=" + videoId;

                // 디폴트 브라우져에서 실행
                //webBrowser1.Navigate(youtubeUrl + "?html5=1");
                //webView.CoreWebView2.Navigate(youtubeUrl);

                webView21.Source = new Uri(youtubeUrl);


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            //SetIE11KeyforWebBrowserControl(appName);
        }

        private void SetIE11KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {

                //MessageBox.Show(appName.ToString());
                // For 64 bit machine
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                else  //For 32 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                // If the path is not correct or
                // if the user haven't priviledges to access the registry
                if (Regkey == null)
                {
                    MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                // Check if key is already present
                if (FindAppkey == "11000")
                {
                    MessageBox.Show("Required Application Settings Present");
                    Regkey.Close();
                    return;
                }

                // If a key is not present add the key, Key value 8000 (decimal)
                if (string.IsNullOrEmpty(FindAppkey))
                    Regkey.SetValue(appName, unchecked((int)0x1F40), RegistryValueKind.DWord);

                // Check for the key after adding
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                if (FindAppkey == "11000")
                    MessageBox.Show("Application Settings Applied Successfully");
                else
                    MessageBox.Show("Application Settings Failed, Ref: " + FindAppkey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Settings Failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the Registry
                if (Regkey != null)
                    Regkey.Close();
            }
        }


    }
}
