using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_StringComparison
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void test()
        {

            String[] cultureNames = { "en-US", "th-TH", "tr-TR" };
            String[] strings1 = { "a", "i", "case", };
            String[] strings2 = { "a-", "\u0130", "Case" };
            StringComparison[] comparisons = (StringComparison[])Enum.GetValues(typeof(StringComparison));

            foreach (var cultureName in cultureNames)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
                Console.WriteLine("Current Culture: {0}", CultureInfo.CurrentCulture.Name);
                for (int ctr = 0; ctr <= strings1.GetUpperBound(0); ctr++)
                {
                    foreach (var comparison in comparisons)
                        Console.WriteLine("   {0} = {1} ({2}): {3}", strings1[ctr],
                                          strings2[ctr], comparison,
                                          String.Equals(strings1[ctr], strings2[ctr], comparison));

                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
