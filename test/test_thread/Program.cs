using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace test_thread
{
    class Program
    {
        static void Main(string[] args)
        {          
            Program program = new Program();

            string input = Console.ReadLine();

            if (input.Equals("0"))
            {
                //
                program.DoTest();
                Console.ReadKey();
            }

        }

        private void DoTest()
        {
            try
            {
                Thread[] myThreads =
                {
                new Thread(new ThreadStart(SayHello)),
                new Thread(new ThreadStart(SayMerry)),
                new Thread(new ThreadStart(SayPapi)),
                new Thread(new ThreadStart(SayHappy))
                };



                foreach (Thread myThread in myThreads)
                {
                    myThread.Start();
                }

                Thread.Sleep(5);
                //ThreadInterruptedException
                if (false)
                {
                    myThreads[1].Interrupt();
                }


                //ThreadStateException
                if (false)
                {
                    myThreads[1].Abort();    // 쓰레드를 중지시킴

                    for (int LoopCtr = 1; LoopCtr < 100; LoopCtr++)
                    {
                        //do nothing
                    }

                    myThreads[1].Resume();   // ThreadStateException 예외 발생    
                }

                //ThreadAbortException 
                if (false)
                {
                    for (int LoopCtr = 1; LoopCtr < 1000; LoopCtr++)
                    {
                        //do nothing
                    }

                    Console.WriteLine(" --- Thread State : " + myThreads[1].ThreadState);
                    myThreads[1].Abort();
                    Console.WriteLine(" --- Thread State : " + myThreads[1].ThreadState);
                }

                if (true)
                {

                    for (int LoopCtr = 1; LoopCtr < 1000; LoopCtr++)
                    {
                        //do nothing
                    }

                    Thread.Sleep(10);
                    myThreads[1].Abort();
                }

            }
            catch (ThreadStateException)
            {

                Console.WriteLine("ThreadStateException");
            }
        }
        private void SayHello()
        {
            for (int LoopCtr = 1; LoopCtr < 10; LoopCtr++)
            {
                Console.WriteLine("Hello, everyone.");
                Thread.Sleep(100);
            }
        }

        private void SayMerry()
        {
            try
            {
                for (int LoopCtr = 1; LoopCtr < 10; LoopCtr++)
                {
                    Console.WriteLine("Merry~~~~~~");
                    Thread.Sleep(100);
                }
            }
            catch (ThreadInterruptedException)
            {

                Console.WriteLine("ThreadInterruptedException -SayMerry()-");
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("--- SayMerry Aborted ---");
                Console.WriteLine("------------------------");
            }
            finally
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("--- SayMerry finally ---");
                Console.WriteLine("------------------------");
            }
        }

        private void SayPapi()
        {
            for (int LoopCtr = 1; LoopCtr < 10; LoopCtr++)
            {
                Console.WriteLine("Papi sounds good.");
                Thread.Sleep(100);
            }
        }

        private void SayHappy()
        {
            for (int LoopCtr = 1; LoopCtr < 10; LoopCtr++)
            {
                Console.WriteLine("All, Happy Programming.");
                Thread.Sleep(100);
            }
        }


    }
}
