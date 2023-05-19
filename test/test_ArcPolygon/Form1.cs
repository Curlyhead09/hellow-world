using Canvas.Calc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_ArcPolygon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            panel1.Paint += panel1_Paint;
        }

        void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (path != null)
            {
                if (path != null && path.PathPoints.Count() > 0)
                {
                    e.Graphics.FillPath(Brushes.LightGreen, path);


                    //e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(15, 15, 100, 100));
                }
            }




            //g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(15, 15, 100, 100));


        }

        GraphicsPath path;

        private void button1_Click(object sender, EventArgs e)
        {
            //this.ClientSize.Width - myControl.Width) / 2 

            float left = (this.ClientSize.Width - panel1.Width) / 2;
            float Top = (this.ClientSize.Height - panel1.Height) / 2;

            CCalculatorArc calc = new CCalculatorArc(
               Convert.ToDouble(x_1.Text) + panel1.Width/2, Convert.ToDouble(y_1.Text) + panel1.Height/2,
               Convert.ToDouble(x_2.Text) + panel1.Width/2, Convert.ToDouble(y_2.Text) + panel1.Height/2,
               Convert.ToDouble(x_3.Text) + panel1.Width/2, Convert.ToDouble(y_3.Text) + panel1.Height/2,
               Convert.ToDouble(x_4.Text) + panel1.Width/2, Convert.ToDouble(y_4.Text) + panel1.Height/2,
               Convert.ToDouble(radius.Text)
            );

            Arc arc = calc.calc();

            float x = (float)(arc.xc - arc.width / 2);
            float y = (float)(arc.yc + arc.width / 2);

            PointF cp = new PointF(x, y);

            float width = (float)arc.width;
            float height = (float)width;
            float xt_1 = (float)(arc.xt_1);
            float yt_1 = (float)(arc.yt_1);

            PointF tp = new PointF(xt_1, yt_1);

            if (path == null)
                path = new GraphicsPath();

            path.AddArc(cp.X, cp.Y, width, height, (float)arc.startAngle, (float)arc.sweepAngle);

            panel1.Invalidate();

        }

    }
}
