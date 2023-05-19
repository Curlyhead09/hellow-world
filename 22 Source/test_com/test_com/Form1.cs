using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace test_com
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ComLibrary.Class1 class1 = new ComLibrary.Class1();

            int A = int.Parse(textBox1.Text);
            int B = int.Parse(textBox2.Text);
            int C = 0;

            //class1.sum(A, B, ref C);

            textBox3.Text = C.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string savePath = System.IO.Directory.GetCurrentDirectory();
            string[,] arry = new string[,]
                {
                    { "","2018"},
                    { "","2019"},
                    { "","2020"},
                    { "","2021"},
                    { "","2022"},
                };

            //lessdotnet3(arry, savePath);
            //moredotnet4(arry, savePath);

        }


        void lessdotnet3(string[,] data, string savePath)
        {
            Excel.Application excelApp = new Excel.Application();

            excelApp.Workbooks.Add(Type.Missing);

            Excel.Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            for (int i = 0; i < data.Length; i++)
            {
                ((Excel.Range)workSheet.Cells[i + 1, 1]).Value2 = data[i, 0];
                ((Excel.Range)workSheet.Cells[i + 1, 2]).Value2 = data[i, 1];
            }

            workSheet.SaveAs(savePath + "\\test-book-dynamic.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelApp.Quit();
        }

        void moredotnet4(string[,] data, string savePath)
        {

            Excel.Application excelApp = new Excel.Application();

            excelApp.Workbooks.Add(Type.Missing);

            Excel.Worksheet workSheet = excelApp.ActiveSheet;


            for (int i = 0; i < data.Length; i++)
            {
                workSheet.Cells[i + 1, 1] = data[i, 0];
                workSheet.Cells[i + 1, 2] = data[i, 1];
            }

            workSheet.SaveAs(savePath + "\\test-book-dynamic.xlsx");
            excelApp.Quit();
        }
    }
}
