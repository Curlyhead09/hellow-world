using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace test_Registry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //        멤버 메서드 //설명

        //CreateSubKey//새 하위 키를 만들거나 기존 하위 키를 엽니다.

        //OpenSubKey//지정된 하위 키를 검색합니다.

        //DeleteSubKey//지정된 하위 키를 삭제합니다.

        //DeleteSubKeyTree//하위 키와 자식 하위 키를 재귀적으로 삭제합니다.

        //GetSubKeyNames//모든 하위 키 이름이 포함된 문자열의 배열을 검색합니다.

        //SetValue//레지스트리 키에서 이름/값 쌍의 값을 설정합니다.

        //GetValue//지정된 이름과 연결된 값을 검색합니다.

        //GetValueKind//지정된 이름과 연결된 값의 레지스트리 데이터 형식을 검색합니다.

        //GetValueNames//이 키와 관련된 모든 값 이름이 포함된 문자열의 배열을 검색합니다.

        //DeleteValue//지정된 값을 이 키에서 삭제합니다.



        private void btnCreateSub_Click(object sender, EventArgs e)
        {
            //RegistryKey reg = Registry.LocalMachine;


            //Create Sub Key
            //// 예 1
            //RegistryKey rkey = Registry.CurrentUser.CreateSubKey("c# rkey").CreateSubKey("testsubkey");
            //// 예 2
            //RegistryKey rkey = Registry.CurrentUser.CreateSubKey(@"c# rkey\testsubkey");
        }

        private void btnOpenSub_Click(object sender, EventArgs e)
        {
            //// 예 1
            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey("c# rkey").OpenSubKey("testsubkey");
            //// 예 2
            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey");

            //만약, OpenSubKey 메서드로 지정된 하위 키를 여는데, 이 키에 대한 쓰기 권한(Write Access)가 필요하면, 두번째 인자에 true라고 설정하시면 됩니다.

            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey", true);
        }

        private void btnDeleteSub_Click(object sender, EventArgs e)
        {
            // 하위 키 삭제: HKEY_CURRENT_USER\c# rkey\qwd
            //Registry.CurrentUser.DeleteSubKey(@"c# rkey\qwd");
        }

        private void btnSetValue_Click(object sender, EventArgs e)
        {
            //RegistryKey rkey = Registry.CurrentUser.CreateSubKey("c# rkey").CreateSubKey("testsubkey");
            //rkey.SetValue("test", "테스트!");
            //rkey.SetValue("text", "텍스트!");

        }

        private void btnGetValue_Click(object sender, EventArgs e)
        {
            //참고
            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey");
            //MessageBox.Show(rkey.GetValue("test").ToString());

        }

        private void btnGetValueTest_Click(object sender, EventArgs e)
        {

            GetInstalledApps();

            //   localMachine();

            // currentUser();

        }

        void localMachine()
        {
            //실패
            //installationDirectory 의 값을 확인 하여 
            //AEDT 설치 경로를 확인 하려고 하였으나 
            //관리자 계정으로 실행 된 regedit에서 보이는 Ansoft 접근이 안 됨
            //      C:\Program Files\AnsysEM\v222\Win64~~~~

            //      RegistryKey rkeyMachine = Registry.LocalMachine.OpenSubKey(@"SOFRWARE\Ansoft\ElectronicsDesktop\2022.2\Desktop\installationDirectory");

            //여기 까지만 접근 됨 
            RegistryKey rkey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE");
            //foreach (var item in rkey1.GetSubKeyNames())
            //{
            //    RegistryKey itemKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + "\\" + item);
            //}

            RegistryKey rkey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\ANSYS, Inc.");





            //Ansoft 보이지 않음??
            foreach (var item in rkey2.GetSubKeyNames())
            {
                //RegistryKey itemKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + "\\" + item);
            }

            RegistryKey rkey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\ANSYS, Inc.\\ANSYS Electromagnetics");

            //subkey 
            RegistryKey rkey4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\ANSYS, Inc.\\ANSYS Electromagnetics\\22.2.0");

        }

        void currentUser()
        {
            //CurrentUser 경로에는 아래 경로 까지는 있으나
            //installationDirectory 가 존재하지 않음
            //RegistryKey rkey1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Ansoft\ElectronicsDesktop\2022.2\Desktop");

            RegistryKey rkey1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE");

            foreach (var item in rkey1.GetSubKeyNames())
            {
                RegistryKey itemKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + "\\" + item);
            }

            //ANSYS와 관련 된 폴더 #1
            RegistryKey rkey2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Ansoft");

            RegistryKey rkey3 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Ansoft\\ElectronicsDesktop");

            RegistryKey rkey4 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Ansoft\\ElectronicsDesktop\\2022.2");

            //subKey
            //DockStates-Ribbon
            RegistryKey rkey5_1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Ansoft\\ElectronicsDesktop\\2022.2\\Desktop");

            //subKey
            //  Recent File List
            //  Settings
            RegistryKey rkey5_2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Ansoft\\ElectronicsDesktop\\2022.2\\Ansys Electronics Desktop");




            //ANSYS와 관련 된 폴더 #2
            rkey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\ANSYS, Inc.");

            rkey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\ANSYS, Inc.\\ANSYS Electromagnetics");

            //subkey 0
            rkey4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\ANSYS, Inc.\\ANSYS Electromagnetics\\22.2.0");

        }

        List<object> lstInstalled = new List<object>();

        public void GetInstalledApps()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            lstInstalled.Add(sk.GetValue("DisplayName"));
                        }
                        catch { }
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //ansys 관련 key 안보임
            RegistryKey rkey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");

            RegistryKey rkey2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion");

            RegistryKey rkey3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths");

            string rkey4 = rkey1.GetSubKeyNames()[63];

            RegistryKey rkey5 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + rkey4);


        }



        private void btnDeleteValue_Click(object sender, EventArgs e)
        {
            //// 하위 키 HKEY_CURRENT_USER\c# rkey\testsubkey에 있는 test 제거
            //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"c# rkey\testsubkey", true);
            //rkey.DeleteValue("test");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strCurSubKeyName = "SOFTWARE\\Classes\\CLSID";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(strCurSubKeyName);

            getRegistryKeyValue(key);
        }


        private void button8_Click(object sender, EventArgs e)
        {
            string strCurSubKeyName = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(strCurSubKeyName);

            getRegistryKeyValue(key, true);

            key = Registry.CurrentUser.OpenSubKey(strCurSubKeyName);

            //getRegistryKeyValue(key, true,false);

        }

        void getRegistryKeyValue(RegistryKey rkey1, bool showSubKeyName = false, bool doClear = true)
        {
            if (doClear)
            {
                listBox1.Items.Clear();
            }


            if (showSubKeyName)
            {
                foreach (string s in rkey1.GetSubKeyNames())
                {

                    using (RegistryKey subkey = rkey1.OpenSubKey(s))
                    {
                        //Console.WriteLine(subkey.GetValue("DisplayName"));
                        try
                        {
                            listBox1.Items.Add(subkey.GetValue("DisplayName"));

                            if (subkey.GetValue("DisplayName").ToString().Contains("Electro"))
                            {

                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            else
            {
                foreach (var item in rkey1.GetSubKeyNames())
                {
                    RegistryKey rkey2 = rkey1.OpenSubKey(item);

                    //setValuesToList(rkey2);

                    var value = rkey2.GetValue("");
                    if (value != null)
                    {
                        listBox1.Items.Add(value.ToString());
                    }
                }
            }



            listBox1.Sorted = true;
        }

        void setValuesToList(RegistryKey rkey)
        {
            foreach (var item in rkey.GetSubKeyNames())
            {
                RegistryKey rkey2 = rkey.OpenSubKey(item);
            }

            foreach (var item in rkey.GetValueNames())
            {
                var value = rkey.GetValue(item);

                if (string.IsNullOrEmpty(value.ToString())) continue;



                //listBox1.Items.Add(value);
                //ltItem.Add(value.ToString()); ;

                dataModel data = new dataModel() { Value = value.ToString(), ValueName = item, SubKey = rkey.Name };
                ltItem.Add(data);

            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;


                //LocalMachine+ Class Root
                dataGridView1.DataSource = null;

                getLocalMachine_CLSIDList();

                getClassesRoot_CLSIDList(false);


                //listBox1.Items.AddRange(ltItem);
                ltItem.Sort(CompareDinosByLength<dataModel>);
                dataGridView1.DataSource = ltItem;

                //foreach (DataGridViewColumn column in dataGridView1.Columns)
                //{
                //    column.SortMode = DataGridViewColumnSortMode.Automatic;
                //}
            }
            catch (Exception)
            {


            }
            finally
            {
                Cursor = Cursors.Default;
            }


        }

        private static int CompareDinosByLength<T>(T data1, T data2)
        {
            string x = null;
            string y = null;

            if (data1 is dataModel dataModel1)
            {
                x = dataModel1.Value;
            }

            if (data2 is dataModel dataModel2)
            {
                y = dataModel2.Value;
            }

            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal.
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater.
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the
                    // lengths of the two strings.
                    //
                    int retval = x.Length.CompareTo(y.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.CompareTo(y);
                    }
                }
            }
        }

        List<dataModel> ltItem;

        void getLocalMachine_CLSIDList(bool isInit = true)
        {
            if (isInit)
                ltItem = new List<dataModel>();

            //https://docs.microsoft.com/ko-kr/windows/win32/com/hkey-local-machine-software-classes
            RegistryKey rkey1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Classes\\CLSID");

            foreach (var item in rkey1.GetSubKeyNames())
            {
                RegistryKey rkey2 = rkey1.OpenSubKey(item);

                setValuesToList(rkey2);

                //var value = rkey2.GetValue("");
                //if (value != null)
                //{
                //    listBox1.Items.Add(value.ToString());
                //}
                //else if (value == null)
                //{
                //    Console.WriteLine(rkey2.GetSubKeyNames()[0]);
                //}
            }
        }

        void getClassesRoot_CLSIDList(bool isInit = true)
        {
            if (isInit)
                ltItem = new List<dataModel>();

            RegistryKey rkey1 = Registry.ClassesRoot.OpenSubKey(@"CLSID");
            foreach (var item in rkey1.GetSubKeyNames())
            {
                RegistryKey rkey2 = rkey1.OpenSubKey(item);
                setValuesToList(rkey2);
            }
        }

        void getClassesRoot_CLSID()
        {
            ltItem = new List<dataModel>();

            RegistryKey rkey1 = Registry.ClassesRoot.OpenSubKey(@"CLSID");
            foreach (var item in rkey1.GetSubKeyNames())
            {
                RegistryKey rkey2 = rkey1.OpenSubKey(item);
                setValuesToList(rkey2);
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            RegistryKey rkey1 = Registry.ClassesRoot.OpenSubKey(@"CLSID");

            foreach (var item in rkey1.GetSubKeyNames())
            {
                RegistryKey rkey2 = rkey1.OpenSubKey(item);

                if (rkey2.GetSubKeyNames().Length == 0) continue;

                if (rkey2.GetSubKeyNames()[0].Equals("InProcServer32"))
                {
                    RegistryKey rkey3 = rkey2.OpenSubKey("InProcServer32");

                    foreach (var name in rkey3.GetValueNames())
                    {
                        var value = rkey3.GetValue(name);
                        listBox1.Items.Add(value.ToString());

                    }


                }


            }
            listBox1.Sorted = true;
        }

        private void FindMyString(string searchString)
        {
            // Ensure we have a proper string to search for.
            if (!string.IsNullOrEmpty(searchString))
            {
                // Find the item in the list and store the index to the item.
                int index = listBox1.FindString(searchString);
                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                    listBox1.SetSelected(index, true);
                //else
                //    MessageBox.Show("The search string did not match any items in the ListBox");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //FindMyString(textBox1.Text);


            string myString = textBox1.Text;
            //bool found = false;
            int idxFirst = -1;
            for (int i = 0; i <= listBox1.Items.Count - 1; i++)
            {
                if (listBox1.Items[i].ToString().Contains(myString))
                {
                    if (idxFirst == -1)
                        idxFirst = i;

                    if (listBox1.SelectedIndex == i || listBox1.SelectedIndex > i)
                        continue;

                    listBox1.SetSelected(i, true);
                    //found = true;
                    return;
                }
            }



            if (idxFirst != -1)
                listBox1.SetSelected(idxFirst, true);


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //string myString = textBox1.Text;
            ////bool found = false;
            //for (int i = 0; i <= listBox1.Items.Count - 1; i++)
            //{
            //    if (listBox1.Items[i].ToString().Contains(myString))
            //    {
            //        listBox1.SetSelected(i, true);
            //        //found = true;
            //        break;
            //    }
            //}
            //if (!found)
            //{
            //    MessageBox.Show("Item not found!");
            //}
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button4.PerformClick();
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //string myString = textBox1.Text;
            ////bool found = false;
            //int idxFirst = -1;
            //for (int i = 0; i <= dataGridView1.DataSource.Count - 1; i++)
            //{
            //    if (listBox1.Items[i].ToString().Contains(myString))
            //    {
            //        if (idxFirst == -1)
            //            idxFirst = i;

            //        if (listBox1.SelectedIndex == i || listBox1.SelectedIndex > i)
            //            continue;

            //        listBox1.SetSelected(i, true);
            //        //found = true;
            //        return;
            //    }
            //}



            //if (idxFirst != -1)
            //    listBox1.SetSelected(idxFirst, true);

            string myString = textBox2.Text;
            int idxRow = -1;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString().Contains(myString))
                    {
                        if (idxRow == -1)
                            idxRow = i;

                        if (dataGridView1.SelectedRows.Count > 0 && (dataGridView1.SelectedRows[0].Index == i || dataGridView1.SelectedRows[0].Index > i))
                            continue;

                        dataGridView1.ClearSelection();

                        dataGridView1.Rows[row.Index].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        //found = true;
                        return;
                    }
                }
            }

            if (idxRow != -1)
            {
                dataGridView1.Rows[idxRow].Selected = true;
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (true)
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    button7.PerformClick();
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;


                //LocalMachine+ Class Root
                dataGridView1.DataSource = null;

                getClassesRoot_CLSID();


                //listBox1.Items.AddRange(ltItem);
                ltItem.Sort(CompareDinosByLength<dataModel>);
                dataGridView1.DataSource = ltItem;

                //foreach (DataGridViewColumn column in dataGridView1.Columns)
                //{
                //    column.SortMode = DataGridViewColumnSortMode.Automatic;
                //}
            }
            catch (Exception)
            {


            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            //Windows 레지스트리의 키 수준 노드를 나타냅니다. 이 클래스는 레지스트리 캡슐화 클래스입니다.
            RegistryKey Root = Registry.ClassesRoot.OpenSubKey("CLSID"); // CLSID 클래스 식별자

            foreach (var ClassId in Root.GetSubKeyNames())
            {
                RegistryKey ProgId = Root.OpenSubKey(ClassId).OpenSubKey("ProgId");

                if (ProgId!=null)
                {
                    if(ProgId.GetValue("") !=null)
                    listBox1.Items.Add(ProgId.GetValue(""));                   

                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            RegistryKey root = Registry.LocalMachine;
            //RegistryKey softwareRoot = root.OpenSubKey("SOFTWARE");
            //RegistryKey ansysRoot = softwareRoot.OpenSubKey("ANSYS, Inc.");
            //RegistryKey etRoot = ansysRoot.OpenSubKey("ANSYS Electromagnetics");

            //// 제대로 된 정보가 안나옴
            //RegistryKey v2020 = etRoot.OpenSubKey("20.2.0");
            //RegistryKey v2220 = etRoot.OpenSubKey("22.2.0");

            RegistryKey classRoot = root.OpenSubKey("SOFTWARE\\Classes"); //https://learn.microsoft.com/ko-kr/windows/win32/com/hkey-local-machine-software-classes
            
            foreach (var item in classRoot.GetSubKeyNames())
            {
                if (item.Contains("Ansoft.ElectronicsDesktop"))
                {
                    RegistryKey key= classRoot.OpenSubKey(item);

                    //RegistryKey key2 = key.OpenSubKey("CLSID");

                    listBox1.Items.Add(item);
                }                
            }

            RegistryKey versionRoot = root.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"); // https://learn.microsoft.com/ko-kr/windows/win32/com/hkey-local-machine-software-microsoft-windows-nt-currentversion

            //RegistryKey progIDRoot = classRoot.OpenSubKey("ProgID");

            // end LocalMachine


            //  x 설치정보 없음
            //RegistryKey rootC = Registry.CurrentUser;
            //RegistryKey softwareRootC = rootC.OpenSubKey("SOFTWARE");
            //RegistryKey ansysRootC = softwareRootC.OpenSubKey("ANSYS, Inc.");
            //RegistryKey ansys = ansysRootC.OpenSubKey("ANSYS");
            //RegistryKey v202 = ansys.OpenSubKey("ANSYS 20.2");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            RegistryKey root = Registry.LocalMachine;

            RegistryKey classRoot = root.OpenSubKey("SOFTWARE\\Classes");


            //RegistryKey progid = classRoot.OpenSubKey("PROGID");
            RegistryKey clsid = classRoot.OpenSubKey("CLSID");



            foreach (var item in clsid.GetSubKeyNames())
            {
                if (item.Contains("Ansoft.ElectronicsDesktop"))
                {
                    RegistryKey key = classRoot.OpenSubKey(item);

                    //RegistryKey key2 = key.OpenSubKey("CLSID");

                    listBox1.Items.Add(item);
                }
            }

            RegistryKey versionRoot = root.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"); // https://learn.microsoft.com/ko-kr/windows/win32/com/hkey-local-machine-software-microsoft-windows-nt-currentversion

            //RegistryKey progIDRoot = classRoot.OpenSubKey("ProgID");

            // end LocalMachine


            //  x 설치정보 없음
            //RegistryKey rootC = Registry.CurrentUser;
            //RegistryKey softwareRootC = rootC.OpenSubKey("SOFTWARE");
            //RegistryKey ansysRootC = softwareRootC.OpenSubKey("ANSYS, Inc.");
            //RegistryKey ansys = ansysRootC.OpenSubKey("ANSYS");
            //RegistryKey v202 = ansys.OpenSubKey("ANSYS 20.2");
        }
    }

    class dataModel
    {

        public string Value { get; set; }
        public string ValueName { get; set; }
        public string SubKey { get; set; }


    }

}
