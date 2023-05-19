using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_async_await
{

    //사용 방법   대체 방법   수행할 작업
    //  await Task.Wait 또는 Task.Result 백그라운드 작업의 결과 검색
    //  await Task.WhenAny Task.WaitAny 작업이 완료될 때까지 대기
    //  await Task.WhenAll Task.WaitAll 모든 작업이 완료될 때까지 대기
    //  await Task.Delay Thread.Sleep 일정 기간 대기

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        //C# 5.0부터 새로운 C# 키워드로 async와 await
        //async는 컴파일러에게 해당 메서드가 await를 가지고 있음을 알려주는 역활을 한다
        //async라고 표시된 메서드는 await를 1개 이상 가질 수 있는데, 하나도 없는 경우라도 컴파일은 가능하지만 Warning 메시지가 표시
        //async를 표시한다고 해서 자동으로 비동기 방식으로 프로그램을 수행하는 것은 아니고, 일종의 보조 역활을 하는 컴파일러 지시어로 볼 수 있다
        //리턴 타입은 대부분의 경우 Task<TResult> (리턴값이 있는 경우) 혹은 Task (리턴값이 없는 경우) 인데, 예를 들어 리턴값이 string일 경우 async Task<string> method() 와 같이 정의

        //await는 일반적으로 Task 혹은 Task<T> 객체와 함께 사용된다
        //Task 이외의 클래스도 사용 가능한데, awaitable 클래스, 즉 GetAwaiter() 라는 메서드를 갖는 클래스이면 함께 사용 가능

        //!!
        // UI 쓰레드가 정지되지 않고 메시지 루프를 계속 돌 수 있도록 필요한 코드를 컴파일러가 await 키워드를 만나면 자동으로 추가
        private void btnrun_Click(object sender, EventArgs e)
        {
            label1.Text = "0";
            btnrun.Enabled = false;
            run();
        }

        private async void run()
        {
            var task1 = Task.Run(() => LongCalcAsync(10));

            int sum = await task1;

            label1.Text = "Sum = " + sum;

            btnrun.Enabled = true;
        }

        private int LongCalcAsync(int v)
        {
            int result = 0;

            for (int i = 0; i < v; i++)
            {


                setPropertyValue(label1, "Text", i.ToString());

                result += 1;
                Thread.Sleep(1000);
            }

            return result;
        }

        void setPropertyValue(Control ctl, string proName, string value)
        {
            try
            {
                if (ctl.InvokeRequired)
                {
                    ctl.Invoke(new Action(() =>
                    {
                        Type type = ctl.GetType();
                        PropertyInfo pi = type.GetProperty(proName);
                        pi.SetValue(ctl, value, null);
                    }
                    ));
                }         

                //실패
                // BeginInvokeObject_Method(listBox1, pName: "Items", mName: "Add", message);
            }
            catch (Exception)
            {

            }
        }


        //await : Task.ContinueWith()
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "0";
            btnrun.Enabled = false;
            run2();
        }

        private void run2()
        {
            var task1 = Task<int>.Run(() => LongCalc2(10));

            task1.ContinueWith(x =>
            {
                this.label1.Text = "Sum = " + task1.Result;
                this.button1.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task<int> LongCalc2(int times)
        {
            //UI Thread에서 실행
            //Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += i;
                await Task.Delay(1000);
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task<int> task = GetUrlContentLengthAsync();
        }

        public async Task<int> GetUrlContentLengthAsync()
        {
            var client = new HttpClient();

            Task<string> getStringTask =
                client.GetStringAsync("https://docs.microsoft.com/dotnet");

            DoIndependentWork();

            string contents = await getStringTask;

            return contents.Length;
        }

        void DoIndependentWork()
        {
            Console.WriteLine("Working...");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task task = DisplayCurrentInfoAsync();//Task 반환 형식

            Task<int> task1 = GetLeisureHoursAsync();//TaskTResult<> 반환 형식
        }

        public static async Task DisplayCurrentInfoAsync()
        {
            await WaitAndApologizeAsync();

            Console.WriteLine($"Today is {DateTime.Now:D}");
            Console.WriteLine($"The current time is {DateTime.Now.TimeOfDay:t}");
            Console.WriteLine("The current temperature is 76 degrees.");
        }

        static async Task WaitAndApologizeAsync()
        {
            await Task.Delay(2000);

            Console.WriteLine("Sorry for the delay...\n");
        }

        public static async Task ShowTodaysInfoAsync()
        {
            string message =
                $"Today is {DateTime.Today:D}\n" +
                "Today's hours of leisure: " +
                $"{await GetLeisureHoursAsync()}";

            Console.WriteLine(message);
        }

        static async Task<int> GetLeisureHoursAsync()
        {
            DayOfWeek today = await Task.FromResult(DateTime.Now.DayOfWeek);

            int leisureHours =
                today is DayOfWeek.Saturday || today is DayOfWeek.Sunday
                ? 16 : 5;

            return leisureHours;
        }

        //텍스트 읽기
        public async Task SimpleReadAsync(string filePath)
        {           
            //string text = await File.ReadAllTextAsync(filePath);
            //Console.WriteLine(text);
        }

    }
}
