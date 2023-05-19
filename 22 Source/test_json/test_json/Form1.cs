using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace test_json
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateJson();
        }

        private void CreateJson()
        {
            string path = Path.Combine(Application.StartupPath, "test.json");

            if (!File.Exists(path))
            {
                using (File.Create(path))
                {

                    MessageBox.Show("성공");
                }
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            uiRtb_Text.Clear();
            WriteJson();
        }

        private void WriteJson()
        {
            string path = Path.Combine(Application.StartupPath, "test.json");


            if (File.Exists(path))
            {
                InputJson(path);
            }
        }

        private void InputJson(string path)
        {
            var users = new[] { "USER1", "USER2", "USER3", "USER4" };

            JObject dbSpec = new JObject(
                new JProperty("IP", "127.0.0.1"),
                new JProperty("ID", "IDidIDid"),
                new JProperty("PW", "1234"),
                new JProperty("SID", "TEST"),
                new JProperty("DATABASE", "TEST")
                );
            dbSpec.Add("USERS", JArray.FromObject(users));

            File.WriteAllText(path, dbSpec.ToString());

            uiRtb_Text.Text = dbSpec.ToString();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            uiRtb_Text.Clear();
            ReadJson();
        }

        private void ReadJson()
        {
            string path = Path.Combine(Application.StartupPath, "test.json");
            string str = string.Empty;
            string users = string.Empty;

            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject json = (JObject)JToken.ReadFrom(reader);

                DataBase db = new DataBase();
                db.IP = json["IP"].ToString();
                db.ID = json["ID"].ToString();
                db.PW = json["PW"].ToString(); 
                db.SID = json["SID"].ToString(); 
                db.DATABASE = json["DATABASE"].ToString();

                var user = json.SelectToken("USERS");
                var cnt = user.Count();

                for (int i = 0; i < cnt; i++)
                {
                    var name = user[i].ToString();

                    if (i==0)
                    {
                        users += $"{name}";
                    }
                    else
                    {
                        users += $" , {name}";
                    }
                }

                str = $"IP : {db.IP}\n ID : {db.ID}\n PW : {db.PW}\n SID : {db.SID}\n DATABASE : {db.DATABASE}\n User : {users}"; 

                uiRtb_Text.Text = str;

            }
        }
    }

    public class DataBase
    {

        public string IP = string.Empty;
        public string ID = string.Empty;
        public string PW = string.Empty;
        public string SID = string.Empty;
        public string DATABASE= string.Empty;

    }

}
