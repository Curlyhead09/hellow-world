using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace test_processWinform
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = "ls";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;

            using (var process = Process.Start(psi))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string data = reader.ReadToEnd();

                    File.WriteAllText("output.txt", data);
                }              
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();

            Array.ForEach(processes, (process) =>
            {
                Console.WriteLine("Process: {0} Id: {1}",
                    process.ProcessName, process.Id);
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("Firefox");
            Console.WriteLine("{0} Firefox processes", processes.Length);

            Array.ForEach(processes, (process) =>
            {
                Console.WriteLine("Process: {0} Id: {1}",
                    process.ProcessName, process.Id);
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var process = Process.Start("notepad.exe");

            Thread.Sleep(3000);
            process.Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var process = new Process();

            process.StartInfo.FileName = "notepad.exe";
            process.StartInfo.Arguments = @"C:\Users\Jano\Documents\words.txt";
            process.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var process = new Process();

            process.StartInfo.FileName = "notepad.exe";
            process.Start();
        }
    }
}
