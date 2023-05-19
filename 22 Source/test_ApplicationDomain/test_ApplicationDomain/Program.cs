using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_ApplicationDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            AEDT adet = new AEDT();

            AppDomain tempDomain = AppDomain.CreateDomain("temp_excel");
            tempDomain.DoCallBack(() => adet.connectAedtProgID());
            AppDomain.Unload(tempDomain);


            Console.ReadLine();


        }

        static void callbackApplicationDomain2()
        {
            AppDomain tempDomain = AppDomain.CreateDomain("temp_excel");
            tempDomain.DoCallBack(() => ExcelTest());
            AppDomain.Unload(tempDomain);  

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void ExcelTest()
        {

        }

        static string greetings = "PONG!";

        static void callbackApplicationDomain()
        {
            AppDomain otherDomain = AppDomain.CreateDomain("otherDomain");

            greetings = "PING!";
            MyCallBack();
            otherDomain.DoCallBack(new CrossAppDomainDelegate(MyCallBack));
        }

        static public void MyCallBack()
        {
            string name = AppDomain.CurrentDomain.FriendlyName;

            if (name == AppDomain.CurrentDomain.SetupInformation.ApplicationName)
            {
                name = "defaultDomain";
            }
            Console.WriteLine(greetings + " from " + name);
        }


        static void createDomain()
        {

            Console.WriteLine("Create new AppDomain");
            AppDomain domain = AppDomain.CreateDomain("MyDomain");

            Console.WriteLine("Host domain  : " + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("Child domain : " + domain.FriendlyName);

            Console.WriteLine("\nPlease Endter Key...\n");
            Console.ReadKey();
        }

        static void unloadApplicationDomain()
        {
            Console.WriteLine("Creating new AppDomain.");
            AppDomain domain = AppDomain.CreateDomain("MyDomain", null);

            Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("child domain: " + domain.FriendlyName);

            try
            {
                AppDomain.Unload(domain);
                Console.WriteLine();
                Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);

                // The following statement creates an exception because the domain no longer exists.
                Console.WriteLine("child domain: " + domain.FriendlyName); //AppDomainUnloadedException

                Console.WriteLine("\nPlease Endter Key...\n");
                Console.ReadKey();

            }
            catch (AppDomainUnloadedException ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine("The appdomain MyDomain does not exist.");

                Console.WriteLine("\nPlease Endter Key...\n");
                Console.ReadKey();
            }
        }

        static void setupApplicationDomain()
        {
            // Create application domain setup information.
            AppDomainSetup domaininfo = new AppDomainSetup();
            domaininfo.ApplicationBase = System.IO.Directory.GetCurrentDirectory();

            // Create the application domain.
            AppDomain domain = AppDomain.CreateDomain("MyDomain", null, domaininfo);

            // Write application domain information to the console.
            Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("child domain: " + domain.FriendlyName);
            Console.WriteLine("Application base is: " + domain.SetupInformation.ApplicationBase);

            // Unload the application domain.
            AppDomain.Unload(domain);

            Console.WriteLine("\nPlease Endter Key...\n");
            Console.ReadKey();
        }

        static void loadAssemblyIntoApplicationDomain()
        {
            // Use the file name to load the assembly into the current
            // application domain.
            System.Reflection.Assembly a = System.Reflection.Assembly.Load("ClassLibrary1");
            // Get the type to use.
            // example      Namespace
            // Class1       Class
            // example_1    Dll file name

            Type myType = a.GetType("ClassLib.MyClass");

            // Get the method to call.
            System.Reflection.MethodInfo myMethod = myType.GetMethod("MethodA");
            // Create an instance.
            object obj = Activator.CreateInstance(myType);
            // Execute the method.
            object obj2 = myMethod.Invoke(obj, null);
        }

        static void getAssemblyInfo()
        {
            object obj = null;
            System.Reflection.Assembly asm = null;
            System.Reflection.MemberInfo[] info = null;

            try
            {
                asm = System.Reflection.Assembly.Load("ClassLibrary1");
                obj = Asminfo.Get(asm, "ClassLib.MyClass");

                //asm = System.Reflection.Assembly.Load("ClassLibrary2");
                //obj = Asminfo.Get(asm, "ClassLibrary2.MyClass2");

                info = (System.Reflection.MemberInfo[])obj;

                foreach (System.Reflection.MemberInfo m in info)
                {
                    Console.WriteLine(" '{0}' is a {1}", m.Name, m.MemberType);
                }

                Console.WriteLine("\nPlease Endter Key...\n");
                Console.ReadKey();
            }
            catch
            {
                //
            }
        }
    }

    class Asminfo
    {
        public static object Get(System.Reflection.Assembly asm, string name)
        {
            try
            {
                Type type = asm.GetType(name);
                if (type != null)
                {
                    System.Reflection.MemberInfo[] info = type.GetMembers();
                    return info;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }


}
