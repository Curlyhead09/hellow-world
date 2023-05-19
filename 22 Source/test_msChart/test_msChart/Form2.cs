using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace test_msChart
{
    public partial class Form2 : Form
    {

        Series data_series = new Series();
        ChartArea myChartArea = new ChartArea("LineChartArea");

        public Form2()
        {
            InitializeComponent();         
        }

        private void SettingMyData(Series series)
        {
            Random r = new Random();

            Push_Data(series, new DateTime(2014, 7, 21, 10, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 21, 18, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 22, 03, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 22, 08, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 22, 14, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 23, 10, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 23, 16, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 24, 11, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 24, 13, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 24, 16, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 25, 12, 11, 00), r .Next(3,100));
            Push_Data(series, new DateTime(2014, 7, 25, 16, 11, 00), r.Next(3, 100));
        }
        private void Push_Data(Series series, DateTime dt, int data)
        {
            DataPoint dp = new DataPoint(); //데이타 기록하기 정도
            dp.SetValueXY(dt, data);
            series.Points.Add(dp);

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                Series series = new Series();

                series.ChartType = SeriesChartType.Line;
                series.Name = "VAS" + i;
                series.XValueType = ChartValueType.DateTime;
                series.IsValueShownAsLabel = true;
                series.LabelForeColor = Color.Red;
                series.MarkerStyle = MarkerStyle.Square;
                series.MarkerSize = 3;
                series.MarkerColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));

                SettingMyData(series);

                chart1.Series.Add(series);
            }
        }


    }
}
