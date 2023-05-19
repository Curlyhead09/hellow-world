using Infragistics.Win.DataVisualization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_msChart
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();
            chart.Update();

            NumericXAxis xAxis;
            NumericYAxis yAxis;

            if (chart.Axes.Count == 0)
            {
                xAxis = new NumericXAxis();
                yAxis = new NumericYAxis();

                chart.Axes.Add(xAxis);
                chart.Axes.Add(yAxis);
            }
            else
            {
                xAxis = (NumericXAxis)chart.Axes[0];
                yAxis = (NumericYAxis)chart.Axes[1];
            }

            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                ScatterLineSeries series = new ScatterLineSeries();
                setDataChart_Line(chart, ref series, "Title" + i, SettingMyData(), "TitleX", "X", "TitleY", "Y", Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)));

                //ScatterLineSeries series = new ScatterLineSeries
                //{
                //    Name = "Title",
                //    XAxis = xAxis,
                //    YAxis = yAxis,
                //    XMemberPath = "X",
                //    YMemberPath = "Y",
                //    ShowDefaultTooltip = true,
                //    Brush = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)),
                //    Title= "Title"+i
                //};

                //series.DataSource = SettingMyData();
                chart.Series.Add(series);

                //chart.Update();
            }
            //Application.DoEvents();
        }


        void setDataChart_Line(UltraDataChart chart, ref ScatterLineSeries series, string seriesTitle, List<chartData> ltData, string titelX, string titelXMember, string titleY, string titleYMember, System.Drawing.Color color, bool useMarker = false)
        {
            NumericXAxis xAxis;
            NumericYAxis yAxis;

            if (chart.Axes.Count == 0)
            {
                xAxis = new NumericXAxis();
                yAxis = new NumericYAxis();

                chart.Axes.Add(xAxis);
                chart.Axes.Add(yAxis);
            }
            else
            {
                xAxis = (NumericXAxis)chart.Axes[0];
                yAxis = (NumericYAxis)chart.Axes[1];
            }



            xAxis.Title = titelX;
            yAxis.Title = titleY;

            series = new ScatterLineSeries
            {
                Name = seriesTitle,
                XAxis = xAxis,
                YAxis = yAxis,
                XMemberPath = titelXMember,
                YMemberPath = titleYMember,
                DataSource = ltData,
                ShowDefaultTooltip = true,
                Brush = color,
                Title = seriesTitle
            };

            if (useMarker)
                series.MarkerType = MarkerType.Circle;
            else
            {
                series.MarkerType = MarkerType.Circle;
                //참고
                //https://www.infragistics.com/community/forums/f/ultimate-ui-for-wpf/104156/scatterlineseries-tooltip-is-not-working
                series.MarkerBrush = new SolidColorBrush(System.Drawing.Color.FromArgb(0, 0, 0, 0));
                series.MarkerOutline = new SolidColorBrush(System.Drawing.Color.FromArgb(0, 0, 0, 0));
            }



            chart.IsHorizontalZoomEnabled = true;
            chart.IsVerticalZoomEnabled = true;

        }


        private List<chartData> SettingMyData()
        {
            Random r = new Random();

            List<chartData> lt = new List<chartData>()
            {
                //new chartData() { X = 0, Y = r.Next(3, 100) },
                //new chartData() { X = 2, Y = r.Next(3, 100) },
                //new chartData() { X = 3, Y = r.Next(3, 100) },
                //new chartData() { X = 4, Y = r.Next(3, 100) },
                //new chartData() { X = 5, Y = r.Next(3, 100) },
                //new chartData() { X = 6, Y = r.Next(3, 100) },
            };

            for (int i = 0; i < 30; i++)
            {
                lt.Add(new chartData() { X = i, Y = r.Next(0, 100) });
            }


            return lt;
        }


        class chartData
        {

            public int X { get; set; }
            public int Y { get; set; }
        }



    }
}
