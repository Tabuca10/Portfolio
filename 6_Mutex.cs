using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads_4
{
    internal class Program
    {
        static int variableCompartida = 0;

        static void Main(string[] args)
        {
            ThreadStart ts1 = new ThreadStart(Restar);

            Thread t2 = new Thread(ts1);

            t2.Start();

            for (int i = 0; i < 10000; i++)
            {
                variableCompartida++;
            }


            t2.Join();

            Console.WriteLine("Resultado final: " + variableCompartida);

            Console.ReadKey();
        }

        static void Restar()
        {
            for (int i = 0; i < 10000; i++)
            {
                variableCompartida--;
            }
        }
    }
}
