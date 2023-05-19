using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_Delegate
{
    class MyClass
    {
        //메서드 파라미터와 리턴 타입에 대한 정의를 한 후, 동일한 파라미터와 리턴 타입을 가진 메서드를 서로 호환해서 불러 쓸 수 있는 기능

        //1 선언
        private delegate void RunDelegate(int i);


        private void RunThis(int val)
        {
            Console.WriteLine($"{val}");
        }

        private void RunThat(int val)
        {
            Console.WriteLine($"0x{val:X}");
        }

        public void Perform()
        {
            //2 인스턴스 생성
            RunDelegate run = new RunDelegate(RunThis);
            //3 실행
            run(1024);

            run = RunThat;
            run(1024);

        }

    }

    class MySort
    {
        public delegate int CompareDelegate(int i1, int i2);

        public static void Sort(int[] arr, CompareDelegate comp)
        {
            if (arr.Length < 2) return;

            Console.WriteLine("함수 Prototype: " + comp.Method);

            int ret;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    ret = comp(arr[i], arr[j]);
                    if (ret == -1)
                    {
                        int tmp = arr[j];
                        arr[j] = arr[i];
                        arr[i] = tmp;
                    }
                }
            }
            Display(arr);
        }

        private static void Display(int[] arr)
        {
            foreach (var i in arr) Console.Write(i + " ");
            Console.WriteLine();
        }

    }

    class MyArea : Form
    {
        public MyArea()
        {
            this.MouseClick += delegate { MyAreaClicked(); };
        }

        public delegate void ClickDelegate(object sender);

        public ClickDelegate MyClick;

        private void MyAreaClicked()
        {
            if (MyClick != null)
            {
                MyClick(this);
            }
        }
    }

    class MyCalc
    {
        public delegate int calculator(int i1, int i2);

        public static int Calc(int i1, int i2, calculator calc)
        {
            int result = calc(i1, i2);

            Console.WriteLine(result);
            return result;
        }
    }
}
