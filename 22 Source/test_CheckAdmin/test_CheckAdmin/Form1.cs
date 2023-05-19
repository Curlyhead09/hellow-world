using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_CheckAdmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label4.Text = username;


            label5.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            label1.Text = IsAdministrator() ? "주인" : "하숙";
        }

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        [DllImport("shell32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsUserAnAdmin();

        private void btnAPI_Click(object sender, EventArgs e)
        {
            label2.Text = IsUserAnAdmin() ? "주인" : "하숙";

        }

        public static bool IsAdministrator2()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = IsAdministrator2() ? "주인" : "하숙";

        }

        //https://blog.codeinside.eu/2017/03/02/howto-get-user-information-and-groups-from-ad/
        private void button2_Click(object sender, EventArgs e)
        {

            //listBox1.Items.Clear();
            listBox1.DataSource = null;

            //using (DirectoryEntry d = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer"))
            //{
            //    using (DirectoryEntry g = d.Children.Find("Administrators", "group"))
            //    {
            //        var members = g.Invoke("Members", null);

            //        //var members = g.Properties["member"];
            //        //foreach (object member in (IEnumerable)members)
            //        //foreach (object member in members)
            //        //{
            //        //    DirectoryEntry x = new DirectoryEntry(member);
            //        //    Console.Out.WriteLine(x.Name);
            //        //}
            //    }
            //}
            string domainName = string.Empty;
            try
            {
                //listBox1.Items.Add("1");
                domainName = Environment.UserDomainName;
                //listBox1.Items.Add("2");
            }
            catch (Exception)
            {
                //listBox1.Items.Add("3-1");
                if (string.IsNullOrEmpty(domainName))
                {
                    //listBox1.Items.Add("없음");
                    return;
                }
            }

            //listBox1.Items.Add("3-2");
            if (string.IsNullOrEmpty(domainName))
            {
                //listBox1.Items.Add("없음");
                return;
            }
            //listBox1.Items.Add(domainName);

            //listBox1.Items.Add("4");
            string username = Environment.UserName;
            //listBox1.Items.Add("5");

            List<string> result = new List<string>();
            try
            {
                PrincipalContext domainContext;

                try
                {
                    domainContext = new PrincipalContext(ContextType.Domain, domainName);
                }
                catch (Exception)
                {

                    domainContext = new PrincipalContext(ContextType.Machine, domainName);
                }

                using (var searcher = new DirectorySearcher(new DirectoryEntry("LDAP://" + domainContext.Name)))
                {

                    //if (domainContext == null)
                    //{
                    //    listBox1.Items.Add("domainContext");

                    //    return;
                    //}

                    //if (searcher == null)
                    //{
                    //    listBox1.Items.Add("searcher");

                    //    return;
                    //}


                    searcher.Filter = String.Format("(&(objectClass=user)(sAMAccountName={0}))", username);
                    SearchResult sr = searcher.FindOne();

                    DirectoryEntry user = sr.GetDirectoryEntry();

                    // access to other user properties, via user.Properties["..."]

                    user.RefreshCache(new string[] { "tokenGroups" });

                    for (int i = 0; i < user.Properties["tokenGroups"].Count; i++)
                    {
                        SecurityIdentifier sid = new SecurityIdentifier((byte[])user.Properties["tokenGroups"][i], 0);
                        NTAccount nt = (NTAccount)sid.Translate(typeof(NTAccount));

                        result.Add(nt.Translate(typeof(NTAccount)).ToString() + " (" + sid + ")");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            //return result;
            listBox1.DataSource = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/3774072/how-can-i-get-all-users-in-a-local-group-with-good-performance
            contextType = ContextType.Machine;
            listBox2.DataSource = GetGroupMembers("Administrators");

        }


        public static ArrayList GetGroupMembers(string sGroupName)
        {
            ArrayList myItems = new ArrayList();
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);

            PrincipalSearchResult<Principal> oPrincipalSearchResult = oGroupPrincipal.GetMembers();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        public static GroupPrincipal GetGroup(string sGroupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        static ContextType contextType = ContextType.Machine;

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(contextType);
            return oPrincipalContext;
        }


        string username = Environment.UserName;

        string domainName = Environment.UserDomainName;

        private void button4_Click(object sender, EventArgs e)
        {


            label6.Text = UserIsAdmin(username, "Administrators") ? "주인" : "하숙";
        }


        //https://www.codeproject.com/Questions/1134711/Detect-admin-rights-in-Csharp
        public static bool UserIsAdmin(string userSamAccountName, string adminSamAccountName)
        {
            PrincipalContext context;

            try
            {
                context = new PrincipalContext(ContextType.Domain);
            }
            catch (Exception)
            {

                context = new PrincipalContext(ContextType.Machine);
            }
            //GroupPrincipal adminGroup = new GroupPrincipal(context, adminSamAccountName);

            GroupPrincipal adminGroup = GroupPrincipal.FindByIdentity(context, adminSamAccountName);
            UserPrincipal user = UserPrincipal.FindByIdentity(context, userSamAccountName);

            return user.IsMemberOf(adminGroup);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            contextType = ContextType.Domain;

            listBox3.DataSource = GetGroupMembers("Administrators");
        }
    }
}
