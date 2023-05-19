
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canvas.Calc
{
    public class CCalculatorArc
    {
        double x_1, x_2, x_3, x_4, y_1, y_2, y_3, y_4;
        double dx12, dy12, dx34, dy34;
        double lt;

        double xp_1, yp_1, xp_2, yp_2;
        //public CArc(Line L1, Line L2, double radius)

        double radius;

        public CCalculatorArc(double L1x, double L1y, double L2x, double L2y, double L3x, double L3y, double L4x, double L4y, double r)
        {
            //넘겨 주기전에 Line1,2 위치 확인하고 좌측부터  넘겨 주기
            x_1 = L1x;
            y_1 = L1y;

            //같아야 함
            x_2 = L2x;
            y_2 = L2y;
            x_4 = L4x;
            y_4 = L4y;

            x_3 = L3x;
            y_3 = L3y;

            radius = r;

        }

        public Arc calc()
        {
            //가. 라인 정보

            setLineInformation();

            //나. 라인 평행 확인
            if (dy12 * dx34 - dx12 * dy34 == 0)
            {
                MessageBox.Show("평행");
                return null;
            }

            //다 내각 계산
            calcInternalAngle();

            //라 중심점 거리계산
            calcCenterDistance();

            //마 TP1 TP2 계산
            initTPPoint();
            calcTPPoint();

            //바 직각점 계산
            calcRightAngle();


            //사 중심점 계산
            calcIntersectionPoint();
            calcCenterPoint();

            //아 각도 및 원의 H W 계산
            return calcArcInformation();
        }

        private Arc calcArcInformation()
        {
            double t1Angle = ConvertRadiansToDegrees(Math.Atan2(yt_1 - yc, xt_1 - xc));
            if (t1Angle < 0)
            {
                t1Angle = 360 + t1Angle;
            }

            double t2Angle = ConvertRadiansToDegrees(Math.Atan2(yt_2 - yc, xt_2 - xc));
            if (t2Angle < 0)
            {
                t2Angle = 360 + t2Angle;
            }

            double sweepAngle = 0;

            if (t1Angle > t2Angle)
            {
                sweepAngle = -(Math.Abs(t1Angle) - Math.Abs(t2Angle));
            }
            else
            {
                sweepAngle = Math.Abs(t2Angle) - Math.Abs(t1Angle);
            }

            //if (sweepAngle > 180)
            //{
            //    sweepAngle = 360 - sweepAngle;
            //}






            double width = radius * 2;
            double height = radius * 2;

            Arc arc = new Arc()
            {
                startAngle = t1Angle,
                sweepAngle = sweepAngle,
                width = width,
                height = height,
                xc = xc,
                yc = yc,
                xt_1 = xt_1,
                yt_1 = yt_1,
                xt_2 = xt_2,
                yt_2 = yt_2
            };

            return arc;
        }



        private bool calcCenterPoint()
        {
            //=SQRT((xc-x_2)^2+(yc-y_2)^2)
            double lcRe = Math.Sqrt(Math.Pow(xc - x_2, 2) + Math.Pow(yc - y_2, 2));
            //IF(lc-C51<0.000000001,"OK", "NG")
            if (lc - lcRe < 0.000000001)
            {
                //MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("NG");
                return false;
            }
            return true;
        }

        private void calcRightAngle()
        {
            double dx_1 = xt_1 - x_1;
            double dy_1 = yt_1 - y_1;

            double dx_2 = xt_2 - x_3;
            double dy_2 = yt_2 - y_3;

            xp_1 = xt_1 - dy_1;
            yp_1 = yt_1 + dx_1;

            xp_2 = xt_2 - dy_2;
            yp_2 = yt_2 + dx_2;
        }

        private bool calcTPPoint()
        {
            //SQRT((x_2 - x_1) ^ 2 + (y_2 - y_1) ^ 2);
            double TP1 = Math.Sqrt(Math.Pow(x_2 - x_1, 2) + Math.Pow(y_2 - y_1, 2));
            //SQRT((x_4-x_3)^2+(y_4-y_3)^2)
            double TP2 = Math.Sqrt(Math.Pow(x_4 - x_3, 2) + Math.Pow(y_4 - y_3, 2));

            if (lt >= TP1)
            {
                MessageBox.Show(" TP1 불가");
                return false;
            }


            if (lt >= TP2)
            {
                MessageBox.Show(" TP2 불가");
                return false;
            }

            return true;

        }

        double lc;
        private void calcCenterDistance()
        {
            //radius/TAN(RADIANS(theta/2))
            lt = radius / Math.Tan(ConvertToRadians(θ / 2));
            lc = radius / Math.Sin(ConvertToRadians(θ / 2));
        }

        double θ;
        private void calcInternalAngle()
        {
            //DEGREES(ATAN2(x_1-x_2,y_1-y_2))
            //- 음의 각이 발생하지 않고 항상 반시계방향의 양의 각이 되도록 처리함
            //- 두선의 순서에 따라 음의 각이 발생하지 않도록함
            // - 각도가 180이상의 외각이 되면 내각으로 변경함

            //c# Atan2 y , x  순서로 입력 받음
            //double θ1 = ConvertRadiansToDegrees(Math.Atan2(x_1 - x_2, y_1 - y_2));
            double θ1 = ConvertRadiansToDegrees(Math.Atan2(y_1 - y_2, x_1 - x_2));

            if (θ1 < 0)
            {
                θ1 = 360 + θ1;
            }

            double θ2 = ConvertRadiansToDegrees(Math.Atan2(y_3 - y_2, x_3 - x_2));
            if (θ2 < 0)
            {
                θ2 = 360 + θ2;
            }

            if (θ1 > θ2)
            {
                θ = θ1 - θ2;
            }
            else
            {
                θ = θ2 - θ1;
            }

            if (θ > 180)
            {
                θ = 360 - θ;
            }

        }


        private void setLineInformation()
        {
            dx12 = x_2 - x_1; //C10
            dx34 = x_4 - x_3; //C11

            dy12 = y_2 - y_1; //D10
            dy34 = y_4 - y_3; //D11
        }

        double xc, yc;

        private void calcIntersectionPoint()
        {
            //1. 두선의 교차점 계산
            //가. 두선의 양측점 좌표
            //나 방향별 차이
            double dx_12 = xp_1 - xt_1;
            double dy_12 = yp_1 - yt_1;

            double dx_34 = xp_2 - xt_2;
            double dy_34 = yp_2 - yt_2;

            //다 매개변수 계산
            double denominator = (dy_12 * dx_34 - dx_12 * dy_34);

            double s, xi, yi, t = 0;
            if (denominator != 0)
            {
                t = ((xt_1 - xt_2) * dy_34 + (yt_2 - yt_1) * dx_34) / denominator;
            }

            if (denominator != 0)
            {
                s = ((xt_1 - xt_2) * dy_12 + (yt_2 - yt_1) * dx_12) / denominator;
            }

            //라 두선의 교차상태 확인
            //분모가 0 이면 두선은 교차하지 않는다.
            if (dy_12 * dx_34 - dx_12 * dy_34 == 0)
            {
                MessageBox.Show("교차하지 않는다");
            }

            //마 교차점 계산

            if (denominator != 0)
            {
                xi = xt_1 + t * dx_12;
                xc = xi;
                yi = yt_1 + t * dy_12;
                yc = yi;
            }


        }


        double P2x, P2y, P1x, P1y, P3x, P3y, P4x, P4y;
        double xt_1, yt_1, xt_2, yt_2;

        private void initTPPoint()
        {
            //1. 거리의 점 계산 (Line1)
            //가 좌표 입력

            P2x = x_2;
            P2y = y_2;
            P1x = x_1;
            P1y = y_1;

            //나 Line 의 길이 계산
            double dx = P1x - P2x;
            double dy = P1y - P2y;
            //SQRT(L9^2+L10^2)
            double length = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

            //다. 거리의 점 계산

            xt_1 = P2x + dx / length * lt;
            yt_1 = P2y + dy / length * lt;

            //1. 거리의 점 계산 (Line2)
            //가. 좌표 입력
            P4x = x_4;
            P4y = y_4;
            P3x = x_3;
            P3y = y_3;

            //나 Line 의 길이 계산
            dx = P3x - P4x;
            dy = P3y - P4y;
            length = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

            //다. 거리의 점 계산
            xt_2 = P4x + dx / length * lt;
            yt_2 = P4y + dy / length * lt;

        }

        private double ConvertRadiansToDegrees(double radians)
        {
            return (180 / Math.PI) * radians;
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

    }


    public class Arc
    {
        public double startAngle { get; set; }
        public double endAngle { get; set; }
        public double sweepAngle { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double xc { get; set; }
        public double yc { get; set; }
        public double xt_1 { get; set; }
        public double yt_1 { get; set; }
        public double xt_2 { get; set; }
        public double yt_2 { get; set; }



    }
}

