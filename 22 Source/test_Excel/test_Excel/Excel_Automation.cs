using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace test_Excel
{
    class Excel_Programming
    {        
        //Excel AutoMation
        //c#에서 엑셀 오토메이션을 이용하기 위해서는 Excel Interop을 참조한 후, office Automation Com API들을 사용
        //Com Interop DLL 참조
        //  Microsoft Excecl Object Library (COM) or Microsoft.Office.Interop.Excel.dll (Assembly)
        public void autoMationRun1()
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;

                //get a new workbook
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                //add table headers going cell by cell
                oSheet.Cells[1, 1] = "First Name";
                oSheet.Cells[1, 2] = "Last Name";
                oSheet.Cells[1, 3] = "Full Name";
                oSheet.Cells[1, 4] = "Salary";

                //format A1:D1 as bold, vertical alignment center
                //oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.Range["A1", "D1"].Font.Bold = true;
                oSheet.Range["A1", "D1"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";

                saNames[1, 0] = "Tom";
                saNames[1, 1] = "Brown";

                saNames[2, 0] = "Sue";
                saNames[2, 1] = "Thomas";

                saNames[3, 0] = "Jane";
                saNames[3, 1] = "Jones";

                saNames[4, 0] = "Adam";
                saNames[4, 1] = "Johnson";

                //fill A2:B6
                oSheet.get_Range("A2", "B6").Value2 = saNames;

                oRng = oSheet.Range["C2", "C6"];
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                //Manipulate a variable number of columns for Quarterly Sales Data.
                DisplayQuarterlySales(oSheet);

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        internal void autoMationRun3()
        {
            //대량의 데이터 처리
            //  cell에 대이터를 하나씩 넣게 되면 여러번 COM 메서드를 호출하게 되어 성능이 저하
            //  range를 한번 호출하여 대량의 배열 데이터를 한번에 처리ㅏ하는 것이 효율적

            // UsedRange = 현재 시트의 전체 범위를 선택

            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();

                // 엑셀 파일 열기
                wb = excelApp.Workbooks.Open(@"C:\Temp\test.xlsx");

                // 첫번째 Worksheet
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;

                // 현재 Worksheet에서 사용된 Range 전체를 선택
                Excel.Range rng = ws.UsedRange;

                // 현재 Worksheet에서 일부 범위만 선택
                // Excel.Range rng = ws.Range[ws.Cells[2, 1], ws.Cells[5, 3]];

                // Range 데이타를 배열 (One-based array)로
                object[,] data = rng.Value;

                for (int r = 1; r <= data.GetLength(0); r++)
                {
                    for (int c = 1; c <= data.GetLength(1); c++)
                    {
                        Debug.Write(data[r, c].ToString() + " ");
                    }
                    Debug.WriteLine("");
                }

                wb.Close(true);
                excelApp.Quit();
            }
            finally
            {
                // Clean up
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }
        }

        private void DisplayQuarterlySales(Excel._Worksheet oWS)
        {
            Excel._Workbook oWB;
            Excel.Series oSeries;
            Excel.Range oResizeRange;
            Excel._Chart oChart;
            String sMsg;
            int iNumQtrs;

            //Determine how many quarters to display data for.
            for (iNumQtrs = 4; iNumQtrs >= 2; iNumQtrs--)
            {
                sMsg = "Enter sales data for ";
                sMsg = String.Concat(sMsg, iNumQtrs);
                sMsg = String.Concat(sMsg, " quarter(s)?");

                DialogResult iRet = MessageBox.Show(sMsg, "Quarterly Sales?",
                MessageBoxButtons.YesNo);
                if (iRet == DialogResult.Yes)
                    break;
            }

            sMsg = "Displaying data for ";
            sMsg = String.Concat(sMsg, iNumQtrs);
            sMsg = String.Concat(sMsg, " quarter(s).");

            MessageBox.Show(sMsg, "Quarterly Sales");

            //Starting at E1, fill headers for the number of columns selected.
            oResizeRange = oWS.get_Range("E1", "E1").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=\"Q\" & COLUMN()-4 & CHAR(10) & \"Sales\"";

            //Change the Orientation and WrapText properties for the headers.
            oResizeRange.Orientation = 38;
            oResizeRange.WrapText = true;

            //Fill the interior color of the headers.
            oResizeRange.Interior.ColorIndex = 36;

            //Fill the columns with a formula and apply a number format.
            oResizeRange = oWS.get_Range("E2", "E6").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=RAND()*100";
            oResizeRange.NumberFormat = "$0.00";

            //Apply borders to the Sales data and headers.
            oResizeRange = oWS.get_Range("E1", "E6").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            //Add a Totals formula for the sales data and apply a border.
            oResizeRange = oWS.get_Range("E8", "E8").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=SUM(E2:E6)";
            oResizeRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle
            = Excel.XlLineStyle.xlDouble;
            oResizeRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).Weight
            = Excel.XlBorderWeight.xlThick;

            //Add a Chart for the selected data.
            oWB = (Excel._Workbook)oWS.Parent;
            oChart = (Excel._Chart)oWB.Charts.Add(Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);

            //Use the ChartWizard to create a new chart from the selected data.
            oResizeRange = oWS.get_Range("E2:E6", Missing.Value).get_Resize(
            Missing.Value, iNumQtrs);

            oChart.ChartWizard(oResizeRange, Excel.XlChartType.xl3DColumn, Missing.Value,
            Excel.XlRowCol.xlColumns, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            oSeries = (Excel.Series)oChart.SeriesCollection(1);
            oSeries.XValues = oWS.get_Range("A2", "A6");
            for (int iRet = 1; iRet <= iNumQtrs; iRet++)
            {
                oSeries = (Excel.Series)oChart.SeriesCollection(iRet);
                String seriesName;
                seriesName = "=\"Q";
                seriesName = String.Concat(seriesName, iRet);
                seriesName = String.Concat(seriesName, "\"");
                oSeries.Name = seriesName;
            }

            oChart.Location(Excel.XlChartLocation.xlLocationAsObject, oWS.Name);

            //Move the chart so as not to cover your data.
            oResizeRange = (Excel.Range)oWS.Rows.get_Item(10, Missing.Value);
            oWS.Shapes.Item("Chart 1").Top = (float)(double)oResizeRange.Top;
            oResizeRange = (Excel.Range)oWS.Columns.get_Item(2, Missing.Value);
            oWS.Shapes.Item("Chart 1").Left = (float)(double)oResizeRange.Left;
        }

        ////////////////////////////////////////////////////////////////////////

        internal void autoMationRun2()
        {
            List<string> testData = new List<string>()
            { "Excel","Access","Word","OneNote"};

            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Add();
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;

                //데이터 넣기
                int r = 1;
                foreach (var d in testData)
                {
                    ws.Cells[r, 1] = d;
                    r++;
                }

                //파일 저장
                //wb.SaveAs("", Excel.XlFileFormat.xlWorkbookNormal);
                wb.Close(true);
                excelApp.Quit();

            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
            }
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
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

        ////////////////////////////////////////////////////////////////////////

        //OLEDB
        //c#에서 엑셀을 OLEDB로 이용할 경우에는 ADO.NET의 OleDb 클래스들을 사용하여 엑셀 데이타를 핸들링

        //data Provider
        // 엑셀 97 - 2003 버젼 .XLS  : Microsoft.Jet.OLEDB.4.0 or Microsoft.ACE.OLEDB.12.0
        // 엑셀 2007 이후의 .XLSX : Microsoft.ACE.OLEDB.12.0

        //provider 설치 확인 (Powershell )
        // (New-Object system.data.oledb.oledbenumerator).GetElements() | select SOURCES_NAME, SOURCES_DESCRIPTION

        internal void oledbRun()
        {
            string source = @"C:/Users/jrmoon/Desktop/★ TSNE연락처조직도_230228.xlsx";
            

            string szConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ source + ";";

            OleDbConnection conn = new OleDbConnection(szConn);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
            OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string data = string.Format("F1:{0}, F2:{1}, F3:{2}", dr[0], dr[1], dr[2]);
                MessageBox.Show(data);
            }

            // 엑셀 데이타 갱신
            cmd = new OleDbCommand("UPDATE [Sheet1$] SET F2='Hello' WHERE F1='a'", conn);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("UPDATE [Sheet1$A2:C2] SET F2='World'", conn);
            cmd.ExecuteNonQuery();

            // 데이타 추가
            cmd = new OleDbCommand("INSERT INTO [Sheet1$](F1,F2,F3) VALUES ('A3','B3','C3')", conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
