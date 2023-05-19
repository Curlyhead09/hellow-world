using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_mergeCSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourceFolder = "";
            string destinationFolder = "";

            mergeCSVFiles(sourceFolder, destinationFolder);
        }


        bool mergeCSVFiles(string sourceFolder, string destinationFolder)
        {

            try
            {
                string[] filePath = Directory.GetFiles(sourceFolder, "*.csv");
                using (StreamWriter fileWriter = new StreamWriter(destinationFolder, true))
                {
                    for (int i = 0; i < filePath.Length; i++)
                    {
                        string file = filePath[i];

                        string[] lines = File.ReadAllLines(file);

                        if (i > 0)
                            lines = lines.Skip(1).ToArray();

                        foreach (var line in lines)
                        {
                            fileWriter.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public static void CombineMisMatchedCsvFiles(string[] filePaths, string destinationFile, char splitter = ',')
        {

            HashSet<string> combinedheaders = new HashSet<string>();
            int i;
            // aggregate headers
            for (i = 0; i < filePaths.Length; i++)
            {
                string file = filePaths[i];
                combinedheaders.UnionWith(File.ReadLines(file).First().Split(splitter));
            }
            var hdict = combinedheaders.ToDictionary(y => y, y => new List<object>());

            string[] combinedHeadersArray = combinedheaders.ToArray();
            for (i = 0; i < filePaths.Length; i++)
            {
                var fileheaders = File.ReadLines(filePaths[i]).First().Split(splitter);
                var notfileheaders = combinedheaders.Except(fileheaders);

                //File.ReadLines(filePaths[i]).Skip(1).Select(line => line.Split(splitter)).ForEach(spline =>
                //{
                //    for (int j = 0; j < fileheaders.Length; j++)
                //    {
                //        hdict[fileheaders[j]].Add(spline[j]);
                //    }
                //    foreach (string header in notfileheaders)
                //    {
                //        hdict[header].Add(null);
                //    }

                //});

                foreach (var spline in File.ReadLines(filePaths[i]).Skip(1).Select(line => line.Split(splitter)))
                {
                    for (int j = 0; j < fileheaders.Length; j++)
                    {
                        hdict[fileheaders[j]].Add(spline[j]);
                    }
                    foreach (string header in notfileheaders)
                    {
                        hdict[header].Add(null);
                    }
                }

            }

            DataTable dt = hdict.ToDataTable();

            dt.ToCSV(destinationFile);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] filePath = Directory.GetFiles(@"C:\AD_Work\Actuator-CAT_3D\CModel\current", "*.csv");
            CombineMisMatchedCsvFiles(filePath, @"C:\AD_Work\Actuator-CAT_3D\CModel\current\merge.csv");

         
        }
    }

    public static class DataTableHelper
    {
        public static DataTable ToDataTable(this Dictionary<string, List<object>> dict)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.AddRange(dict.Keys.Select(c => new DataColumn(c)).ToArray());

            for (int i = 0; i < dict.Values.Max(item => item.Count()); i++)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (var key in dict.Keys)
                {
                    if (dict[key].Count > i)
                        dataRow[key] = dict[key][i];
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public static void ToCSV(this DataTable dt, string destinationfile)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(destinationfile, sb.ToString());
        }
    }
}
