using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Delegate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            MyClass myClass = new MyClass();
            myClass.Perform();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] a = { 5, 53, 3, 7, 1 };

            // 올림차순으로 소트
            MySort.CompareDelegate compDelegate = AscendingCompare;
            MySort.Sort(a, compDelegate);

            // 내림차순으로 소트
            compDelegate = DescendingCompare;
            MySort.Sort(a, compDelegate);
        }

        // CompareDelegate 델리게이트와 동일한 Prototype
        int AscendingCompare(int i1, int i2)
        {
            if (i1 == i2) return 0;
            return (i2 - i1) > 0 ? 1 : -1;
        }

        // CompareDelegate 델리게이트와 동일한 Prototype
        int DescendingCompare(int i1, int i2)
        {
            if (i1 == i2) return 0;
            return (i1 - i2) > 0 ? 1 : -1;
        }

        MyArea area;
        private void button3_Click(object sender, EventArgs e)
        {
          
            area = new MyArea();
            area.MyClick = Area_Click;
            area.ShowDialog();
        }

         void Area_Click(object sender)
        {
            area.Text = "MyArea 클릭!";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i1 = 5;
            int i2 = 3;

            MyCalc.calculator calculator = add;
            MyCalc.Calc(i1, i2, add);

            calculator = substract;
            MyCalc.Calc(i1, i2, substract);
            calculator = multiply;
            MyCalc.Calc(i1, i2, multiply);

            calculator = divide;
            MyCalc.Calc(i1, i2, divide);

        }

        int add(int i1, int i2)
        {
            return i1 + i2;
        }

        int substract(int i1, int i2)
        {
            return i1 - i2;
        }

        int multiply(int i1, int i2)
        {
            return i1 * i2;
        }
        int divide(int i1, int i2)
        {
            return i1 / i2;
        }

    }
}
