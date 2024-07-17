using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads_3
{
    internal class Program
    {
        static Random rnd = new Random();

        static bool threadAcabado = false;

        static int diezMil = 0;
        static int numAleatorio = rnd.Next();

        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(thread2);
            ThreadStart ts2 = new ThreadStart(thread3);
            ThreadStart ts3 = new ThreadStart(thread4);

            Thread t2 = new Thread(ts1);
            Thread t3 = new Thread(ts2);
            Thread t4 = new Thread(ts3);
           
            t2.Start();
            t3.Start();
            t4.Start();

            t2.Join();
            t3.Join();

            threadAcabado = true;
            Console.WriteLine("Primer y segundo thread finalizado.");

            t4.Join();
            Console.WriteLine(diezMil);

            Console.ReadKey();
        }

        public static void thread2()
        {
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(diezMil);
                diezMil++;
            }
        }
        public static void thread3()
        {
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(diezMil);
                diezMil--;
            }
        }
        public static void thread4()
        {
            while (threadAcabado == false)
            {
                Console.WriteLine(numAleatorio);
                Thread.Sleep(100);
            }
        }
    }
}
