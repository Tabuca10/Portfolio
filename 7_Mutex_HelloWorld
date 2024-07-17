using System;
using System.Threading;

namespace Mutex
{
    internal class Program
    {
        private static System.Threading.Mutex mutex = new System.Threading.Mutex();

        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(MostrarPalabraHello);
            ThreadStart ts2 = new ThreadStart(MostrarPalabraWorld);
            ThreadStart ts3 = new ThreadStart(MostrarPalabraCon);

            Thread t2 = new Thread(ts1);
            Thread t3 = new Thread(ts2);
            Thread t4 = new Thread(ts3);

            t2.Start();
            t3.Start();
            t4.Start();

            Thread.Sleep(100);
            MostrarPalabraThreads();

            t2.Join();
            t3.Join();
            t4.Join();

            Console.ReadKey();
        }

        static void MostrarPalabraHello()
        {
            while (true)
            {
                mutex.WaitOne();

                Console.Write("Hello " + " ");
                Thread.Sleep(100);

                mutex.ReleaseMutex();
            }
        }
        static void MostrarPalabraWorld()
        {
            while (true)
            {
                mutex.WaitOne();

                Console.Write("World " + " ");
                Thread.Sleep(100);

                mutex.ReleaseMutex();
            }
        }
        static void MostrarPalabraCon()
        {
            while (true)
            {
                mutex.WaitOne();

                Console.Write("con " + " ");
                Thread.Sleep(100);

                mutex.ReleaseMutex();
            }
        }
        static void MostrarPalabraThreads()
        {
            while (true)
            {
                mutex.WaitOne();

                Console.Write("Threads " + " ");
                Thread.Sleep(100);

                mutex.ReleaseMutex();

                Console.WriteLine();
            }
        }
    }
}
