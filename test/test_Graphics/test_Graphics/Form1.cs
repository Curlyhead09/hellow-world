using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Graphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //https://blog.aspose.com/ko/drawing/graphics-programming-in-csharp/
        Bitmap bitmap;
        private void btn1_Click(object sender, EventArgs e)
        {


            // 이 코드 예제는 닫힌 곡선, 호 및 원을 그리는 방법을 보여줍니다.
            // 비트맵 생성
            bitmap = new Bitmap(this.Size.Width, this.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            // Bitmap의 이니셜 그래픽
            Graphics graphics = Graphics.FromImage(bitmap);

            // 그릴 펜 정의
            Pen penBlue = new Pen(Color.Blue, 4);

            int x = this.Size.Width / 2;
            int y = this.Size.Height / 2;

            // 곡선 그리기
            graphics.DrawClosedCurve(penBlue, new Point[] { new Point(10, y + y / 2), new Point(y/2, y), new Point(y, 10), new Point((int)(y*1.5), y), new Point(y*2, y + y / 2) });

            // 호 그리기
            Pen penRed = new Pen(Color.Red, 2);
            graphics.DrawArc(penRed, 0, 0, y+y/2 , y+y/2 , 0, 180);

            // 타원 그리기
            Pen penGreen = new Pen(Color.Green, 2);
            graphics.DrawEllipse(penGreen, 10, 10, y, y);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bitmap != null)
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 이 코드 예제는 다각형과 사각형을 그리는 방법을 보여줍니다.
            // 비트맵 생성
            bitmap = new Bitmap(this.Size.Width, this.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);


            // Bitmap의 이니셜 그래픽
            Graphics graphics = Graphics.FromImage(bitmap);

            // 그릴 펜 정의
            Pen penBlue = new Pen(Color.Blue, 4);

            // 다각형 그리기
            graphics.DrawPolygon(penBlue, new Point[] { new Point(30, 30), new Point(Size.Width/2, this.Size.Height-60), new Point(Size.Width-60, 30) });

            // 직사각형 그리기
            Pen penRed = new Pen(Color.Red, 2);
            graphics.DrawRectangle(penRed, 10, 10, Size.Width-40, this.Size.Height-60);

            this.Invalidate();
        }
    }
}
