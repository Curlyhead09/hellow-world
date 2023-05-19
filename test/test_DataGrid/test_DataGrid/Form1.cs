using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_DataGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;


        }


        Data data = new Data();
        private void Form1_Load(object sender, EventArgs e)
        {
            

            ultraGrid1.DataSource = data.getSampleData();
        }

        private void ultraGrid1_AfterCellUpdate(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {

        }

        //ultragrid 설정
        //https://yangjetmul.tistory.com/209

    }

    public class Data
    {
        public int data1 { get; set; }
        public int data2 { get; set; }
        public int data3 { get; set; }

        public Data()
        {

        }

        public List<Data> getSampleData()
        {
            List<Data> ltData = new List<Data>();

            for (int i = 0; i < 10; i++)
            {
                Data newData = new Data()
                {
                    data1 = i,
                    data2 = i + 1,
                    data3 = i + 2
                };

                ltData.Add(newData);
            }
            return ltData;
        }
    }


}
