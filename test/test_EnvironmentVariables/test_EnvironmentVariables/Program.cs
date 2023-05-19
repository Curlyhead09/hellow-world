using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_EnvironmentVariables
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine();
            Console.WriteLine("GetEnvironmentVariables: ");
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                if (de.Key.ToString().Contains("Ansys") || de.Key.ToString().Contains("ANSYS") || de.Key.ToString().Contains("ansys"))
                    Console.WriteLine("  {0} = {1}", de.Key, de.Value);
            }


            Console.ReadLine();

        }
    }
}
