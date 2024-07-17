using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static int[] valores;
        static int primeraMitad;
        static int segundaMitad;
        static void SecondaryThread()
        {
            for(int i = 0; i < 50; i++)
            {
                valores[primeraMitad] = 1;
                primeraMitad++;

            }
        }
        static void Main(string[] args)
        {
            valores = new int[100];
            primeraMitad = 0;
            segundaMitad = 50;

            for (int i = 0; i < 100; i++)
            {
                valores[i] = 0;
            }
            ThreadStart ts = new ThreadStart(SecondaryThread);
            Thread thread = new Thread(ts);
            thread.Start();           
            for (int i = 0; i < 50; i++)
            {                
                valores[segundaMitad] = 1;
                segundaMitad++;
            }
            thread.Join();
            int total = 0;
            for (int i = 0; i < 100; i++)
            {
                total += valores[i];
            }
            if (total == 100)
            {
                Console.WriteLine("Okay");
            }
            else
            {
                Console.WriteLine("No Okay");
            }
            Console.ReadLine();
        }
    }
}
