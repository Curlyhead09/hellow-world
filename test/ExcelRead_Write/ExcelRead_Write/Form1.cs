using System;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.IO;

namespace ExcelRead_Write
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

            txtWrite.Text = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();


            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            //if (xlRange.Cells[1, 2] != null && xlRange.Cells[1, 2].Value2 != null)
            //{
            //    txtRead.Text = xlRange.Cells[1, 2].Value2.ToString();
            //}

            StringBuilder sb = new StringBuilder();

            int col = Convert.ToInt32(numericUpDownCol.Value);
            int row = Convert.ToInt32(numericUpDownRow.Value);


            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= col; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        sb.Append(xlRange.Cells[i, j].Value2.ToString() + "\t");
                    }
                }
                sb.AppendLine();
            }

            //xlRange.Cells[row+1, col+1].Value2 = "test";


            txtWrite.Text = sb.ToString();

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();

            if (string.IsNullOrEmpty(ofd.FileName)) return;


            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ofd.FileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            xlWorksheet.Cells[13, 1] = txtWrite.Text;
            xlApp.Visible = false;
            xlApp.UserControl = false;
            xlWorkbook.Save();

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            excelApp = new Excel.Application();
            wb = excelApp.Workbooks.Add();
            ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            // 데이타 넣기
            int r = 1;

            ws.Cells[1, 1] = "test";


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xls";
            sfd.ShowDialog();

            if (string.IsNullOrEmpty(sfd.FileName)) return;


            // 엑셀파일 저장
            wb.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal);
            wb.Close(true);
            excelApp.Quit();

            ReleaseExcelObject(ws);
            ReleaseExcelObject(wb);
            ReleaseExcelObject(excelApp);


            //////////

            //Excel.Application xlApp = new Excel.Application();

            //object misValue = System.Reflection.Missing.Value;
            //Excel.Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);

            //Excel._Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.ActiveSheet;

            //xlWorkSheet.Cells[1, 1].Value2 = "ID";
            //xlWorkSheet.Cells[1, 2].Value2 = "Name";
            //xlWorkSheet.Cells[2, 1].Value2 = "1";
            //xlWorkSheet.Cells[2, 2].Value2 = "One";
            //xlWorkSheet.Cells[3, 1].Value2 = "2";
            //xlWorkSheet.Cells[3, 2].Value2 = "Two";


            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.DefaultExt = "xlsx";
            //sfd.ShowDialog();

            //if (string.IsNullOrEmpty(sfd.FileName)) return;

            //xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            ////xlWorkBook.Close(true, misValue, misValue);
            //xlWorkBook.Close();

            //xlApp.Quit();


            //Marshal.ReleaseComObject(xlWorkSheet);
            //Marshal.ReleaseComObject(xlWorkBook);
            //Marshal.ReleaseComObject(xlApp);

        }
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
