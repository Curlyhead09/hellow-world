using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_SharedAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            SharedAPI sharedAPI = new SharedAPI();
            string server = "\\\\jrmoon-pc\\";
            try
            {
                Console.WriteLine("Folder Name : ");
                Console.WriteLine("ex) jrmoon-pc");
                string readLine = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(readLine))
                {
                    server = "\\\\" + readLine + "\\";
                }

                Console.WriteLine("Folder Name : ");
                string dir = Console.ReadLine();

                Console.WriteLine("ID : ");
                //string id = Console.ReadLine();

                string id = Console.ReadLine();// "tsne\\jrmoon";


                Console.WriteLine("PASSWORK : ");
                //string password = Console.ReadLine();
                string password = Console.ReadLine();//"tsne1234!";

                server = server + dir;

                sharedAPI.CencelRemoteServer(server);

                //int result = sharedAPI.ConnectRemoteServer(server,"tsne\\jrmoon","tsne1234!");
                int result = sharedAPI.ConnectRemoteServer(server, id, password);


                Console.WriteLine(result);

                if (result == 0)
                {
                    string path = server + "\\test.txt";
                    Console.WriteLine(File.ReadAllText(path));
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sharedAPI.CencelRemoteServer(server);
            }
        }
    }
}
