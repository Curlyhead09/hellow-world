using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_SplashScreen
{
    public partial class SplashForm : Form
    {
        delegate void ProgressDelegate(int i);
        delegate void CloseDelegate();

        public SplashForm()
        {
            InitializeComponent();
        }

        static SplashForm splashForm;

        static ProgressBar ProgressBar;

        static public void ShowSplashScreen()
        {
            if (splashForm != null) return;
            splashForm = new SplashForm();
            Thread thread = new Thread(new ThreadStart(SplashForm.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);

            ProgressBar = splashForm.progressBar1;

            thread.Start();

            while (!splashForm.Visible)
            {

            }            
            worker();

        }

        static private void ShowForm()
        {
            if (splashForm != null) Application.Run(splashForm);                
        }

       static public void FormClose()
        {
            splashForm?.Invoke(new CloseDelegate(SplashForm.CloseFormInternal));
        }
        static private void CloseFormInternal()
        {
            if (splashForm != null)
            {
                splashForm.Close();
                splashForm = null;
            };
        }


       static private void Step(int i)
        {
            ProgressBar.Value = i;
        }

        static private void worker()
        {
            for (int i = 0; i <= 100; i++)
            {
                splashForm.Invoke(new ProgressDelegate(Step), i);
                System.Threading.Thread.Sleep(50);
            }
            System.Threading.Thread.Sleep(1000);
            splashForm.Invoke(new CloseDelegate(FormClose));
        }


    }
}
